using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace gbs.Client.Wasm.Services.UiService;

public class UiService : IUiService
{
    private readonly NavigationManager _navManager;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ISnackbar _snackbar;

    public bool Loading { get; set; }
    public event Action? LoadingChanged;

    public UiService(NavigationManager navManager, ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider, ISnackbar snackbar)
    {
        _navManager = navManager;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
        _snackbar = snackbar;
    }

    public async Task ShowErrorAlert(string message, int statusCode = 400)
    {
        _snackbar.Add(message, Severity.Error);
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

    public void LoadingStart()
    {
        Loading = true;
        LoadingChanged?.Invoke();
    }

    public void LoadingStop()
    {
        Loading = false;
        LoadingChanged?.Invoke();
    }
}