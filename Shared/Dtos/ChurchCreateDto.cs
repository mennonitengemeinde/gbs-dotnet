using System.ComponentModel.DataAnnotations;

namespace gbs.Shared.Dtos;

public class ChurchCreateDto
{
    [Required, StringLength(150, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
}