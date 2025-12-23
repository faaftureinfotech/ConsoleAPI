using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs.User
{
 public class CreateUserDto
 {
 [Required]
 [MinLength(3)]
 public string Username { get; set; } = null!;

 [Required]
 [EmailAddress]
 public string Email { get; set; } = null!;

 [Required]
 [MinLength(6)]
 public string Password { get; set; } = null!;

 public string? FirstName { get; set; }
 public string? LastName { get; set; }
 public int? RoleId { get; set; }
 }
}
