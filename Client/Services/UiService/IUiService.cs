

namespace gbs.Client.Services.UiService;

public interface IUiService
{
    List<AlertDto> Alerts { get; set; }
    event Action AlertsChanged;
    void AddAlert(AlertDto alert);
    void RemoveAlert(string alertId);
}