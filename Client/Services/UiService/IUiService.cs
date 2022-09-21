

namespace gbs.Client.Services.UiService;

public interface IUiService
{
    List<AlertDto> Alerts { get; set; }
    event Action AlertsChanged;
    Task ShowErrorAlert(string message, int statusCode);
    void ShowSuccessAlert(string message);
    void RemoveAlert(string alertId);
}