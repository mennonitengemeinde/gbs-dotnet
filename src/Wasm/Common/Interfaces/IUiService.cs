namespace Gbs.Wasm.Common.Interfaces;

public interface IUiService
{
    event Action LoadingChanged;
    Task ShowErrorAlert(string message, int statusCode = 400);
    void ShowSuccessAlert(string message);
}