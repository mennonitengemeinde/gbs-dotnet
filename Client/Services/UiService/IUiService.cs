

namespace gbs.Client.Services.UiService;

public interface IUiService
{
    List<AlertDto> Alerts { get; set; }
    event Action AlertsChanged;
    void ShowErrorAlert(string message);
    void ShowSuccessAlert(string message);
    void RemoveAlert(string alertId);
}