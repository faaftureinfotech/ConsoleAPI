using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string ProjectName { get; set; } = null!;

        // ✅ FK TYPE MUST MATCH Customer.Id (int)
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public string ProjectType { get; set; } = "Other";
        public string Location { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Status { get; set; } = "Planning";

        public decimal ContractValue { get; set; }
        public decimal AdvanceReceived { get; set; }
        public decimal RemainingAmount { get; set; }

        public string? PaymentTerms { get; set; }
        public string? ProjectManager { get; set; }
        public string? Contractor { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}