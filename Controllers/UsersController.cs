using AutoMapper;
using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.User;
using ConstructionFinance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ConstructionFinance.API.Controllers
{
 [ApiController]
 [Route("api/[controller]")]
 public class UsersController : ControllerBase
 {
 private readonly AppDbContext _db;
 private readonly IMapper _mapper;

 public UsersController(AppDbContext db, IMapper mapper)
 {
 _db = db;
 _mapper = mapper;
 }

 // GET: api/users
 [HttpGet]
 public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
 {
 var users = await _db.Users.Include(u => u.Role).ToListAsync();
 return Ok(_mapper.Map<List<UserDto>>(users));
 }

 // GET: api/users/{id}
 [HttpGet("{id}")]
 public async Task<ActionResult<UserDto>> GetById(int id)
 {
 var user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
 if (user == null) return NotFound(new { message = $"User with ID {id} not found." });
 return Ok(_mapper.Map<UserDto>(user));
 }

 // POST: api/users
 [HttpPost]
 public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);

 // Unique username/email
 if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
 return Conflict(new { message = "Username already in use." });
 if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
 return Conflict(new { message = "Email already in use." });

 // Validate role
 if (dto.RoleId.HasValue)
 {
 if (!await _db.Roles.AnyAsync(r => r.Id == dto.RoleId.Value))
 return BadRequest(new { message = $"Role with ID {dto.RoleId.Value} does not exist." });
 }

 var user = new User
 {
 Username = dto.Username,
 Email = dto.Email,
 PasswordHash = HashPassword(dto.Password),
 FirstName = dto.FirstName,
 LastName = dto.LastName,
 RoleId = dto.RoleId,
 IsActive = true
 };

 _db.Users.Add(user);
 await _db.SaveChangesAsync();

 var saved = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == user.Id);
 return CreatedAtAction(nameof(GetById), new { id = user.Id }, _mapper.Map<UserDto>(saved));
 }

 // PUT: api/users/{id}
 [HttpPut("{id}")]
 public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto dto)
 {
 if (!ModelState.IsValid) return BadRequest(ModelState);

 var user = await _db.Users.FindAsync(id);
 if (user == null) return NotFound(new { message = $"User with ID {id} not found." });

 // Unique username/email
 if (await _db.Users.AnyAsync(u => u.Username == dto.Username && u.Id != id))
 return Conflict(new { message = "Username already in use by another user." });
 if (await _db.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id))
 return Conflict(new { message = "Email already in use by another user." });

 // Validate role
 if (dto.RoleId.HasValue)
 {
 if (!await _db.Roles.AnyAsync(r => r.Id == dto.RoleId.Value))
 return BadRequest(new { message = $"Role with ID {dto.RoleId.Value} does not exist." });
 }

 user.Username = dto.Username;
 user.Email = dto.Email;
 if (!string.IsNullOrWhiteSpace(dto.Password)) user.PasswordHash = HashPassword(dto.Password);
 user.FirstName = dto.FirstName;
 user.LastName = dto.LastName;
 user.RoleId = dto.RoleId;
 user.IsActive = dto.IsActive;

 await _db.SaveChangesAsync();
 var updated = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
 return Ok(_mapper.Map<UserDto>(updated));
 }

 // DELETE: api/users/{id}
 [HttpDelete("{id}")]
 public async Task<ActionResult> Delete(int id)
 {
 var user = await _db.Users.FindAsync(id);
 if (user == null) return NotFound(new { message = $"User with ID {id} not found." });

 _db.Users.Remove(user);
 await _db.SaveChangesAsync();
 return NoContent();
 }

 // Simple SHA256 password hash for demo - replace with proper hashing in production
 private static string HashPassword(string password)
 {
 using var sha = SHA256.Create();
 var bytes = Encoding.UTF8.GetBytes(password);
 var hash = sha.ComputeHash(bytes);
 return Convert.ToBase64String(hash);
 }
 }
}
