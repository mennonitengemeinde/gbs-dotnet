using System.ComponentModel.DataAnnotations;
using gbs.Shared.Enums;

namespace gbs.Shared.Dtos.Students;

public class StudentCreateDto : StudentBaseCreateDto
{
    public override int? ChurchId { get; set; }
}