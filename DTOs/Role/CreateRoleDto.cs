using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs.Role
{
 public class CreateRoleDto
 {
 [Required]
 [MaxLength(100)]
 public string Name { get; set; } = null!;

 public string? Description { get; set; }

 [MaxLength(20)]
 public string? Status { get; set; }
 }
}
