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
        var employee = _mapper.Map<Employee>(dto);

        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();

        // ✅ FIX: Map to EmployeeDto, NOT CreateEmployeeDto
        var result = _mapper.Map<EmployeeDto>(employee);
        return Ok(result);
    }

    // ✅ GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
    {
        var employees = await _db.Employees.ToListAsync();
        var result = _mapper.Map<List<EmployeeDto>>(employees);
        return Ok(result);
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        var employee = await _db.Employees.FindAsync(id);

        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        return Ok(_mapper.Map<EmployeeDto>(employee));
    }

    // ✅ UPDATE
    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDto>> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        var employee = await _db.Employees.FindAsync(id);

        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        _mapper.Map(dto, employee);
        await _db.SaveChangesAsync();

        return Ok(_mapper.Map<EmployeeDto>(employee));
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _db.Employees.FindAsync(id);

        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        _db.Employees.Remove(employee);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
