using System.ComponentModel.DataAnnotations.Schema;
using gbs.Shared.Const;

namespace gbs.Shared.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailVerified { get; set; } = false;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.User;
    public bool IsActive { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLogin { get; set; }

    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}