namespace ConstructionFinance.API.Models.Quotation
{
    public class Quotation
    {
        public int Id { get; set; }
        public string QuotationNumber { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public DateTime QuotationDate { get; set; }
        public DateTime ValidUntil { get; set; }

        public decimal SubTotal { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public string Notes { get; set; }
        public string Status { get; set; }

        public ICollection<QuotationItem> Items { get; set; }
    }

}
