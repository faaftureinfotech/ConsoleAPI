using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Quotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BOQItem = ConstructionFinance.API.Models.Quotation.BOQItem;

namespace ConstructionFinance.API.Controllers.Quotation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BOQItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BOQItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BOQItemDto>>> GetItems()
        {
            var items = await _context.BOQItems.ToListAsync();
            return Ok(items.Select(x => new BOQItemDto
            {
                Id = x.Id,
                Description = x.Description,
                Unit = x.Unit,
                Quantity = x.Quantity,
                Rate = x.Rate,
                Category = x.Category,
                Remarks = x.Remarks,
                ProjectId = x.ProjectId
            }));
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem(CreateBOQItemDto dto)
        {
            var item = new BOQItem
            {
                Description = dto.Description,
                Unit = dto.Unit,
                Quantity = dto.Quantity,
                Rate = dto.Rate,
                Category = dto.Category,
                Remarks = dto.Remarks,
                ProjectId = dto.ProjectId
            };

            _context.BOQItems.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, CreateBOQItemDto dto)
        {
            var item = await _context.BOQItems.FindAsync(id);
            if (item == null) return NotFound();

            item.Description = dto.Description;
            item.Unit = dto.Unit;
            item.Quantity = dto.Quantity;
            item.Rate = dto.Rate;
            item.Category = dto.Category;
            item.Remarks = dto.Remarks;
            item.ProjectId = dto.ProjectId;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var item = await _context.BOQItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.BOQItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}
