using Gbs.Application.Features.Subjects;
using Gbs.Shared.Subjects;

namespace Gbs.Tests.Application.UnitTests.Features.Subjects;

public class SubjectCommandsTests : SubjectTestBase
{
    [Fact]
    public async Task Add_AddsSubject()
    {
        var cmd = new SubjectCommands(Context, Mapper);
        var subject = new CreateSubjectRequest { Name = "Subject 4" };

        var result = await cmd.Add(subject);
        var ctxCount = Context.Subjects.Count();

        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(subject.Name, result.Data.Name);
        Assert.Equal(4, ctxCount);
    }

    [Fact]
    public async Task Add_ReturnsValidationError_WhenNameAlreadyExists()
    {
        var cmd = new SubjectCommands(Context, Mapper);
        var subject = new CreateSubjectRequest { Name = "Subject 1" };

        var result = await cmd.Add(subject);
        var ctxCount = Context.Subjects.Count();

        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.Equal(3, ctxCount);
    }

    [Fact]
    public async Task Update_UpdatesSubject()
    {
        var cmd = new SubjectCommands(Context, Mapper);
        var subject = new UpdateSubjectRequest { Name = "Subject 1 Updated" };

        var result = await cmd.Update(1, subject);
        var ctxCount = Context.Subjects.Count();

        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(subject.Name, result.Data.Name);
        Assert.Equal(3, ctxCount);
    }
    
    [Fact]
    public async Task Update_ReturnsValidationError_WhenNameAlreadyExists()
    {
        var cmd = new SubjectCommands(Context, Mapper);
        var subject = new UpdateSubjectRequest { Name = "Subject 2" };

        var result = await cmd.Update(1, subject);
        var ctxCount = Context.Subjects.Count();

        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.Equal(3, ctxCount);
    }
    
    [Fact]
    public async Task Update_ReturnsNotFound_WhenSubjectDoesNotExist()
    {
        var cmd = new SubjectCommands(Context, Mapper);
        var subject = new UpdateSubjectRequest { Name = "Subject 1 Updated" };

        var result = await cmd.Update(4, subject);
        var ctxCount = Context.Subjects.Count();

        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(3, ctxCount);
    }
}