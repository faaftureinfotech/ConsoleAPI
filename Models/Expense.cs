namespace ConstructionFinance.API.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Customer? Customer { get; set; }
    }
}
