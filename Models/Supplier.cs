using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionFinance.API.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        // Supplier Info
        [Required, MaxLength(200)]
        public string SupplierName { get; set; }

        [MaxLength(150)]
        public string ContactPerson { get; set; }

        [Required, MaxLength(20)]
        public string MobileNumber { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(20)]
        public string GstNumber { get; set; }

        [MaxLength(20)]
        public string PanNumber { get; set; }

        [MaxLength(50)]
        public string SupplierType { get; set; } // Material / Service

        [MaxLength(20)]
        public string Status { get; set; } // Active / Inactive

        // Bank Details
        [MaxLength(150)]
        public string BankName { get; set; }

        [MaxLength(50)]
        public string AccountNumber { get; set; }

        [MaxLength(20)]
        public string IfscCode { get; set; }

        [MaxLength(50)]
        public string UpiId { get; set; }

        // Control
        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningBalance { get; set; }

        public string Notes { get; set; }
    }
}
