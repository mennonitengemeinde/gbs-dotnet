namespace gbs.Server.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<UserDto>>> GetUsers()
    {
        var users = await _context.Users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            EmailVerified = u.EmailVerified,
            Role = u.Role,
        }).ToListAsync();

        return new ServiceResponse<List<UserDto>> {Data = users};
    }

    public async Task<ServiceResponse<UserDto>> UpdateUserRole(int userId, string newRole)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return new ServiceResponse<UserDto> {Success = false, Message = "User not found"};
        }

        user.Role = newRole;
        await _context.SaveChangesAsync();

        return new ServiceResponse<UserDto>
        {
            Data = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailVerified = user.EmailVerified,
                Role = user.Role,
            }
        };
    }
}