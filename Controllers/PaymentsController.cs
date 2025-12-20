using Microsoft.AspNetCore.Mvc;
using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs;
using ConstructionFinance.API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public PaymentsController(AppDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll() => await _db.Payments.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Payment>> Create(CreatePaymentDto dto)
        {
            var p = _mapper.Map<Payment>(dto);
            _db.Payments.Add(p);
            await _db.SaveChangesAsync();
            return CreatedAtAction(null, new { id = p.Id }, p);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Payments.FindAsync(id);
            if (p == null) return NotFound();
            _db.Payments.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
