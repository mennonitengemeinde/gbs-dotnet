using System.ComponentModel.DataAnnotations;
using Gbs.Shared.Common.Enums;

namespace Gbs.Shared.Students;

public class CreateStudentRequest
{
    [Display(Name = "First Name")] public string FirstName { get; set; } = string.Empty;
    [Display(Name = "Last Name")] public string LastName { get; set; } = string.Empty;
    [Display(Name = "Date of Birth")] public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string City { get; set; } = string.Empty;
    [Display(Name = "Province/State")] public string Province { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    [Display(Name = "Marital Status")] public MaritalStatus MaritalStatus { get; set; }
    public string? Email { get; set; }
    [Display(Name = "Country Code")] public string? PhoneCodeId { get; set; }
    public string? Phone { get; set; }
    [Display(Name = "Status")] public EnrollmentState Status { get; set; }
    public string? Testimony { get; set; }
    public bool AgreedToGbsConcept { get; set; } = false;
    public int ChurchId { get; set; }
    public int GenerationId { get; set; }
}

public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(3, 100).WithMessage("First name must be between 3 and 100 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(3, 100).WithMessage("Last name must be between 3 and 100 characters");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required");

        RuleFor(x => x.Address)
            .Length(3, 100).WithMessage("Address must be between 3 and 100 characters");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .Length(3, 50).WithMessage("City must be between 3 and 50 characters");

        RuleFor(x => x.Province)
            .NotEmpty().WithMessage("Province is required")
            .Length(3, 50).WithMessage("Province must be between 3 and 50 characters");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .Length(3, 50).WithMessage("Country must be between 3 and 50 characters");

        RuleFor(x => x.PostalCode)
            .Must(x =>
            {
                if (string.IsNullOrEmpty(x))
                    return true;
                return x.Length is > 3 and < 10;
            }).WithMessage("Postal code must be between 3 and 10 characters");

        RuleFor(x => x.MaritalStatus)
            .NotEmpty().WithMessage("Marital status is required");

        RuleFor(x => x.Email)
            .EmailAddress()
            .Length(3, 100).WithMessage("Email must be between 3 and 100 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");

        RuleFor(x => x.Phone)
            .Length(3, 50).WithMessage("Phone must be between 3 and 50 characters");

        RuleFor(x => x.AgreedToGbsConcept)
            .Equal(true).WithMessage("You must agree to the GBS concept");

        RuleFor(x => x.ChurchId)
            .NotEmpty().WithMessage("Church is required")
            .GreaterThan(0).WithMessage("Church is required");

        RuleFor(x => x.GenerationId)
            .NotEmpty().WithMessage("Generation is required")
            .GreaterThan(0).WithMessage("Generation is required");
    }
}