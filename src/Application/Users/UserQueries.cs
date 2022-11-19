using Gbs.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Gbs.Application.Users;

public class UserQueries : IUserQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public UserQueries(
        IGbsDbContext context, 
        IMapper mapper, 
        IAuthenticatedUserService authenticatedUserService)
    {
        _context = context;
        _mapper = mapper;
        _authenticatedUserService = authenticatedUserService;
    }
    
    public async Task<Result<List<UserDto>>> GetAll()
    {
        if (!_authenticatedUserService.GetUserRoles().Contains(Roles.SuperAdmin))
        {
            var superAdminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.SuperAdmin);
            if (superAdminRole == null)
                return Result.BadRequest<List<UserDto>>("Your role is not valid");

            var users = await _context.Users
                .Where(u => u.UserRoles.Any(ur => ur.RoleId != superAdminRole.Id) || u.UserRoles.Count == 0)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .OrderBy(u => u.FirstName)
                .ToListAsync();

            return Result.Ok(users);
        }

        var data = await _context.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .OrderBy(u => u.FirstName)
            .ToListAsync();

        return Result.Ok(data);
    }

    public async Task<Result<UserDto>> GetById(string id)
    {
        if (_authenticatedUserService.GetUserId() != id &&
            !_authenticatedUserService.GetUserRoles().Contains(Roles.SuperAdmin))
            return Result.NotFound<UserDto>("User not found");

        var user = await _context.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user == null
            ? Result.NotFound<UserDto>("User not found")
            : Result.Ok(user);
    }
}