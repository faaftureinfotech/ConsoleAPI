using ConstructionFinance.API.Data;
using ConstructionFinance.API.DTOs.Supplier;
using ConstructionFinance.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SuppliersController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
        {
            var suppliers = await _db.Suppliers
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    SupplierName = s.SupplierName,
                    ContactPerson = s.ContactPerson,
                    MobileNumber = s.MobileNumber,
                    Email = s.Email,
                    Address = s.Address,
                    City = s.City,
                    State = s.State,
                    GstNumber = s.GstNumber,
                    PanNumber = s.PanNumber,
                    SupplierType = s.SupplierType,
                    Status = s.Status,
                    BankName = s.BankName,
                    AccountNumber = s.AccountNumber,
                    IfscCode = s.IfscCode,
                    UpiId = s.UpiId,
                    OpeningBalance = s.OpeningBalance,
                    Notes = s.Notes
                })
                .ToListAsync();

            return Ok(suppliers);
        }

        // GET: api/suppliers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetById(int id)
        {
            var supplier = await _db.Suppliers.FindAsync(id);
            if (supplier == null) return NotFound(new { message = $"Supplier with ID {id} not found." });

            return Ok(new SupplierDto
            {
                Id = supplier.Id,
                SupplierName = supplier.SupplierName,
                ContactPerson = supplier.ContactPerson,
                MobileNumber = supplier.MobileNumber,
                Email = supplier.Email,
                Address = supplier.Address,
                City = supplier.City,
                State = supplier.State,
                GstNumber = supplier.GstNumber,
                PanNumber = supplier.PanNumber,
                SupplierType = supplier.SupplierType,
                Status = supplier.Status,
                BankName = supplier.BankName,
                AccountNumber = supplier.AccountNumber,
                IfscCode = supplier.IfscCode,
                UpiId = supplier.UpiId,
                OpeningBalance = supplier.OpeningBalance,
                Notes = supplier.Notes
            });
        }

        // POST: api/suppliers
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SupplierCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Basic validations
            if (string.IsNullOrWhiteSpace(dto.SupplierName))
                return BadRequest(new { message = "SupplierName is required." });

            if (string.IsNullOrWhiteSpace(dto.MobileNumber))
                return BadRequest(new { message = "MobileNumber is required." });

            // Prevent duplicate by name + mobile
            var exists = await _db.Suppliers.AnyAsync(s => s.SupplierName == dto.SupplierName && s.MobileNumber == dto.MobileNumber);
            if (exists) return Conflict(new { message = "Supplier with same name and mobile number already exists." });

            var supplier = new Supplier
            {
                SupplierName = dto.SupplierName,
                ContactPerson = dto.ContactPerson,
                MobileNumber = dto.MobileNumber,
                Email = dto.Email,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                GstNumber = dto.GstNumber,
                PanNumber = dto.PanNumber,
                SupplierType = dto.SupplierType,
                Status = dto.Status,
                BankName = dto.BankName,
                AccountNumber = dto.AccountNumber,
                IfscCode = dto.IfscCode,
                UpiId = dto.UpiId,
                OpeningBalance = dto.OpeningBalance,
                Notes = dto.Notes
            };

            _db.Suppliers.Add(supplier);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier.Id);
        }

        // PUT: api/suppliers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] SupplierCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var supplier = await _db.Suppliers.FindAsync(id);
            if (supplier == null) return NotFound(new { message = $"Supplier with ID {id} not found." });

            // Basic validations
            if (string.IsNullOrWhiteSpace(dto.SupplierName))
                return BadRequest(new { message = "SupplierName is required." });

            if (string.IsNullOrWhiteSpace(dto.MobileNumber))
                return BadRequest(new { message = "MobileNumber is required." });

            supplier.SupplierName = dto.SupplierName;
            supplier.ContactPerson = dto.ContactPerson;
            supplier.MobileNumber = dto.MobileNumber;
            supplier.Email = dto.Email;
            supplier.Address = dto.Address;
            supplier.City = dto.City;
            supplier.State = dto.State;
            supplier.GstNumber = dto.GstNumber;
            supplier.PanNumber = dto.PanNumber;
            supplier.SupplierType = dto.SupplierType;
            supplier.Status = dto.Status;
            supplier.BankName = dto.BankName;
            supplier.AccountNumber = dto.AccountNumber;
            supplier.IfscCode = dto.IfscCode;
            supplier.UpiId = dto.UpiId;
            supplier.OpeningBalance = dto.OpeningBalance;
            supplier.Notes = dto.Notes;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/suppliers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var supplier = await _db.Suppliers.FindAsync(id);
            if (supplier == null) return NotFound(new { message = $"Supplier with ID {id} not found." });

            // If there were entities referencing suppliers, we'd prevent deletion.
            // For now remove directly.
            _db.Suppliers.Remove(supplier);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
