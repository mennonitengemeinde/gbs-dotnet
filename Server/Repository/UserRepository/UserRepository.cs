namespace gbs.Server.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
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
            IsActive = u.IsActive,
            ChurchId = u.ChurchId,
            Church = u.Church,
        }).OrderBy(u => u.FirstName).ToListAsync();

        return new ServiceResponse<List<UserDto>> {Data = users};
    }

    public async Task<ServiceResponse<UserDto>> GetUserById(int userId)
    {
        var user = await _context.Users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            EmailVerified = u.EmailVerified,
            Role = u.Role,
            IsActive = u.IsActive,
            ChurchId = u.ChurchId,
            Church = u.Church,
        }).FirstOrDefaultAsync(u => u.Id == userId);

        return user == null
            ? new ServiceResponse<UserDto> {Success = false, Message = "User not found"}
            : new ServiceResponse<UserDto> {Data = user};
    }

    public async Task<ServiceResponse<UserDto>> UpdateUser(int userId, UserUpdateDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new ServiceResponse<UserDto> {Success = false, Message = "User not found"};
        }

        user.Role = userDto.Role;
        user.IsActive = userDto.IsActive;
        user.ChurchId = userDto.ChurchId;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return CreateUserDto(user);
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

        return CreateUserDto(user);
    }

    public async Task<ServiceResponse<UserDto>> UpdateUserActiveState(int userId, bool newActiveState)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return new ServiceResponse<UserDto> {Success = false, Message = "User not found"};
        }

        user.IsActive = newActiveState;
        await _context.SaveChangesAsync();
        return CreateUserDto(user);
    }

    public async Task<ServiceResponse<UserDto>> UpdateUserChurch(int userId, int churchId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return new ServiceResponse<UserDto> {Success = false, Message = "User not found"};
        }

        user.ChurchId = churchId;
        await _context.SaveChangesAsync();
        return CreateUserDto(user);
    }

    private static ServiceResponse<UserDto> CreateUserDto(User user)
    {
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
                IsActive = user.IsActive,
                ChurchId = user.ChurchId,
                Church = user.Church,
            }
        };
    }
}