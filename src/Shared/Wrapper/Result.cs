namespace Gbs.Shared.Wrapper;

public static class Result
{
    public static Result<T> Ok<T>(T data)
    {
        return new Result<T>(true, "Success", 200, data);
    }

    public static Result<T> Ok<T>(T data, string message)
    {
        return new Result<T>(true, message, 200, default!);
    }

    public static Result<T> BadRequest<T>(string message)
    {
        return new Result<T>(false, message, 400, default!);
    }

    public static Result<T> Forbidden<T>()
    {
        return new Result<T>(false, "You are not authorized to access this resource", 403, default!);
    }

    public static Result<T> InternalError<T>()
    {
        return new Result<T>(false, "Internal server error", 500, default!);
    }

    public static Result<T> NotFound<T>(string message)
    {
        return new Result<T>(false, message, 404, default!);
    }

    public static Result<T> Unauthorized<T>()
    {
        return new Result<T>(false, "Unauthorized", 401, default!);
    }
}

public class Result<T>
{
    public Result(bool success, string message, int statusCode)
    {
        Data = default!;
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }
    
    public Result(bool success, string message, int statusCode, T data)
    {
        Data = data;
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }

    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}