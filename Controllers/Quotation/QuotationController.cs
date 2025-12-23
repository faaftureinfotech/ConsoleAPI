using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Quotation;
using ConstructionFinance.API.Models.Quotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers.Quotation
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuotationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuotationDto dto)
        {
            var quotation = new ConstructionFinance.API.Models.Quotation.Quotation
            {
                QuotationNumber = dto.QuotationNumber,
                CustomerId = dto.CustomerId,
                ProjectId = dto.ProjectId,
                QuotationDate = dto.QuotationDate,
                ValidUntil = dto.ValidUntil,
                TaxPercentage = dto.TaxPercentage,
                Notes = dto.Notes,
                Status = dto.Status
            };

            quotation.Items = dto.Items.Select(i => new QuotationItem
            {
                ItemId = i.ItemId,
                Description = i.Description,
                Unit = i.Unit,
                Quantity = i.Quantity,
                Rate = i.Rate,
                Amount = i.Quantity * i.Rate,
                Phase = i.Phase
            }).ToList();

            quotation.SubTotal = quotation.Items.Sum(x => x.Amount);
            quotation.TaxAmount = quotation.SubTotal * (quotation.TaxPercentage / 100);
            quotation.TotalAmount = quotation.SubTotal + quotation.TaxAmount;

            _context.Quotations.Add(quotation);
            await _context.SaveChangesAsync();

            return Ok(quotation.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var quotation = await _context.Quotations
                .Include(q => q.Items)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quotation == null) return NotFound();

            return Ok(quotation);
        }
    }

}
