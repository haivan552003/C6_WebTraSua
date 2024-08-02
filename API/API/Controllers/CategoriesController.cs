using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CategoriesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> Getcategory()
        {
            return await _context.category.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var bill = await _context.category
                              .Include(u => u.Products)
                            .ThenInclude(u => u.Image)
                            .ThenInclude(u => u.Products)
                            .ThenInclude(u => u.Size_Product)
                            .Where(u => u.CateID == id)
                            .ToListAsync();


            if (bill == null)
            {
                return NotFound();
            }

            var serializedData = JsonSerializer.Serialize(bill, options);
            return Content(serializedData, "application/json");
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(int id, Categories categories)
        {
            if (id != categories.CateID)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
            var newCate = new Categories
            {
                Image = categories.Image,
                Name = categories.Name
            };

            _context.category.Add(newCate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.CateID }, newCate);
        }
        private bool CategoriesExists(int id)
        {
            return _context.category.Any(e => e.CateID == id);
        }
    }
}
