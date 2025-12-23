namespace ConstructionFinance.API.DTOs.Quotation
{
    public class QuotationItemDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; } // links to BOQMaster

        public string Description { get; set; }
        public string Unit { get; set; }

        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }

        public string Phase { get; set; }
    }

}
