using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs.Project
{
    public class CreateProjectDto
    {
        public string ProjectName { get; set; } = null!;
        public int CustomerId { get; set; }
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
    }

}