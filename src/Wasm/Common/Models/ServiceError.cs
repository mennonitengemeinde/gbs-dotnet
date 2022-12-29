namespace Gbs.Wasm.Common.Models;

public class ServiceError
{
    public ServiceError(string message, string[]? errors, int statusCode = 400)
    {
        Message = message;
        Errors = errors;
        StatusCode = statusCode;
    }
    public string Message { get; set; }
    public string[]? Errors { get; set; }
    public int StatusCode { get; set; }
}