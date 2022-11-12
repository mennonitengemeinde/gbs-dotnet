namespace Gbs.Shared.Wrapper;

public static class Result
{
    public static Result<T> Ok<T>(T data)
    {
        return new Result<T> { Success = true, Message = "Success", StatusCode = 200, Data = data };
    }

    public static Result<T> Ok<T>(T data, string message)
    {
        return new Result<T> { Success = true, Message = message, StatusCode = 200, Data = data };
    }

    public static Result<T> OkNoContent<T>(string message)
    {
        return new Result<T> { Success = true, Message = message, StatusCode = 200 };
    }

    public static Result<T> BadRequest<T>(string message)
    {

        return new Result<T>{Success = false, Message = message, StatusCode = 400};
    }

    public static Result<T> Forbidden<T>()
    {
        return new Result<T> { Success = false, Message = "You are not authorized to access this resource", StatusCode = 403 };
    }

    public static Result<T> InternalError<T>()
    {
        return new Result<T> { Success = false, Message = "Internal server error", StatusCode = 500 };
    }

    public static Result<T> NotFound<T>(string message)
    {
        return new Result<T> { Success = false, Message = message, StatusCode = 404 };
    }

    public static Result<T> Unauthorized<T>()
    {
        return new Result<T> { Success = false, Message = "Unauthorized", StatusCode = 401 };
    }
}

public class Result<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}