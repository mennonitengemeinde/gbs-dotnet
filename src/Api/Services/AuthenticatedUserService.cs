﻿using System.Security.Claims;
using Gbs.Application.Common.Interfaces.Services;

namespace Gbs.Api.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetUserId() =>
        _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    
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
}