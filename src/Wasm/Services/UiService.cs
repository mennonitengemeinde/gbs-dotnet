using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Gbs.Wasm.Services;

public class UiService : IUiService
{
    private readonly NavigationManager _navManager;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ISnackbar _snackbar;

    public UiService(NavigationManager navManager, ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider, ISnackbar snackbar)
    {
        _navManager = navManager;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
        _snackbar = snackbar;
    }

    public async Task ShowErrorAlert(string? message, int statusCode = 400)
    {
        _snackbar.Add(message ?? "Something went wrong!", Severity.Error);
        if (statusCode == 401)
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navManager.NavigateTo("");
        }
    }

    public void ShowSuccessAlert(string message)
    {
        _snackbar.Add(message, Severity.Success);
    }
}