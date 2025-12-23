namespace ConstructionFinance.API.DTOs.EmployeeAllocation
{
 public class EmployeeAllocationDto
 {
 public int Id { get; set; }
 public int EmployeeId { get; set; }
 public string? EmployeeName { get; set; }
 public string? EmployeeType { get; set; }
 public int? ProjectId { get; set; }
 public string? ProjectName { get; set; }
 public DateTime AllocationDate { get; set; }
 public string AllocationType { get; set; }
 public string? Notes { get; set; }
 }
}
