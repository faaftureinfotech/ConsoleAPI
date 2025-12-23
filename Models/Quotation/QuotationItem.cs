namespace ConstructionFinance.API.Models.Quotation
{
    public class QuotationItem
    {
        public int Id { get; set; }

        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        public int ItemId { get; set; } // maps to BOQMaster.Id
        public string Description { get; set; }
        public string Unit { get; set; }

        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
        public string Phase { get; set; }
    }

}
