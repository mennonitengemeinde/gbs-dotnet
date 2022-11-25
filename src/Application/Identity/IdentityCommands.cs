using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Gbs.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gbs.Application.Identity;

public class IdentityCommands : IIdentityCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityQueries _identityQueries;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public IdentityCommands(
        IGbsDbContext context,
        IMapper mapper,
        IIdentityQueries identityQueries,
        IConfiguration configuration,
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _context = context;
        _mapper = mapper;
        _identityQueries = identityQueries;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result<string>> Login(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user == null)
            return Result.BadRequest<string>("Incorrect email or password");

        if (user.IsActive == false)
            return Result.BadRequest<string>("You account needs to be verified. Please come back later.");

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
            return Result.BadRequest<string>("Incorrect email or password");

        user.LastLogin = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Result.Ok(await CreateToken(user));
    }

    public async Task<Result<string>> Add(RegisterDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Email != null && u.Email.ToLower().Equals(request.Email.ToLower())))
        {
            return Result.BadRequest<string>("Email already exists");
        }

        var newUser = _mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded)
            return Result.Ok(newUser.Id, "User created successfully");

        var error = result.Errors.Select(x => x.Description).First();
        return Result.BadRequest<string>(error);
    }

    public async Task<Result<UserDto>> UpdateRoles(string id, List<string> newRoles)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<UserDto>("User not found");

        var userRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = userRoles.Except(newRoles).ToList();
        if (rolesToRemove.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
                return Result.BadRequest<UserDto>("Failed to remove roles");
        }

        var rolesToAdd = newRoles.Except(userRoles).ToList();
        if (!rolesToAdd.Any())
            return await _identityQueries.GetById(user.Id);

        var result = await _userManager.AddToRolesAsync(user, rolesToAdd);
        if (!result.Succeeded)
            return Result.BadRequest<UserDto>("Failed to add roles");

        return await _identityQueries.GetById(user.Id);
    }

    public async Task<Result<UserDto>> UpdateActiveState(string id, bool request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.BadRequest<UserDto>("User not found");

        user.IsActive = request;
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<UserDto>(user));
    }

    public async Task<Result<UserDto>> UpdateChurch(string id, UserUpdateChurchDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<UserDto>("User not found");

        user.ChurchId = request.ChurchId;
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<UserDto>(user));
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<bool>("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    private async Task<string> CreateToken(User user)
    {
        List<string> userRoles = (await _userManager.GetRolesAsync(user)).ToList();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
        };
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? throw new InvalidOperationException()));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}