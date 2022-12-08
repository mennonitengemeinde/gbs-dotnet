using Gbs.Application.Features.Subjects;

namespace Gbs.Tests.Application.UnitTests.Features.Subjects;

public class SubjectTestBase : GbsTestBase
{
    protected IMapper Mapper { get; set; }

    protected SubjectTestBase()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new SubjectMapping()); });
        Mapper = config.CreateMapper();
    }
}