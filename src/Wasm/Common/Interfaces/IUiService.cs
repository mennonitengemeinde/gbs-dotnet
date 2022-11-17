namespace Gbs.Wasm.Common.Interfaces;

public interface IUiService
{
    public bool Loading { get; set; }
    event Action LoadingChanged;
    Task ShowErrorAlert(string message, int statusCode = 400);
    void ShowSuccessAlert(string message);
    void LoadingStart();
    void LoadingStop();
}