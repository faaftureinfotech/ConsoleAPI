namespace ConstructionFinance.API.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? GstNumber { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public string? Project { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalExpenses { get; set; }
    }
}
