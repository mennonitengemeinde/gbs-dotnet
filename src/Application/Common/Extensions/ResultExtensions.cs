using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gbs.Application.Common.Extensions;

public static class ResultExtensions
{
    public static ActionResult<Result<T>> ToActionResult<T>(this Result<T> result)
    {
        if (result.Success)
        {
            return new OkObjectResult(result);
        }

        switch (result.StatusCode)
        {
            case 400:
                return new BadRequestObjectResult(result);
            case 404:
                return new NotFoundObjectResult(result);
            case 422:
                return new UnprocessableEntityObjectResult(result);
            case 500:
                return new ObjectResult(result) { StatusCode = StatusCodes.Status500InternalServerError };
            default:
                return new ObjectResult(result) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    public static Result<TOut> Parse<TIn, TOut>(this Result<TIn> result)
    {
        return new Result<TOut> { Success = result.Success, Message = result.Message, StatusCode = result.StatusCode };
    }
}