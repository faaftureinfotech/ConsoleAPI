using AutoMapper;
using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Role;
using ConstructionFinance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers
{
 [ApiController]
 [Route("api/[controller]")]
 public class RoleController : ControllerBase
 {
 private readonly AppDbContext _db;
 private readonly IMapper _mapper;

 public RoleController(AppDbContext db, IMapper mapper)
 {
 _db = db;
 _mapper = mapper;
 }

 [HttpGet]
 public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
 {
 var roles = await _db.Set<Role>().ToListAsync();
 return Ok(_mapper.Map<List<RoleDto>>(roles));
 }

 [HttpGet("{id}")]
 public async Task<ActionResult<RoleDto>> GetById(int id)
 {
 var role = await _db.Set<Role>().FindAsync(id);
 if (role == null) return NotFound(new { message = $"Role with ID {id} not found." });
 return Ok(_mapper.Map<RoleDto>(role));
 }

 [HttpPost]
 public async Task<ActionResult<RoleDto>> Create([FromBody] CreateRoleDto dto)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);

 // Prevent duplicate role names
 var exists = await _db.Set<Role>().AnyAsync(r => r.Name == dto.Name);
 if (exists) return Conflict(new { message = "Role with same name already exists." });

 var role = _mapper.Map<Role>(dto);
 _db.Set<Role>().Add(role);
 await _db.SaveChangesAsync();

 return CreatedAtAction(nameof(GetById), new { id = role.Id }, _mapper.Map<RoleDto>(role));
 }

 [HttpPut("{id}")]
 public async Task<ActionResult<RoleDto>> Update(int id, [FromBody] UpdateRoleDto dto)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);

 var role = await _db.Set<Role>().FindAsync(id);
 if (role == null) return NotFound(new { message = $"Role with ID {id} not found." });

 // Prevent duplicate name with other roles
 var exists = await _db.Set<Role>().AnyAsync(r => r.Name == dto.Name && r.Id != id);
 if (exists) return Conflict(new { message = "Another role with same name already exists." });

 _mapper.Map(dto, role);
 await _db.SaveChangesAsync();

 return Ok(_mapper.Map<RoleDto>(role));
 }

 [HttpDelete("{id}")]
 public async Task<IActionResult> Delete(int id)
 {
 var role = await _db.Set<Role>().FindAsync(id);
 if (role == null) return NotFound(new { message = $"Role with ID {id} not found." });

 // If any users or entities reference roles, prevent deletion here (none currently)
 _db.Set<Role>().Remove(role);
 await _db.SaveChangesAsync();
 return NoContent();
 }
 }
}
