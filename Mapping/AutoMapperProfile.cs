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
                .ForMember(d => d.CustomerId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName));

            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CreatePaymentDto, Models.Payment>();
            CreateMap<CreateExpenseDto, Models.Expense>();

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
                    !string.IsNullOrWhiteSpace(src.FirstName) ? src.FirstName :
                    (!string.IsNullOrWhiteSpace(src.FullName) ? src.FullName.Split(new[] { ' ' }, 2)[0] : string.Empty)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src =>
                    !string.IsNullOrWhiteSpace(src.LastName) ? src.LastName :
                    (!string.IsNullOrWhiteSpace(src.FullName) && src.FullName.Split(new[] { ' ' }, 2).Length > 1 ? src.FullName.Split(new[] { ' ' }, 2)[1] : string.Empty)))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Type) ? "Employee" : src.Type))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
                    !string.IsNullOrWhiteSpace(src.FirstName) ? src.FirstName :
                    (!string.IsNullOrWhiteSpace(src.FullName) ? src.FullName.Split(new[] { ' ' }, 2)[0] : string.Empty)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src =>
                    !string.IsNullOrWhiteSpace(src.LastName) ? src.LastName :
                    (!string.IsNullOrWhiteSpace(src.FullName) && src.FullName.Split(new[] { ' ' }, 2).Length > 1 ? src.FullName.Split(new[] { ' ' }, 2)[1] : string.Empty)))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null));

            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<Project, ProjectDto>()
          .ForMember(dest => dest.CustomerName,
                     opt => opt.MapFrom(src => src.Customer.Name));

            // Role mappings
            CreateMap<Role, DTOs.Role.RoleDto>();
            CreateMap<DTOs.Role.CreateRoleDto, Role>();
            CreateMap<DTOs.Role.UpdateRoleDto, Role>();

            // User mappings
            CreateMap<User, DTOs.User.UserDto>()
        .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null));
            CreateMap<DTOs.User.CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<DTOs.User.UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Employee allocation mappings
            CreateMap<EmployeeAllocation, DTOs.EmployeeAllocation.EmployeeAllocationDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : null))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Type : null))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : null));
            CreateMap<DTOs.EmployeeAllocation.CreateEmployeeAllocationDto, EmployeeAllocation>();
            CreateMap<DTOs.EmployeeAllocation.UpdateEmployeeAllocationDto, EmployeeAllocation>();
        }
    }
}