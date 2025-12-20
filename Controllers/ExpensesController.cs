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
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ExpensesController(AppDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAll() => await _db.Expenses.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Expense>> Create(CreateExpenseDto dto)
        {
            var e = _mapper.Map<Expense>(dto);
            _db.Expenses.Add(e);
            await _db.SaveChangesAsync();
            return CreatedAtAction(null, new { id = e.Id }, e);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _db.Expenses.FindAsync(id);
            if (e == null) return NotFound();
            _db.Expenses.Remove(e);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
