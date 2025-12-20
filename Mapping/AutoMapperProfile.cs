using AutoMapper;
using ConstructionFinance.API.DTOs;
using ConstructionFinance.API.DTOs.Employee;
using ConstructionFinance.API.DTOs.Project;
using ConstructionFinance.API.Models;
using System.Linq;

namespace ConstructionFinance.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.Id));

            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CreatePaymentDto, Models.Payment>();
            CreateMap<CreateExpenseDto, Models.Expense>();

            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();

            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<Project, ProjectDto>()
          .ForMember(dest => dest.CustomerName,
                     opt => opt.MapFrom(src => src.Customer.Name));
        }
    }
}