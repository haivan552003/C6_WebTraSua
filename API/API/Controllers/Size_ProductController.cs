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
    public class Size_ProductController : ControllerBase
    {
        private readonly AppDBContext _context;

        public Size_ProductController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Size_Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size_Product>>> Getsize_product()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var product = await _context.size_product
               .Include(u => u.Product)
               .ThenInclude(u => u.Image)
               .Include(u => u.Size)
               .ToListAsync();

            if (product == null)
            {
                return NotFound();
            }

            var serializedData = JsonSerializer.Serialize(product, options);
            return Content(serializedData, "application/json");
        }

        // GET: api/Size_Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Size_Product>> GetSize_Product(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var bill = await _context.size_product
               
                .Include(u => u.Size)
                            .Include(u => u.Product)
                .ThenInclude(u => u.Image)
                .Where(u => u.SizeProductID == id)
                .ToListAsync();


            if (bill == null)
            {
                return NotFound();
            }

            var serializedData = JsonSerializer.Serialize(bill, options);
            return Content(serializedData, "application/json");
        }

        // PUT: api/Size_Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSize_Product(int id, Size_Product size_Product)
        {
            if (id != size_Product.SizeProductID)
            {
                return BadRequest();
            }

            _context.Entry(size_Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Size_ProductExists(id))
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

        // POST: api/Size_Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Size_Product>> PostSize_Product(Size_Product size_Product)
        {
            var newSizePro = new Size_Product
            {
                ProductID = size_Product.ProductID,
                SizeID = size_Product.SizeID
            };

            _context.size_product.Add(newSizePro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSize_Product", new { id = size_Product.SizeProductID }, newSizePro);
        }
        private bool Size_ProductExists(int id)
        {
            return _context.size_product.Any(e => e.SizeProductID == id);
        }
    }
}
