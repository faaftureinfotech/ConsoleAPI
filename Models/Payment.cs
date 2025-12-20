namespace ConstructionFinance.API.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Customer? Customer { get; set; }
    }
}
