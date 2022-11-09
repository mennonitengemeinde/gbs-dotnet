using gbs.Core.Shared.Const;
using Gbs.Server.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Gbs.Server.Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public UserRepository(
        DataContext context,
        UserManager<User> userManager,
        IAuthenticatedUserService authenticatedUserService
    )
    {
        _context = context;
        _userManager = userManager;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<Result<List<UserDto>>> GetUsers()
    {
        if (!_authenticatedUserService.GetUserRoles().Contains(Roles.SuperAdmin))
        {
            var superAdminRoleId = await _context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.SuperAdmin);
            if (superAdminRoleId == null)
                return Result.BadRequest<List<UserDto>>("Your role is not valid");

            var users = await _context.Users
                .Where(u => u.UserRoles.Any(ur => ur.RoleId != superAdminRoleId.Id))
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
                    ChurchName = u.Church!.Name,
                })
                .OrderBy(u => u.FirstName)
                .ToListAsync();

            return Result.Ok(users);
        }

        var data = await _context.Users
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
                ChurchName = u.Church!.Name,
            })
            .OrderBy(u => u.FirstName)
            .ToListAsync();

        return Result.Ok(data);
    }

    public async Task<Result<UserDto>> GetUserById(string userId)
    {
        if (_authenticatedUserService.GetUserId() != userId &&
            !_authenticatedUserService.GetUserRoles().Contains(Roles.SuperAdmin))
            return Result.NotFound<UserDto>("User not found");

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
            ChurchName = u.Church!.Name,
        }).FirstOrDefaultAsync(u => u.Id == userId);

        return user == null
            ? Result.NotFound<UserDto>("User not found")
            : Result.Ok(user);
    }

    public async Task<Result<List<UserDto>>> UpdateUserRole(string userId, List<string> newRoles)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return Result.NotFound<List<UserDto>>("User not found");

        var userRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = userRoles.Except(newRoles).ToList();
        if (rolesToRemove.Any())
        {
            var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!result.Succeeded)
                return Result.BadRequest<List<UserDto>>("Failed to remove roles");
        }

        var rolesToAdd = newRoles.Except(userRoles).ToList();
        if (!rolesToAdd.Any()) return await GetUsers();
        {
            var result = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (result.Succeeded) return await GetUsers();
            return Result.BadRequest<List<UserDto>>("Failed to add roles");
        }
    }

    public async Task<Result<List<UserDto>>> UpdateUserActiveState(string userId, bool newActiveState)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return Result.BadRequest<List<UserDto>>("User not found");

        user.IsActive = newActiveState;
        await _context.SaveChangesAsync();
        return await GetUsers();
    }

    public async Task<Result<List<UserDto>>> UpdateUserChurch(string userId, UserUpdateChurchDto updateDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return Result.NotFound<List<UserDto>>("User not found");

        user.ChurchId = updateDto.ChurchId;
        await _context.SaveChangesAsync();
        return await GetUsers();
    }
}