namespace gbs.Client.Services.UiService;

public class UiService : IUiService
{
    public List<AlertDto> Alerts { get; set; } = new List<AlertDto>();
    public event Action? AlertsChanged;

    public void ShowErrorAlert(string message)
    {
        Alerts.Add(new AlertDto
        {
            Message = message,
            Type = AlertType.Error
        });
        AlertsChanged?.Invoke();
    }
    
    public void ShowSuccessAlert(string message)
    {
        Alerts.Add(new AlertDto
        {
            Message = message,
            Type = AlertType.Success
        });
        AlertsChanged?.Invoke();
    }

    public void RemoveAlert(string alertId)
    {
        var alert = Alerts.Single(a => a.Id == alertId);
        Alerts.Remove(alert);
        AlertsChanged?.Invoke();
    }
}