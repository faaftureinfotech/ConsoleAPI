namespace ConstructionFinance.API.DTOs.Supplier
{
    public class SupplierCreateUpdateDto
    {
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public string GstNumber { get; set; }
        public string PanNumber { get; set; }

        public string SupplierType { get; set; }
        public string Status { get; set; }

        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string UpiId { get; set; }

        public decimal OpeningBalance { get; set; }
        public string Notes { get; set; }
    }
}
