namespace ConstructionFinance.API.DTOs.Quotation
{
    public class CreateQuotationDto
    {
        public string QuotationNumber { get; set; }

        public int CustomerId { get; set; }
        public int ProjectId { get; set; }

        public DateTime QuotationDate { get; set; }
        public DateTime ValidUntil { get; set; }

        public decimal TaxPercentage { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public List<CreateQuotationItemDto> Items { get; set; }
    }

}
