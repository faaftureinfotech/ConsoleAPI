using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs.EmployeeAllocation
{
 public class UpdateEmployeeAllocationDto
 {
 [Required]
 public int EmployeeId { get; set; }
 public int? ProjectId { get; set; }
 [Required]
 public DateTime AllocationDate { get; set; }
 [Required]
 [RegularExpression("^(Full Day|Half Day)$", ErrorMessage = "Allocation type must be either 'Full Day' or 'Half Day'")]
 public string AllocationType { get; set; }
 [MaxLength(1000)]
 public string? Notes { get; set; }
 }
}
