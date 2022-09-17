namespace gbs.Client.Services.UiService;

public class UiService : IUiService
{
    public List<AlertDto> Alerts { get; set; } = new List<AlertDto>();
    public event Action? AlertsChanged;

    public void AddErrorAlert(string message)
    {
        Alerts.Add(new AlertDto
        {
            Message = message,
            Type = AlertType.Error
        });
        AlertsChanged?.Invoke();
    }
    
    public void AddSuccessAlert(string message)
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