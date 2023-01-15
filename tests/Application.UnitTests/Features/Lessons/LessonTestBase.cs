using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Lessons;
using Moq;

namespace Gbs.Tests.Application.UnitTests.Features.Lessons;

public class LessonTestBase : GbsTestBase
{
    protected LessonTestBase()
    {
        Validator = new LessonValidator(Context);
        var authedUserService = new Mock<IAuthenticatedUserService>();
        authedUserService.Setup(x => x.GetUserId()).Returns("superAdmin");
        AuthenticatedUserService = authedUserService.Object;
    }

    protected LessonValidator Validator { get; }
    protected IAuthenticatedUserService AuthenticatedUserService { get; }
}