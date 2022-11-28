using Gbs.Application.Common.Interfaces.Services;
using Gbs.Domain.Common.Wrapper;

namespace Gbs.Application.Identity;

public class IdentityQueries : IIdentityQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public IdentityQueries(
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

    public async Task<Result<List<RolesDto>>> GetRoles()
    {
        var userRoles = _authenticatedUserService.GetUserRoles();
        List<RolesDto> roles;
        if (userRoles.Contains(Roles.SuperAdmin))
        {
            roles = await _context.Roles.Select(x => new RolesDto
            {
                Id = x.Id,
                Name = x.Name!,
                NormalizedName = x.NormalizedName!
            }).ToListAsync();
        }
        else
        {
            roles = await _context.Roles.Where(x => x.Name != Roles.SuperAdmin).Select(x => new RolesDto
            {
                Id = x.Id,
                Name = x.Name!,
                NormalizedName = x.NormalizedName!
            }).ToListAsync();
        }

        return Result.Ok(roles);
    }
}