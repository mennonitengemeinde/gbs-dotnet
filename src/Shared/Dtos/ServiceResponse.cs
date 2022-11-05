namespace gbs.Shared.Dtos;

public class ServiceResponse<T>
{
    public static ServiceResponse<T> BadRequest(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = message,
            StatusCode = 400
        };
    }

    public static ServiceResponse<T> Ok(T data, string message = "Success")
    {
        return new ServiceResponse<T>
        {
            Message = message,
            Data = data
        };
    }

    public T Data { get; set; } = default!;
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; } = 200;
}