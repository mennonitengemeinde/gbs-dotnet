using Gbs.Application.Features.Lessons;
using Gbs.Shared.Common.Enums;
using Gbs.Shared.Lessons;

namespace Gbs.Tests.Application.UnitTests.Features.Lessons;

public class LessonCommandsTests : LessonTestBase
{
    [Fact]
    public async Task Add_AddsLesson()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, CreateLessonValidator, UpdateLessonValidator);
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
    public async Task Add_ReturnsValidationError_WhenNameAlreadyExists()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, CreateLessonValidator, UpdateLessonValidator);
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
    public async Task Add_AddsLesson_WhenItsTheFirstLesson()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, CreateLessonValidator, UpdateLessonValidator);
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
    public async Task Update_UpdatesLesson()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, CreateLessonValidator, UpdateLessonValidator);
        var lesson = new UpdateLessonRequest
            { Id = 1, Name = "Test Lesson", IsVisible = Visibility.Visible, GenerationId = 1, TeacherId = 1 };
        // Act
        var result = await cmd.Update(lesson);
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
    public async Task Update_ReturnsValidationError_WhenNameAlreadyExists()
    {
        // Arrange
        var cmd = new LessonCommands(Context, Mapper, CreateLessonValidator, UpdateLessonValidator);
        var lesson = new UpdateLessonRequest
            { Id = 1, Name = "Lesson 2", IsVisible = Visibility.Visible, GenerationId = 1, TeacherId = 1 };
        // Act
        var result = await cmd.Update(lesson);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.NotNull(result.Errors);
        Assert.Null(result.Data);
    }
}