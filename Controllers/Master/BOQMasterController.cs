using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Master;
using ConstructionFinance.API.Models.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class BOQMasterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BOQMasterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BOQMasterDto>>> Get()
        {
            var data = await _context.BOQMasters.ToListAsync();

            return Ok(data.Select(x => new BOQMasterDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                DefaultUnitId = x.DefaultUnitId,
                DefaultRate = x.DefaultRate,
                Description = x.Description
            }));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBOQMasterDto dto)
        {
            var master = new BOQMaster
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                DefaultUnitId = dto.DefaultUnitId,
                DefaultRate = dto.DefaultRate,
                Description = dto.Description
            };

            _context.BOQMasters.Add(master);
            await _context.SaveChangesAsync();

            return Ok(master.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateBOQMasterDto dto)
        {
            var master = await _context.BOQMasters.FindAsync(id);
            if (master == null) return NotFound();

            master.Name = dto.Name;
            master.CategoryId = dto.CategoryId;
            master.DefaultUnitId = dto.DefaultUnitId;
            master.DefaultRate = dto.DefaultRate;
            master.Description = dto.Description;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var master = await _context.BOQMasters.FindAsync(id);
            if (master == null) return NotFound();

            _context.BOQMasters.Remove(master);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
