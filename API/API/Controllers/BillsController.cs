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
                .Include(u => u.BillDetail)
                .ThenInclude(u => u.Product)
                .ThenInclude(u => u.Image)
                .ToListAsync();

            var serializedData = JsonSerializer.Serialize(billUserStatus, options);
            return Content(serializedData, "application/json");
        }

        [HttpPut("{id}/update-status")]
        public async Task<IActionResult> UpdateBillStatus(int id, [FromBody] int statusId)
        {
            var bill = await _context.bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            bill.StatusID = statusId;
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


        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills(int id)
        {
            var bill = await _context.bill
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.BillDetail)
                .ThenInclude(u => u.Product)
                .FirstOrDefaultAsync(u => u.BillId == id);

            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bill bills)
        {
            if (id != bills.BillId)
            {
                return BadRequest();
            }

            var bill = await _context.bill
                .FirstOrDefaultAsync(p => p.BillId == id);

            if (bill == null)
            {
                return NotFound();
            }

            bill.Date = bills.Date;
            bill.Total = bills.Total;
            bill.UserID = bills.UserID;
            bill.StatusID = bills.StatusID;

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
        //public async Task<IActionResult> PutBill(int id, Bill bill)
        //{
        //    if (id != bill.BillId)
        //    {
        //        return BadRequest("Bill ID mismatch.");
        //    }

        //    _context.Entry(bill).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BillExists(id))
        //        {
        //            return NotFound("Bill not found.");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


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
