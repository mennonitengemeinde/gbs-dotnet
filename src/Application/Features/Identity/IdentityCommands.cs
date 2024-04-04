using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gbs.Application.Features.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gbs.Application.Features.Identity;

public class IdentityCommands(
    IGbsDbContext context,
    IMapper mapper,
    IIdentityQueries identityQueries,
    IConfiguration configuration,
    UserManager<User> userManager,
    SignInManager<User> signInManager)
    : IIdentityCommands
{
    public async Task<Result<string>> Login(string email, string password)
    {
        var user = await userManager.FindByNameAsync(email);
        if (user == null)
            return Result.BadRequest<string>("Incorrect email or password");

        if (user.IsActive == false)
            return Result.BadRequest<string>("You account needs to be verified. Please come back later.");

        var result = await signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
            return Result.BadRequest<string>("Incorrect email or password");

        user.LastLogin = DateTime.UtcNow;
        context.Users.Update(user);
        await context.SaveChangesAsync();

        return Result.Ok(await CreateToken(user));
    }

    public async Task<Result<string>> Add(RegisterRequest request)
    {
        if (await context.Users.AnyAsync(u => u.Email != null && u.Email.ToLower().Equals(request.Email.ToLower())))
            return Result.BadRequest<string>("Email already exists");

        var newUser = mapper.Map<User>(request);

        var result = await userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded)
            return Result.Ok(newUser.Id, "User created successfully");

        var error = result.Errors.Select(x => x.Description).First();
        return Result.BadRequest<string>(error);
    }

    public async Task<Result<UserResponse>> UpdateRoles(string id, List<string> newRoles)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<UserResponse>("User not found");

        var userRoles = await userManager.GetRolesAsync(user);

        var rolesToRemove = userRoles.Except(newRoles).ToList();
        if (rolesToRemove.Any())
        {
            var removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
                return Result.BadRequest<UserResponse>("Failed to remove roles");
        }

        var rolesToAdd = newRoles.Except(userRoles).ToList();
        if (!rolesToAdd.Any())
            return await identityQueries.GetById(user.Id);

        var result = await userManager.AddToRolesAsync(user, rolesToAdd);
        if (!result.Succeeded)
            return Result.BadRequest<UserResponse>("Failed to add roles");

        return await identityQueries.GetById(user.Id);
    }

    public async Task<Result<UserResponse>> UpdateActiveState(string id, bool request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.BadRequest<UserResponse>("User not found");

        user.IsActive = request;
        await context.SaveChangesAsync();
        return Result.Ok(mapper.Map<UserResponse>(user));
    }

    public async Task<Result<UserResponse>> UpdateChurch(string id, UpdateUserChurchRequest request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<UserResponse>("User not found");

        user.ChurchId = request.ChurchId;
        await context.SaveChangesAsync();
        return Result.Ok(mapper.Map<UserResponse>(user));
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<bool>("User not found");

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return Result.Ok(true);
    }

    private async Task<string> CreateToken(User user)
    {
        var userRoles = (await userManager.GetRolesAsync(user)).ToList();
        var claims = new List<Claim>
        {
            new("sub", user.Id),
            new("email", user.Email ?? string.Empty),
            new("name", $"{user.FirstName} {user.LastName}"),
            new("church_id", user.ChurchId.ToString() ?? string.Empty)
        };
        claims.AddRange(userRoles.Select(role => new Claim("role", role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(configuration.GetSection("AppSettings:Secret").Value ?? throw new InvalidOperationException()));

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