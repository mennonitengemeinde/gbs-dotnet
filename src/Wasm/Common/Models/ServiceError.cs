namespace Gbs.Wasm.Common.Models;

public class ServiceError
{
    public string Message { get; set; } = string.Empty;
    public string[]? Errors { get; set; }
    public int StatusCode { get; set; }
}