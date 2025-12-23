namespace ConstructionFinance.API.DTOs.Quotation
{
    public class CreateBOQItemDto
    {
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public string Category { get; set; }
        public string Remarks { get; set; }
        public int? ProjectId { get; set; }
    }

}
