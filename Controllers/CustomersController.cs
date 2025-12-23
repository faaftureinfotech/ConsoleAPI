using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConstructionFinance.API.Data;
using ConstructionFinance.API.Models;
using ConstructionFinance.API.DTOs;
using AutoMapper;

namespace ConstructionFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public CustomersController(AppDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var list = await _db.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDto>>(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null) return NotFound();
            return _mapper.Map<CustomerDto>(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody]  CreateCustomerDto dto)
        {
            // Normalize input
            var firstName = dto.FirstName.Trim().ToLower();
            var lastName = dto.LastName.Trim().ToLower();

            // Check existing
            var exists = await _db.Customers
                .AnyAsync(c => c.FirstName.ToLower() == firstName &&
                               c.LastName.ToLower() == lastName);

            if (exists)
            {
                return Conflict(new
                {
                    message = $"Customer '{dto.FirstName} {dto.LastName}' already exists."
                });
            }

            var customer = _mapper.Map<Customer>(dto);

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] CreateCustomerDto dto)
        {
            var existingCustomer = await _db.Customers.FindAsync(id);

            if (existingCustomer == null)
                return NotFound($"Customer with ID {id} not found.");

            // Map updated fields into existing entity
            _mapper.Map(dto, existingCustomer);

            await _db.SaveChangesAsync();

            var updatedCustomerDto = _mapper.Map<CustomerDto>(existingCustomer);

            return Ok(updatedCustomerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            // Prevent deletion when projects reference this customer
            var hasProjects = await _db.Projects.AnyAsync(p => p.CustomerId == id);
            if (hasProjects)
            {
                return Conflict(new { message = "Cannot delete customer because one or more projects are mapped to this customer." });
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();

            return NoContent(); // 204 - Success with no response body
        }


    }
}
