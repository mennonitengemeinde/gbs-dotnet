namespace Gbs.Tests.Application.UnitTests.Features;

public interface ICommandTests
{
    Task Add_AddsNewEntity();
    Task Add_ReturnValidationError_WhenEntityAlreadyExists();
    Task Update_UpdatesEntity();
    Task Update_ReturnsNotFound_WhenEntityDoesNotExist();
    Task Update_ReturnValidationError_WhenEntityAlreadyExists();
    Task Delete_DeletesEntity();
    Task Delete_ReturnsNotFound_WhenEntityDoesNotExist();
}