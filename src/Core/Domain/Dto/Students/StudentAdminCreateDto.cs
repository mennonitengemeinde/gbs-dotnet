using System.ComponentModel.DataAnnotations;

namespace gbs.Shared.Dtos.Students;

public class StudentAdminCreateDto : StudentBaseCreateDto
{
    [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a church")]
    public override int? ChurchId { get; set; }
}