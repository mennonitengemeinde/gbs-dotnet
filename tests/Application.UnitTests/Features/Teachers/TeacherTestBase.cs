using Gbs.Application.Features.Teachers;

namespace Gbs.Tests.Application.UnitTests.Features.Teachers;

public class TeacherTestBase : GbsTestBase
{
    protected IMapper Mapper { get; set; }
    
    protected TeacherTestBase()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new TeacherMapping()); });
        Mapper = config.CreateMapper();
    }
}