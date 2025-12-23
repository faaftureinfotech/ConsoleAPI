using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionFinance.API.Models
{
 public class EmployeeAllocation
 {
 [Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
 public int Id { get; set; }

 [Required]
 public int EmployeeId { get; set; }
 public Employee? Employee { get; set; }

 public int? ProjectId { get; set; }
 public Project? Project { get; set; }

 [Required]
 public DateTime AllocationDate { get; set; }

 [Required]
 [MaxLength(50)]
 public string AllocationType { get; set; } = null!; // Full Day / Half Day

 [MaxLength(1000)]
 public string? Notes { get; set; }
 }
}
