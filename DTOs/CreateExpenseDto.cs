using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs
{
    public class CreateExpenseDto
    {
        [Required] public int CustomerId { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        [Required] public decimal Amount { get; set; }
    }
}
