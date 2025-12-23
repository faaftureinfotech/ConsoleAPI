namespace ConstructionFinance.API.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }

        public string Designation { get; set; }
        public string? Department { get; set; }
        public string? AssignedProject { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Status { get; set; }

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
        public DateTime CreatedAt { get; set; }

        public int? RoleId { get; set; }
        public string? RoleName { get; set; }
    }


}
