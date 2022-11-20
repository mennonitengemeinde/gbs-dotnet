using Gbs.Application.Teachers;

namespace Gbs.Tests.Application.UnitTests.Teachers;

public class TeacherQueriesTests : GbsTestBase
{
    [Fact]
    public async Task GetAll_ReturnsAllTeachers()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new TeacherQueries(Context, mapper);
        
        var result = await q.GetAll();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }
    
    [Fact]
    public async Task GetById_ReturnsTeacher()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new TeacherQueries(Context, mapper);
        
        var result = await q.GetById(1);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }
}