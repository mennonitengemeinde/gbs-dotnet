namespace gbs.Server.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IAuthRepository _authRepo;
    private readonly UserManager<User> _userManager;

    public UserRepository(DataContext context, IAuthRepository authRepo, UserManager<User> userManager)
    {
        _context = context;
        _authRepo = authRepo;
        _userManager = userManager;
    }

    public async Task<ServiceResponse<List<UserDto>>> GetUsers()
    {
        var response = new ServiceResponse<List<UserDto>>();

        if (!_authRepo.GetUserRoles().Contains(Roles.SuperAdmin))
        {
            response.Data = await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = u.UserRoles
                        .Select(r => r.Role.Name)
                        .Where(r => r != Roles.SuperAdmin)
                        .ToList(),
                    IsActive = u.IsActive,
                    ChurchId = u.ChurchId,
                    ChurchName = u.Church.Name,
                })
                .OrderBy(u => u.FirstName)
                .ToListAsync();
            return response;
        }

        response.Data = await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Roles = u.UserRoles
                    .Select(r => r.Role.Name)
                    .ToList(),
                IsActive = u.IsActive,
                ChurchId = u.ChurchId,
                ChurchName = u.Church.Name,
            })
            .OrderBy(u => u.FirstName)
            .ToListAsync();
        return response;
    }

    public async Task<ServiceResponse<UserDto>> GetUserById(string userId)
    {
        if (_authRepo.GetUserId() != userId && !_authRepo.GetUserRoles().Contains(Roles.SuperAdmin))
        {
            return ServiceResponse<UserDto>.BadRequest("User not found");
        }

        var user = await _context.Users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Roles = u.UserRoles
                .Select(r => r.Role.Name)
                .ToList(),
            IsActive = u.IsActive,
            ChurchId = u.ChurchId,
            ChurchName = u.Church.Name,
        }).FirstOrDefaultAsync(u => u.Id == userId);

        return user == null
            ? ServiceResponse<UserDto>.BadRequest("User not found")
            : new ServiceResponse<UserDto> { Data = user };
    }

    public async Task<ServiceResponse<List<UserDto>>> UpdateUserRole(string userId, List<string> newRoles)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return ServiceResponse<List<UserDto>>.BadRequest("User not found");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = userRoles.Except(newRoles).ToList();
        if (rolesToRemove.Any())
        {
            var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!result.Succeeded)
            {
                Console.WriteLine(result.Errors);
                return ServiceResponse<List<UserDto>>.BadRequest("Failed to remove roles");
            }
        }

        var rolesToAdd = newRoles.Except(userRoles).ToList();
        if (!rolesToAdd.Any()) return await GetUsers();
        {
            var result = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (result.Succeeded) return await GetUsers();
            Console.WriteLine(result.Errors);
            return ServiceResponse<List<UserDto>>.BadRequest("Failed to add roles");
        }
    }

    public async Task<ServiceResponse<List<UserDto>>> UpdateUserActiveState(string userId, bool newActiveState)
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

    public async Task<ServiceResponse<List<UserDto>>> UpdateUserChurch(string userId, UserUpdateChurchDto updateDto)
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