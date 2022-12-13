using FluentValidation.Results;

namespace Gbs.Shared.Common.Wrapper;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public string[]? Errors { get; set; }

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

    public static Result<T> BadRequest<T>(string message, string[]? errors = null)
    {
        return new Result<T> { Success = false, Message = message, StatusCode = 400 };
    }

    public static Result<T> Forbidden<T>()
    {
        return new Result<T>
            { Success = false, Message = "You are not authorized to access this resource", StatusCode = 403 };
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

    public static Result<T> ValidationError<T>(ValidationResult errors)
    {
        var errArray = errors.Errors.Select(x => x.ErrorMessage).ToArray();
        return new Result<T>
        {
            Success = false, Message = "One or more validation errors occurred.", StatusCode = 422, Errors = errArray
        };
    }
}

public class Result<T> : Result
{
    public T? Data { get; set; }
}