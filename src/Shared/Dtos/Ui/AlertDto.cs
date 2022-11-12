using Gbs.Shared.Enums;

namespace gbs.Shared.Dtos.Ui;

public class AlertDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Message { get; set; } = string.Empty;
    public AlertType Type { get; set; } = AlertType.Info;
    public bool Dismissible { get; set; } = true;
    public bool AutoDismiss { get; set; } = true;
    public int AutoDismissDelay { get; set; } = 3000;
    
}