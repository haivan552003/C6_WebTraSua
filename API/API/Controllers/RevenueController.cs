using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using API.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly AppDBContext _context;

        public RevenueController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Revenue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RevenueDto>>> GetRevenue(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int? year = null,
            [FromQuery] int? month = null,
            [FromQuery] int? day = null,
            [FromQuery] int[] statusIds = null)
        {
            var query = _context.bill.AsQueryable();

            // Apply status filter
            if (statusIds != null && statusIds.Any())
            {
                query = query.Where(b => statusIds.Contains(b.StatusID));
            }

            // Apply date filters
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(b => b.Date >= startDate.Value && b.Date <= endDate.Value);
            }
            else if (year.HasValue)
            {
                if (month.HasValue)
                {
                    var startOfMonth = new DateTime(year.Value, month.Value, 1);
                    var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                    query = query.Where(b => b.Date >= startOfMonth && b.Date <= endOfMonth);
                }
                else
                {
                    var startOfYear = new DateTime(year.Value, 1, 1);
                    var endOfYear = startOfYear.AddYears(1).AddDays(-1);
                    query = query.Where(b => b.Date >= startOfYear && b.Date <= endOfYear);
                }
            }
            else if (day.HasValue)
            {
                var date = new DateTime(year.Value, month.Value, day.Value);
                query = query.Where(b => b.Date.Date == date);
            }

            var bills = await query
                .Include(b => b.BillDetail)
                .ToListAsync();

            var revenueData = bills
                .GroupBy(b => new
                {
                    b.Date.Year,
                    b.Date.Month,
                    b.Date.Day
                })
                .Select(g => new RevenueDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Day = g.Key.Day,
                    TotalRevenue = g.Sum(b => b.BillDetail.Sum(bd => bd.Subtotal))
                })
                .ToList();

            return Ok(revenueData);
        }

        public class RevenueDto
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int? Day { get; set; }
            public float TotalRevenue { get; set; }
        }
    }
}
