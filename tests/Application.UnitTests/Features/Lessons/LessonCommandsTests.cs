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
        Assert.NotNull(result.Errors);
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

    public async Task Delete_DeletesEntity()
    {
        throw new NotImplementedException();
    }

    public async Task Delete_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        throw new NotImplementedException();
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
}