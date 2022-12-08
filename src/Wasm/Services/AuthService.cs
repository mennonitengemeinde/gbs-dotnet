using System.Security.Claims;
using Gbs.Shared.Identity;
using Microsoft.AspNetCore.Components.Authorization;

namespace Gbs.Wasm.Services;

public class AuthApiService : IAuthService
{
    private readonly HttpClient _http;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthApiService(HttpClient http, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _authStateProvider = authStateProvider;
    }

    public async Task<Result<string>> Login(LoginRequest userLogin)
    {
        return await _http.PostAsJsonAsync("api/auth/login", userLogin)
            .EnsureSuccess<string>();
    }

    public async Task<Result<List<RolesResponse>>> FetchRoles()
    {
        return await _http.GetAsync("api/auth/roles")
            .EnsureSuccess<List<RolesResponse>>();
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity!.IsAuthenticated;
    }

    public async Task<List<string>> GetUserRoles()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var roles = authState.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
        return roles;
    }

    public async Task<bool> UserIsAdmin()
    {
        var roles = await GetUserRoles();
        return roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin);
    }
}