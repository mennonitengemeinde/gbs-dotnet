using System.ComponentModel.DataAnnotations;

namespace gbs.Shared.Dtos;

public class UserUpdateDto
{
    public int? ChurchId { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}