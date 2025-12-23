using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs
{
    public class CreateCustomerDto
    {
        [Required]  public string FirstName { get; set; }
        [Required]  public string LastName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? GstNumber { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        
    }
}
