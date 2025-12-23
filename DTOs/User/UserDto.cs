namespace ConstructionFinance.API.DTOs.User
{
 public class UserDto
 {
 public int Id { get; set; }
 public string Username { get; set; } = null!;
 public string Email { get; set; } = null!;
 public string? FirstName { get; set; }
 public string? LastName { get; set; }
 public int? RoleId { get; set; }
 public string? RoleName { get; set; }
 public bool IsActive { get; set; }
 public DateTime CreatedAt { get; set; }
 }
}
