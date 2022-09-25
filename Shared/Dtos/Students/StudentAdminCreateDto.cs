using System.ComponentModel.DataAnnotations;

namespace gbs.Shared.Dtos.Students;

public class StudentAdminCreateDto : StudentCreateDto
{
    [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a church")]
    public new int? ChurchId { get; set; }
}