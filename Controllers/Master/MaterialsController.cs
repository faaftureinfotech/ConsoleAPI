using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Master;
using ConstructionFinance.API.Models.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaterialsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterials()
        {
            var materials = await _context.Materials.ToListAsync();
            return Ok(materials.Select(m => new MaterialDto
            {
                Id = m.Id,
                Name = m.Name,
                DefaultUnitId = m.DefaultUnitId,
                DefaultRate = m.DefaultRate
            }));
        }

        [HttpPost]
        public async Task<ActionResult> CreateMaterial(CreateMaterialDto dto)
        {
            var material = new Material
            {
                Name = dto.Name,
                DefaultUnitId = dto.DefaultUnitId,
                DefaultRate = dto.DefaultRate
            };

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return Ok(material.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMaterial(int id, CreateMaterialDto dto)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return NotFound();

            material.Name = dto.Name;
            material.DefaultUnitId = dto.DefaultUnitId;
            material.DefaultRate = dto.DefaultRate;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMaterial(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return NotFound();

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
