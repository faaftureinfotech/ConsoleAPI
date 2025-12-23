using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs.User
{
 public class UpdateUserDto
 {
 [Required]
 [MinLength(3)]
 public string Username { get; set; } = null!;

 [Required]
 [EmailAddress]
 public string Email { get; set; } = null!;

 public string? Password { get; set; }
 public string? FirstName { get; set; }
 public string? LastName { get; set; }
 public int? RoleId { get; set; }
 public bool IsActive { get; set; }
 }
}
