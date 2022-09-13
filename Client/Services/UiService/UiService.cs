namespace gbs.Client.Services.UiService;

public class UiService : IUiService
{
    public List<AlertDto> Alerts { get; set; } = new List<AlertDto>();
    public event Action? AlertsChanged;

    public void AddAlert(AlertDto alert)
    {
        Console.WriteLine($"Adding alert {alert.Id} type {alert.Type}");
        Alerts.Add(alert);
        AlertsChanged?.Invoke();
    }

    public void RemoveAlert(string alertId)
    {
        var alert = Alerts.Single(a => a.Id == alertId);
        Console.WriteLine($"Removing alert {alertId} type {alert.Type}");
        Alerts.Remove(alert);
        Console.WriteLine($"Alert removed {Alerts.Count}");
        AlertsChanged?.Invoke();
    }
}