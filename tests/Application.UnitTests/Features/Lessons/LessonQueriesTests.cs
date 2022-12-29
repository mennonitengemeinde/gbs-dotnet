using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Lessons;
using Gbs.Tests.Application.UnitTests.Common;
using Moq;

namespace Gbs.Tests.Application.UnitTests.Features.Lessons;

public class LessonQueriesTests : LessonTestBase, IQueryTests
{
    [Fact]
    public async Task GetAll_ReturnsAllRecords()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lessons = await q.GetAll(null);

        // Assert
        Assert.True(lessons.Success);
        Assert.Equal(3, lessons.Data.Count);
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoRecords()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        Context.Lessons.RemoveRange(Context.Lessons);
        await Context.SaveChangesAsync();
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lessons = await q.GetAll(null);

        // Assert
        Assert.True(lessons.Success);
        Assert.Empty(lessons.Data!);
    }
    
    [Fact]
    public async Task GetAll_ReturnsForbidden_WhenUserIsNotAdmin()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        Context.Lessons.RemoveRange(Context.Lessons);
        await Context.SaveChangesAsync();
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lessons = await q.GetAll("all");

        // Assert
        Assert.False(lessons.Success);
        Assert.Null(lessons.Data);
    }

    [Fact]
    public async Task GetById_ReturnsRecord()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lesson = await q.GetById(1);

        // Assert
        Assert.True(lesson.Success);
        Assert.Equal(1, lesson.Data.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenNoRecord()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lesson = await q.GetById(99);

        // Assert
        Assert.False(lesson.Success);
        Assert.Null(lesson.Data);
    }

    [Fact]
    public async Task GetById_ReturnsRecord_WhenRecordIsHidden()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lesson = await q.GetById(5);

        // Assert
        Assert.True(lesson.Success);
        Assert.Equal(5, lesson.Data.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenRecordIsPrivate()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lesson = await q.GetById(4);

        // Assert
        Assert.False(lesson.Success);
        Assert.Null(lesson.Data);
    }

    [Fact]
    public async Task GetAll_ReturnsAllRecordsIncludeHidden()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(false);
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lessons = await q.GetAll("hidden");

        // Assert
        Assert.True(lessons.Success);
        Assert.Equal(4, lessons.Data.Count);
    }

    [Fact]
    public async Task GetAll_ReturnsAllRecordsIncludePrivate()
    {
        // Arrange
        var mock = new Mock<IAuthenticatedUserService>();
        mock.Setup(x => x.UserIsAdmin()).Returns(true);
        var q = new LessonQueries(Context, Mapper, mock.Object);

        // Act
        var lessons = await q.GetAll("all");

        // Assert
        Assert.True(lessons.Success);
        Assert.Equal(5, lessons.Data.Count);
    }
}