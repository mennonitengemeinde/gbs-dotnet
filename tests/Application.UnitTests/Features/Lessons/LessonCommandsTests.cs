using Gbs.Application.Features.Lessons;
using Gbs.Shared.Common.Enums;
using Gbs.Shared.Lessons;

namespace Gbs.Tests.Application.UnitTests.Features.Lessons;

public class LessonCommandsTests : LessonTestBase, ICommandTests
{
    [Fact]
    public async Task Add_AddsNewEntity()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        var lesson = new CreateLessonRequest
            { Name = "Test Lesson", IsVisible = Visibility.Visible, GenerationId = 1, TeacherId = 1 };
        // Act
        var result = await cmd.Add(lesson);
        var ctxCount = Context.Lessons.Count();
        // Assert
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(lesson.Name, result.Data.Name);
        Assert.Equal(6, result.Data.Order);
        Assert.Equal(6, ctxCount);
    }

    [Fact]
    public async Task Add_ReturnValidationError_WhenEntityAlreadyExists()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        var lesson = new CreateLessonRequest
            { Name = "Lesson 1", IsVisible = Visibility.Visible, GenerationId = 1, TeacherId = 1 };
        // Act
        var result = await cmd.Add(lesson);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.NotNull(result.Errors);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Update_UpdatesEntity()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        var lesson = new CreateLessonRequest
            { Name = "Test Lesson", IsVisible = Visibility.Visible, GenerationId = 1, TeacherId = 1 };
        // Act
        var result = await cmd.Update(1, lesson);
        var ctxCount = Context.Lessons.Count();
        // Assert
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(lesson.Name, result.Data.Name);
        Assert.Equal(1, result.Data.Order);
        Assert.Equal(5, ctxCount);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        var lesson = new CreateLessonRequest
            { Name = "Test Lesson", IsVisible = Visibility.Visible, GenerationId = 1, TeacherId = 1 };

        // Act
        var result = await cmd.Update(6, lesson);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Update_ReturnValidationError_WhenEntityAlreadyExists()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        var lesson = new CreateLessonRequest
            { Name = "Lesson 2", IsVisible = Visibility.Visible, GenerationId = 1, TeacherId = 1 };
        // Act
        var result = await cmd.Update(1, lesson);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.NotNull(result.Errors);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Delete_DeletesEntity()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.Delete(1);
        var ctxCount = Context.Lessons.Count();
        // Assert
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(4, ctxCount);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.Delete(6);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task Add_AddsLesson_WhenItsTheFirstLesson()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        var lesson = new CreateLessonRequest
            { Name = "Test Lesson", IsVisible = Visibility.Visible, GenerationId = 2, TeacherId = 1 };
        var lessons = await Context.Lessons.ToListAsync();
        Context.Lessons.RemoveRange(lessons);
        // Act
        var result = await cmd.Add(lesson);
        var ctxCount = Context.Lessons.Count();
        // Assert
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(lesson.Name, result.Data.Name);
        Assert.Equal(1, result.Data.Order);
        Assert.Equal(1, ctxCount);
    }
    
    [Fact]
    public async Task UpdateOrder_UpdatesOrder()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.UpdateOrder(1, 2);
        var ctxCount = Context.Lessons.Count();
        // Assert
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Order);
        Assert.Equal(1, Context.Lessons.FirstOrDefault(x => x.Id == 2)?.Order);
        Assert.Equal(5, ctxCount);
    }
    
    [Fact]
    public async Task UpdateOrder_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.UpdateOrder(6, 2);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
    
    [Fact]
    public async Task UpdateOrder_Returns_WhenOrderIsTheSame()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.UpdateOrder(1, 1);
        // Assert
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
    }
}