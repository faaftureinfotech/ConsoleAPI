using AutoMapper;
using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Project;
using ConstructionFinance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProjectsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // ✅ CREATE
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create([FromBody] CreateProjectDto dto)
        {
            // ✅ Validate Customer Exists
            bool customerExists = await _db.Customers
                .AnyAsync(x => x.Id == dto.CustomerId);

            if (!customerExists)
                return BadRequest("Invalid CustomerId.");

            if (dto.AdvanceReceived > dto.ContractValue)
                return BadRequest("Advance cannot exceed Contract Value.");

            dto.RemainingAmount = dto.ContractValue - dto.AdvanceReceived;

            var project = _mapper.Map<Project>(dto);

            _db.Projects.Add(project);
            await _db.SaveChangesAsync();

            var result = await _db.Projects
                .Include(p => p.Customer)
                .FirstAsync(x => x.Id == project.Id);

            return Ok(_mapper.Map<ProjectDto>(result));
        }

        // ✅ GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            var projects = await _db.Projects
                .Include(p => p.Customer)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(_mapper.Map<List<ProjectDto>>(projects));
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            var project = await _db.Projects
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            return Ok(_mapper.Map<ProjectDto>(project));
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDto>> Update(int id, [FromBody] UpdateProjectDto dto)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            bool customerExists = await _db.Customers
                .AnyAsync(x => x.Id == dto.CustomerId);

            if (!customerExists)
                return BadRequest("Invalid CustomerId.");

            if (dto.AdvanceReceived > dto.ContractValue)
                return BadRequest("Advance cannot exceed Contract Value.");

            dto.RemainingAmount = dto.ContractValue - dto.AdvanceReceived;

            _mapper.Map(dto, project);
            await _db.SaveChangesAsync();

            var result = await _db.Projects
                .Include(p => p.Customer)
                .FirstAsync(x => x.Id == project.Id);

            return Ok(_mapper.Map<ProjectDto>(result));
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }

}
