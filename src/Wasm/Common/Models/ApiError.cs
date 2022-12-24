namespace Gbs.Wasm.Common.Models;

public class ApiError
{
    public string Message { get; set; } = string.Empty;
    public string[]? Errors { get; set; }
    public int StatusCode { get; set; }
}