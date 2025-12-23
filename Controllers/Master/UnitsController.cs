using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Master;
using ConstructionFinance.API.Models.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UnitsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetUnits()
        {
            var units = await _context.Units.ToListAsync();
            return Ok(units.Select(u => new UnitDto { Id = u.Id, Name = u.Name }));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUnit(CreateUnitDto dto)
        {
            var unit = new Unit { Name = dto.Name };
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return Ok(unit.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUnit(int id, CreateUnitDto dto)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null) return NotFound();

            unit.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null) return NotFound();

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
