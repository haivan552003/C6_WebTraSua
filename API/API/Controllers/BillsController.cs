using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Model;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public BillsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> Getbill()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var billUserStatus = await _context.bill
                .Include(u => u.User)
                .Include(u => u.Status)
                .ToListAsync();

            var serializedData = JsonSerializer.Serialize(billUserStatus, options);
            return Content(serializedData, "application/json");
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var bill = await _context.bill
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.BillDetail)
                .ToListAsync();


            if (bill == null)
            {
                return NotFound();
            }

            var serializedData = JsonSerializer.Serialize(bill, options);
            return Content(serializedData, "application/json");
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bill bill)
        {
            if (id != bill.BillId)
            {
                return BadRequest();
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            var newBill = new Bill
            {
                Date = bill.Date,
                Total = bill.Total,
                UserID = bill.UserID,
                StatusID = bill.StatusID
            };
            _context.bill.Add(newBill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.BillId }, newBill);
        }

        private bool BillExists(int id)
        {
            return _context.bill.Any(e => e.BillId == id);
        }
    }
}
