namespace Gbs.Wasm.Common.Interfaces;

public interface IUiService
{
    Task ShowErrorAlert(string? message, int statusCode = 400);
    void ShowSuccessAlert(string message);
}