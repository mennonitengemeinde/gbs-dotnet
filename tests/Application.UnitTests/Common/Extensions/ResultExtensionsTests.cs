using Gbs.Application.Common.Extensions;
using Gbs.Shared.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gbs.Tests.Application.UnitTests.Common.Extensions;

public class ResultExtensionsTests
{
    [Fact]
    public void ToActionResult_ReturnsOkObjectResult()
    {
        // Arrange
        var ok = Result.Ok(true);

        // Act
        var result = ok.ToActionResult();

        // Assert
        Assert.Equal(typeof(ActionResult<Result<bool>>), result.GetType());
        Assert.Equal(typeof(OkObjectResult), result.Result.GetType());
    }

    [Fact]
    public void ToActionResult_ReturnsBadRequestObjectResult()
    {
        // Arrange
        var error = Result.BadRequest<bool>("Bad Request");

        // Act
        var result = error.ToActionResult();

        // Assert
        Assert.Equal(typeof(ActionResult<Result<bool>>), result.GetType());
        Assert.Equal(typeof(BadRequestObjectResult), result.Result.GetType());
    }

    [Fact]
    public void ToActionResult_ReturnsNotFoundObjectResult()
    {
        // Arrange
        var error = Result.NotFound<bool>("Not Found");

        // Act
        var result = error.ToActionResult();

        // Assert
        Assert.Equal(typeof(ActionResult<Result<bool>>), result.GetType());
        Assert.Equal(typeof(NotFoundObjectResult), result.Result.GetType());
    }

    [Fact]
    public void ToActionResult_ReturnsStatus500InternalServerError()
    {
        // Arrange
        var error = Result.InternalError<bool>();

        // Act
        var result = error.ToActionResult();

        // Assert
        Assert.Equal(typeof(ActionResult<Result<bool>>), result.GetType());
        Assert.Equal(typeof(ObjectResult), result.Result.GetType());
    }

    [Fact]
    public void ToActionResult_ReturnsStatusUnknown500InternalServerError()
    {
        // Arrange
        var error = new Result<bool> { Data = true, Message = "Unknown Error", Success = false, StatusCode = 300 };

        // Act
        var result = error.ToActionResult();

        // Assert
        Assert.Equal(typeof(ActionResult<Result<bool>>), result.GetType());
        Assert.Equal(typeof(ObjectResult), result.Result.GetType());
    }

    [Fact]
    public void Parse_Success()
    {
        // Arrange
        var result = Result.BadRequest<int>("Bad Request");

        // Act
        var parsedResult = result.Parse<int, string>();

        // Assert
        Assert.Equal(typeof(Result<string>), parsedResult.GetType());
    }
}