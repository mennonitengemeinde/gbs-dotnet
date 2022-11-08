namespace gbs.Core.Shared.Wrapper;

public static class Result
{
    public static Result<T> Ok<T>(T data)
    {
        return new Result<T>(data, true, "Success", 200);
    }

    public static Result<T> Ok<T>(T data, string message)
    {
        return new Result<T>(data, true, message, 200);
    }

    public static Result<T> BadRequest<T>(string message)
    {
        return new Result<T>(default!, false, message, 400);
    }

    public static Result<T> Forbidden<T>()
    {
        return new Result<T>(default!, false, "You are not authorized to access this resource", 403);
    }

    public static Result<T> InternalError<T>()
    {
        return new Result<T>(default!, false, "Internal server error", 500);
    }

    public static Result<T> NotFound<T>(string message)
    {
        return new Result<T>(default!, false, message, 404);
    }
    
    public static Result<T> Unauthorized<T>()
    {
        return new Result<T>(default!, false, "Unauthorized", 401);
    }
}

public class Result<T>
{
    public Result(T data, bool success, string message, int statusCode)
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