using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs
{
    public class CreatePaymentDto
    {
        [Required] public int CustomerId { get; set; }
        [Required] public decimal Amount { get; set; }
        public string? Note { get; set; }
    }
}
