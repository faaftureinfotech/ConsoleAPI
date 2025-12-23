namespace ConstructionFinance.API.DTOs.Quotation
{
    public class CreateQuotationItemDto
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }

        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public string Phase { get; set; }
    }

}
