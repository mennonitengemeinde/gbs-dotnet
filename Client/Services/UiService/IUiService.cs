

namespace gbs.Client.Services.UiService;

public interface IUiService
{
    public bool Loading { get; set; }
    event Action LoadingChanged;
    List<AlertDto> Alerts { get; set; }
    event Action AlertsChanged;
    Task ShowErrorAlert(string message, int statusCode = 400);
    void ShowSuccessAlert(string message);
    void LoadingStart();
    void LoadingStop();
}