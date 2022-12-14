using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Gbs.Wasm;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _http;

    public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
    {
        _localStorageService = localStorageService;
        _http = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorageService.GetItemAsStringAsync("authToken");
        ClaimsIdentity? identity = null;

        if (!string.IsNullOrWhiteSpace(savedToken))
        {
            try
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(savedToken), "jwt");
                int.TryParse(identity.Claims.First(c => c.Type == "exp").Value, out var exp);
                if (exp < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                {
                    identity = new ClaimsIdentity();
                    await _localStorageService.RemoveItemAsync("authToken");
                    // savedToken = string.Empty;
                }
                else
                {
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", savedToken.Replace("\"", ""));
                }
            }
            catch (Exception)
            {
                await _localStorageService.RemoveItemAsync("authToken");
                identity = new ClaimsIdentity();
            }
        }
        
        identity ??= new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        
        return state;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs!.TryGetValue("role", out object? roles);
        if (roles != null)
        {
            if (roles.ToString()!.Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);

                claims.AddRange(parsedRoles!.Select(parsedRole => new Claim("role", parsedRole)));
            }
            else
            {
                claims.Add(new Claim("role", roles.ToString()!));
            }

            keyValuePairs.Remove("role");
        }
        
        keyValuePairs.TryGetValue("church_id", out var churchId);
        if (churchId != null)
        {
            claims.Add(new Claim("church_id", churchId.ToString()!));
            keyValuePairs.Remove("church_id");
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

        return claims;
    }
}