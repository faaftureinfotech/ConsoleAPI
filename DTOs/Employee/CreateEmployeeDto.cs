using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        // Optional employee type (Employee / Contractor)
        public string? Type { get; set; }

        // Support both FullName or FirstName+LastName for compatibility
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName { get; set; }

        [Required, Phone]
        public string MobileNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }

        [Required]
        public string Designation { get; set; }
        public string? Department { get; set; }
        public string? AssignedProject { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        [Required]
        public string Status { get; set; }

        [Required]
        public string SalaryType { get; set; }
        public decimal? RatePerDay { get; set; }
        public decimal? MonthlySalary { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? UPIId { get; set; }

        public string? AadharNumber { get; set; }
        public string? PanNumber { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }

        // Role assignment
        public int? RoleId { get; set; }
    }

}
