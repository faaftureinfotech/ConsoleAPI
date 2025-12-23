namespace ConstructionFinance.API.DTOs.Role
{
 public class RoleDto
 {
 public int Id { get; set; }
 public string Name { get; set; } = null!;
 public string? Description { get; set; }
 public string Status { get; set; } = "Active";
 public DateTime CreatedAt { get; set; }
 }
}
