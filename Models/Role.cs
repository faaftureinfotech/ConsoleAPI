using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionFinance.API.Models
{
 public class Role
 {
 [Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 public int Id { get; set; }

 [Required]
 [MaxLength(100)]
 public string Name { get; set; } = null!;

 public string? Description { get; set; }

 [MaxLength(20)]
 public string Status { get; set; } = "Active"; // Active / Inactive

 public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
 }
}
