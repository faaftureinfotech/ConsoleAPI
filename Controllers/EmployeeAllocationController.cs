using AutoMapper;
using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.EmployeeAllocation;
using ConstructionFinance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers
{
 [ApiController]
 [Route("api/[controller]")]
 public class EmployeeAllocationController : ControllerBase
 {
 private readonly AppDbContext _db;
 private readonly IMapper _mapper;

 public EmployeeAllocationController(AppDbContext db, IMapper mapper)
 {
 _db = db;
 _mapper = mapper;
 }

 [HttpGet]
 public async Task<ActionResult<IEnumerable<EmployeeAllocationDto>>> GetAll()
 {
 var list = await _db.EmployeeAllocations.Include(e => e.Employee).Include(e => e.Project).ToListAsync();
 return Ok(_mapper.Map<List<EmployeeAllocationDto>>(list));
 }

 [HttpGet("{id}")]
 public async Task<ActionResult<EmployeeAllocationDto>> GetById(int id)
 {
 var alloc = await _db.EmployeeAllocations.Include(e => e.Employee).Include(e => e.Project).FirstOrDefaultAsync(a => a.Id == id);
 if (alloc == null) return NotFound(new { message = $"Employee allocation with ID {id} not found" });
 return Ok(_mapper.Map<EmployeeAllocationDto>(alloc));
 }

 [HttpPost]
 public async Task<ActionResult<EmployeeAllocationDto>> Create([FromBody] CreateEmployeeAllocationDto dto)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);

 // Validate employee
 var employeeExists = await _db.Employees.AnyAsync(e => e.Id == dto.EmployeeId);
 if (!employeeExists) return BadRequest(new { message = $"Employee with ID {dto.EmployeeId} does not exist." });

 // Validate project if provided
 if (dto.ProjectId.HasValue)
 {
 var projectExists = await _db.Projects.AnyAsync(p => p.Id == dto.ProjectId.Value);
 if (!projectExists) return BadRequest(new { message = $"Project with ID {dto.ProjectId.Value} does not exist." });
 }

 // Prevent duplicate allocation for same employee on same date and same allocation type
 var exists = await _db.EmployeeAllocations.AnyAsync(a => a.EmployeeId == dto.EmployeeId && a.AllocationDate.Date == dto.AllocationDate.Date && a.AllocationType == dto.AllocationType && a.ProjectId == dto.ProjectId);
 if (exists) return Conflict(new { message = "Employee allocation with same parameters already exists." });

 var alloc = _mapper.Map<EmployeeAllocation>(dto);
 _db.EmployeeAllocations.Add(alloc);
 await _db.SaveChangesAsync();

 var saved = await _db.EmployeeAllocations.Include(e => e.Employee).Include(e => e.Project).FirstOrDefaultAsync(a => a.Id == alloc.Id);
 return CreatedAtAction(nameof(GetById), new { id = alloc.Id }, _mapper.Map<EmployeeAllocationDto>(saved));
 }

 [HttpPut("{id}")]
 public async Task<ActionResult<EmployeeAllocationDto>> Update(int id, [FromBody] UpdateEmployeeAllocationDto dto)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);

 var existing = await _db.EmployeeAllocations.FindAsync(id);
 if (existing == null) return NotFound(new { message = $"Employee allocation with ID {id} not found" });

 // Validate employee
 var employeeExists = await _db.Employees.AnyAsync(e => e.Id == dto.EmployeeId);
 if (!employeeExists) return BadRequest(new { message = $"Employee with ID {dto.EmployeeId} does not exist." });

 // Validate project if provided
 if (dto.ProjectId.HasValue)
 {
 var projectExists = await _db.Projects.AnyAsync(p => p.Id == dto.ProjectId.Value);
 if (!projectExists) return BadRequest(new { message = $"Project with ID {dto.ProjectId.Value} does not exist." });
 }

 // Prevent duplicate allocation excluding current record
 var exists = await _db.EmployeeAllocations.AnyAsync(a => a.Id != id && a.EmployeeId == dto.EmployeeId && a.AllocationDate.Date == dto.AllocationDate.Date && a.AllocationType == dto.AllocationType && a.ProjectId == dto.ProjectId);
 if (exists) return Conflict(new { message = "Another allocation with same parameters already exists." });

 _mapper.Map(dto, existing);
 await _db.SaveChangesAsync();

 var updated = await _db.EmployeeAllocations.Include(e => e.Employee).Include(e => e.Project).FirstOrDefaultAsync(a => a.Id == id);
 return Ok(_mapper.Map<EmployeeAllocationDto>(updated));
 }

 [HttpDelete("{id}")]
 public async Task<IActionResult> Delete(int id)
 {
 var existing = await _db.EmployeeAllocations.FindAsync(id);
 if (existing == null) return NotFound(new { message = $"Employee allocation with ID {id} not found" });

 _db.EmployeeAllocations.Remove(existing);
 await _db.SaveChangesAsync();
 return NoContent();
 }
 }
}
