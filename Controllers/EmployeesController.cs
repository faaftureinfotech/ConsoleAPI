using AutoMapper;
using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Employee;
using ConstructionFinance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public EmployeesController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // ✅ CREATE
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create([FromBody] CreateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Validate role if provided
        if (dto.RoleId.HasValue)
        {
            var roleExists = await _db.Roles.AnyAsync(r => r.Id == dto.RoleId.Value);
            if (!roleExists)
                return BadRequest(new { message = $"Role with ID {dto.RoleId.Value} does not exist." });
        }

        // Basic business validations
        if (dto.SalaryType == "Daily" && (dto.RatePerDay == null || dto.RatePerDay <= 0))
            return BadRequest(new { message = "RatePerDay must be provided and >0 for Daily salary type." });

        if (dto.SalaryType == "Monthly" && (dto.MonthlySalary == null || dto.MonthlySalary <= 0))
            return BadRequest(new { message = "MonthlySalary must be provided and >0 for Monthly salary type." });

        var employee = _mapper.Map<Employee>(dto);

        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        // Reload employee including Role so RoleName is populated
        var saved = await _db.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == employee.Id);
        var result = _mapper.Map<EmployeeDto>(saved!);
        return Ok(result);
    }

    // ✅ GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
    {
        var employees = await _db.Employees.Include(e => e.Role).ToListAsync();
        var result = _mapper.Map<List<EmployeeDto>>(employees);
        return Ok(result);
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        var employee = await _db.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null)
            return NotFound(new { message = $"Employee with ID {id} not found." });

        return Ok(_mapper.Map<EmployeeDto>(employee));
    }

    // ✅ UPDATE
    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDto>> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employee = await _db.Employees.FindAsync(id);

        if (employee == null)
            return NotFound(new { message = $"Employee with ID {id} not found." });

        // Validate role if provided
        if (dto.RoleId.HasValue)
        {
            var roleExists = await _db.Roles.AnyAsync(r => r.Id == dto.RoleId.Value);
            if (!roleExists)
                return BadRequest(new { message = $"Role with ID {dto.RoleId.Value} does not exist." });
        }

        // Business rules
        if (dto.SalaryType == "Daily" && (dto.RatePerDay == null || dto.RatePerDay <= 0))
            return BadRequest(new { message = "RatePerDay must be provided and >0 for Daily salary type." });

        if (dto.SalaryType == "Monthly" && (dto.MonthlySalary == null || dto.MonthlySalary <= 0))
            return BadRequest(new { message = "MonthlySalary must be provided and >0 for Monthly salary type." });

        _mapper.Map(dto, employee);
        await _db.SaveChangesAsync();

        var updated = await _db.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == id);
        return Ok(_mapper.Map<EmployeeDto>(updated!));
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _db.Employees.FindAsync(id);

        if (employee == null)
            return NotFound(new { message = $"Employee with ID {id} not found." });

        // No foreign keys reference employees currently — remove directly
        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
