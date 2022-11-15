using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Gbs.Application.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gbs.Infrastructure.Persistence.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public AuthRepository(
        DataContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        IAuthenticatedUserService authenticatedUserService)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<Result<string>> Register(RegisterDto request)
    {
        if (await UserExists(request.Email))
        {
            return Result.BadRequest<string>("Email already exists");
        }

        var newUser = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded)
            return Result.Ok(newUser.Id, "User created successfully");

        var error = result.Errors.Select(x => x.Description).First();
        return Result.BadRequest<string>(error);
    }

    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));
    }

    public async Task<Result<string>> Login(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user == null)
        {
            return Result.BadRequest<string>("Incorrect email or password");
        }

        if (user.IsActive == false)
        {
            return Result.BadRequest<string>("You account needs to be verified. Please come back later.");
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
            return Result.BadRequest<string>("Incorrect email or password");

        user.LastLogin = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Result.Ok(await CreateToken(user));
    }

    public async Task<Result<bool>> ChangePassword(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return Result.NotFound<bool>("User not found");
        }

        await _context.SaveChangesAsync();
        
        return Result.Ok(true, "Password changed successfully");
    }

    public async Task<Result<List<RolesDto>>> GetRoles()
    {
        var userRoles = _authenticatedUserService.GetUserRoles();
        List<RolesDto> roles;
        if (userRoles.Contains(Roles.SuperAdmin))
        {
            roles = await _context.Roles.Select(x => new RolesDto
            {
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName
            }).ToListAsync();
        }
        else
        {
            roles = await _context.Roles.Where(x => x.Name != Roles.SuperAdmin).Select(x => new RolesDto
            {
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName
            }).ToListAsync();
        }

        return Result.Ok(roles);
    }

    private async Task<string> CreateToken(User user)
    {
        List<string> userRoles = (await _userManager.GetRolesAsync(user)).ToList();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
        };
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Secret").Value));

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