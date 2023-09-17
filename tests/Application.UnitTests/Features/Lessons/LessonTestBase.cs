using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Lessons;
using NSubstitute;

namespace Gbs.Tests.Application.UnitTests.Features.Lessons;

public class LessonTestBase : GbsTestBase
{
    protected LessonTestBase()
    {
        Validator = new LessonValidator(Context);
        var authedUserService = Substitute.For<IAuthenticatedUserService>();
        authedUserService.GetUserId().Returns("superAdmin");
        AuthenticatedUserService = authedUserService;
    }

    protected LessonValidator Validator { get; }
    protected IAuthenticatedUserService AuthenticatedUserService { get; }
}