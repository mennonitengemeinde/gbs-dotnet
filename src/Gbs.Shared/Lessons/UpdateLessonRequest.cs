using System.ComponentModel.DataAnnotations;
using Gbs.Shared.Common.Enums;

namespace Gbs.Shared.Lessons;

public class UpdateLessonRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? VideoUrl { get; set; }
    public Visibility IsVisible { get; set; }
    public int GenerationId { get; set; }
    public int? SubjectId { get; set; }
    public int TeacherId { get; set; }
}

public class UpdateLessonRequestValidator : AbstractValidator<UpdateLessonRequest>
{
    public UpdateLessonRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long");
        
        RuleFor(x => x.IsVisible)
            .NotEmpty();
        
        RuleFor(x => x.GenerationId)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(x => x.TeacherId)
            .NotEmpty()
            .GreaterThan(0);
    }
}