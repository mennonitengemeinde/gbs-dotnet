using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace gbs.Server.Repository.AuthRepository;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthRepository(
        DataContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<string>> Register(RegisterDto request)
    {
        if (await UserExists(request.Email))
        {
            return ServiceResponse<string>.BadRequest("User already exists");
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
            return new ServiceResponse<string> { Data = newUser.Id, Message = "Registration successful" };

        var error = result.Errors.Select(x => x.Description).First();
        return ServiceResponse<string>.BadRequest(error);
    }

    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));
    }

    public async Task<ServiceResponse<string>> Login(string email, string password)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user == null)
        {
            return ServiceResponse<string>.BadRequest("Incorrect email or password");
        }

        if (user.IsActive == false)
        {
            return ServiceResponse<string>.BadRequest("You account needs to be verified. Please come back later.");
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
            return ServiceResponse<string>.BadRequest("Incorrect email or password");

        var response = new ServiceResponse<string>();
        user.LastLogin = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        response.Data = await CreateToken(user);

        return response;
    }

    public async Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return ServiceResponse<bool>.BadRequest("User not found");
        }

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "Password changed successfully",
        };
    }

    public async Task<ServiceResponse<List<RolesResponse>>> GetRoles()
    {
        var userRoles = GetUserRoles();
        List<RolesResponse> roles;
        if (userRoles.Contains(Roles.SuperAdmin))
        {
            roles = await _context.Roles.Select(x => new RolesResponse
            {
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName
            }).ToListAsync();
        }
        else
        {
            roles = await _context.Roles.Where(x => x.Name != Roles.SuperAdmin).Select(x => new RolesResponse
            {
                Id = x.Id,
                Name = x.Name,
                NormalizedName = x.NormalizedName
            }).ToListAsync();
        }

        return new ServiceResponse<List<RolesResponse>> { Data = roles };
    }

    public string GetUserId() =>
        _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public List<string> GetUserRoles() =>
        _httpContextAccessor.HttpContext!.User
            .FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

    public bool UserIsAdmin()
    {
        var role = GetUserRoles();
        return role.Contains(Roles.Admin) || role.Contains(Roles.SuperAdmin);
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

    // private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    // {
    //     using var hmac = new System.Security.Cryptography.HMACSHA512();
    //     passwordSalt = hmac.Key;
    //     passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    // }
    //
    // private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    // {
    //     using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
    //     var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    //     return computedHash.SequenceEqual(passwordHash);
    // }
}