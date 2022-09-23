using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace gbs.Client.Services.UiService;

public class UiService : IUiService
{
    private readonly NavigationManager _navManager;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ISnackbar _snackbar;
    public List<AlertDto> Alerts { get; set; } = new List<AlertDto>();
    public event Action? AlertsChanged;

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
        // Alerts.Add(new AlertDto
        // {
        //     Message = message,
        //     Type = AlertType.Error
        // });
        // AlertsChanged?.Invoke();
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
        // Alerts.Add(new AlertDto
        // {
        //     Message = message,
        //     Type = AlertType.Success
        // });
        // AlertsChanged?.Invoke();
        _snackbar.Add(message, Severity.Success);
    }

    public void RemoveAlert(string alertId)
    {
        var alert = Alerts.Single(a => a.Id == alertId);
        Alerts.Remove(alert);
        AlertsChanged?.Invoke();
    }
}