using FluentValidation;
using Gbs.Application.Features.Teachers;

namespace Gbs.Tests.Application.UnitTests.Features.Teachers;

public class TeacherTestBase : GbsTestBase
{
    public TeacherValidator Validator { get; set; }

    protected TeacherTestBase()
    {
        Validator = new TeacherValidator(Context);
    }
}