using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Identity.Interfaces;

namespace Gbs.Application.Features.Identity;

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
    
    public async Task<Result<List<UserResponse>>> GetAll()
    {
        if (!_authenticatedUserService.GetUserRoles().Contains(Roles.SuperAdmin))
        {
            var superAdminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.SuperAdmin);
            if (superAdminRole == null)
                return Result.BadRequest<List<UserResponse>>("Your role is not valid");

            var users = await _context.Users
                .Where(u => u.UserRoles.Any(ur => ur.RoleId != superAdminRole.Id) || u.UserRoles.Count == 0)
                .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
                .OrderBy(u => u.FirstName)
                .ToListAsync();

            return Result.Ok(users);
        }

        var data = await _context.Users
            .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
            .OrderBy(u => u.FirstName)
            .ToListAsync();

        return Result.Ok(data);
    }

    public async Task<Result<UserResponse>> GetById(string id)
    {
        if (_authenticatedUserService.GetUserId() != id &&
            !_authenticatedUserService.GetUserRoles().Contains(Roles.SuperAdmin))
            return Result.NotFound<UserResponse>("User not found");

        var user = await _context.Users
            .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user == null
            ? Result.NotFound<UserResponse>("User not found")
            : Result.Ok(user);
    }

    public async Task<Result<List<RolesResponse>>> GetRoles()
    {
        var userRoles = _authenticatedUserService.GetUserRoles();
        List<RolesResponse> roles;
        if (userRoles.Contains(Roles.SuperAdmin))
        {
            roles = await _context.Roles.Select(x => new RolesResponse
            {
                Id = x.Id,
                Name = x.Name!,
                NormalizedName = x.NormalizedName!
            }).ToListAsync();
        }
        else
        {
            roles = await _context.Roles.Where(x => x.Name != Roles.SuperAdmin).Select(x => new RolesResponse
            {
                Id = x.Id,
                Name = x.Name!,
                NormalizedName = x.NormalizedName!
            }).ToListAsync();
        }

        return Result.Ok(roles);
    }
}