using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionFinance.API.Models.Quotation
{
    public class BOQItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [NotMapped]
        public decimal Amount => Quantity * Rate;

        public string Category { get; set; }

        public string Remarks { get; set; }

        // Foreign Key
        public int? ProjectId { get; set; }
    }
}
