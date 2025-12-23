using System.ComponentModel.DataAnnotations;

namespace ConstructionFinance.API.Models
{
    public class Employee
    {
        public int Id { get; set; }

        // Basic Info
        public string Type { get; set; }            // Employee / Contractor

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        // Backwards-compatible FullName computed from first+last
        public string FullName => $"{FirstName} {LastName}".Trim();

        public string MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }

        // Work Details
        public string Designation { get; set; }
        public string? Department { get; set; }
        public string? AssignedProject { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Status { get; set; }          // Active / Inactive

        // Payment Details
        public string SalaryType { get; set; }      // Daily / Monthly
        public decimal? RatePerDay { get; set; }
        public decimal? MonthlySalary { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? UPIId { get; set; }

        // Contractors fields
        public string? AadharNumber { get; set; }   // Optional
        public string? PanNumber { get; set; }      // Optional

        public DateTime? ContractStartDate { get; set; } // Optional
        public DateTime? ContractEndDate { get; set; }   // Optional

        // Role FK
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
