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

        // GET: api/Bills/stats/daily
        [HttpGet("stats/daily")]
        public async Task<ActionResult> GetDailyStats(DateTime date)
        {
            var bills = await _context.bill
                .Where(b => b.Date.Date == date.Date)
                .Include(b => b.Status)
                .Include(b => b.User)
                .ToListAsync();

            var statusStats = bills
                .GroupBy(b => b.Status.Name)
                .Select(g => new
                {
                    Status = g.Key,
                    TotalRevenue = g.Sum(b => b.Total),
                    TotalBills = g.Count(),
                    Bills = g.Select(b => new
                    {
                        b.BillId,
                        b.Date,
                        b.User.Name,
                        b.Total,
                        Status = b.Status.Name
                    }).ToList()
                })
                .ToList();

            return Ok(new
            {
                Date = date.Date,
                StatusStats = statusStats
            });
        }


        // GET: api/Bills/stats/monthly
        [HttpGet("stats/monthly")]
        public async Task<ActionResult> GetMonthlyStats(int year, int month)
        {
            var bills = await _context.bill
                .Where(b => b.Date.Year == year && b.Date.Month == month)
                .Include(b => b.Status)
                .Include(b => b.User)
                .ToListAsync();

            var statusStats = bills
                .GroupBy(b => b.Status.Name)
                .Select(g => new
                {
                    Status = g.Key,
                    TotalRevenue = g.Sum(b => b.Total),
                    TotalBills = g.Count(),
                    Bills = g.Select(b => new
                    {
                        b.BillId,
                        b.Date,
                        b.User.Name,
                        b.Total,
                        Status = b.Status.Name
                    }).ToList()
                })
                .ToList();

            return Ok(new
            {
                Year = year,
                Month = month,
                StatusStats = statusStats
            });
        }


        // GET: api/Bills/stats/annual
        [HttpGet("stats/annual")]
        public async Task<ActionResult> GetAnnualStats(int year)
        {
            var bills = await _context.bill
                .Where(b => b.Date.Year == year)
                .Include(b => b.Status)
                .Include(b => b.User)
                .ToListAsync();

            var statusStats = bills
                .GroupBy(b => b.Status.Name)
                .Select(g => new
                {
                    Status = g.Key,
                    TotalRevenue = g.Sum(b => b.Total),
                    TotalBills = g.Count(),
                    Bills = g.Select(b => new
                    {
                        b.BillId,
                        b.Date,
                        b.User.Name,
                        b.Total,
                        Status = b.Status.Name
                    }).ToList()
                })
                .ToList();

            return Ok(new
            {
                Year = year,
                StatusStats = statusStats
            });
        }


        // GET: api/Bills/stats/user
        [HttpGet("stats/user")]
        public async Task<ActionResult> GetStatsByUser(int year, int month)
        {
            var bills = await _context.bill
                .Where(b => b.Date.Year == year && b.Date.Month == month)
                .Include(b => b.Status)
                .Include(b => b.User)
                .ToListAsync();

            var userStats = bills
                .GroupBy(b => b.UserID)
                .Select(g => new
                {
                    UserID = g.Key,
                    UserName = g.FirstOrDefault()?.User.UserName,
                    StatusStats = g
                        .GroupBy(b => b.Status.Name)
                        .Select(sg => new
                        {
                            Status = sg.Key,
                            TotalRevenue = sg.Sum(b => b.Total),
                            TotalBills = sg.Count()
                        })
                        .ToList(),
                    TotalRevenue = g.Sum(b => b.Total),
                    TotalBills = g.Count()
                })
                .ToList();

            return Ok(new
            {
                Year = year,
                Month = month,
                UserStats = userStats
            });
        }

        private bool BillExists(int id)
        {
            return _context.bill.Any(e => e.BillId == id);
        }
    }
}
