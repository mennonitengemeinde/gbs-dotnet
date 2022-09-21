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
            ChurchName = u.Church.Name,
        }).OrderBy(u => u.FirstName).ToListAsync();

        return new ServiceResponse<List<UserDto>> { Data = users };
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
            ChurchName = u.Church.Name,
        }).FirstOrDefaultAsync(u => u.Id == userId);

        return user == null
            ? ServiceResponse<UserDto>.BadRequest("User not found")
            : new ServiceResponse<UserDto> { Data = user };
    }

    public async Task<ServiceResponse<List<UserDto>>> UpdateUserRole(int userId, string newRole)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return ServiceResponse<List<UserDto>>.BadRequest("User not found");
        }

        user.Role = newRole;
        await _context.SaveChangesAsync();

        return await GetUsers();
    }

    public async Task<ServiceResponse<List<UserDto>>> UpdateUserActiveState(int userId, bool newActiveState)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return ServiceResponse<List<UserDto>>.BadRequest("User not found");
        }

        user.IsActive = newActiveState;
        await _context.SaveChangesAsync();
        return await GetUsers();
    }

    public async Task<ServiceResponse<List<UserDto>>> UpdateUserChurch(int userId, UserUpdateChurchDto updateDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return ServiceResponse<List<UserDto>>.BadRequest("User not found");
        }

        user.ChurchId = updateDto.ChurchId;
        await _context.SaveChangesAsync();
        return await GetUsers();
    }
}