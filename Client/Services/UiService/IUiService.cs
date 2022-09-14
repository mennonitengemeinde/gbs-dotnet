

namespace gbs.Client.Services.UiService;

public interface IUiService
{
    List<AlertDto> Alerts { get; set; }
    event Action AlertsChanged;
    void AddErrorAlert(string message);
    void AddSuccessAlert(string message);
    void RemoveAlert(string alertId);
}