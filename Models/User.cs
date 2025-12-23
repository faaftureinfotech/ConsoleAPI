using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionFinance.API.Models
{
 public class User
 {
 [Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 public int Id { get; set; }

 [Required]
 [MaxLength(100)]
 public string Username { get; set; } = null!;

 [Required]
 [MaxLength(150)]
 public string Email { get; set; } = null!;

 [Required]
 public string PasswordHash { get; set; } = null!;

 [MaxLength(100)]
 public string? FirstName { get; set; }

 [MaxLength(100)]
 public string? LastName { get; set; }

 // Role FK
 public int? RoleId { get; set; }
 public Role? Role { get; set; }

 public bool IsActive { get; set; } = true;

 public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 }
}
