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
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Getproduct()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var product = await _context.product
                .Include(u => u.Image)
                .Include(u => u.Categories)
                .ToListAsync();

            var serializedData = JsonSerializer.Serialize(product, options);
            return Content(serializedData, "application/json");
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var product = await _context.product
               .Include(u => u.Image)
               .Include(u => u.Categories)
               .Include(u => u.Size)
               .FirstOrDefaultAsync(u => u.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            var serializedData = JsonSerializer.Serialize(product, options);
            return Content(serializedData, "application/json");
        }

        // GET: api/Products/Cate/5
        [HttpGet("Cate/{cateId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int cateId)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var products = await _context.product
                .Include(u => u.Image)
                .Include(u => u.Categories)
                .Where(u => u.CateID == cateId)
                .ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            var serializedData = JsonSerializer.Serialize(products, options);
            return Content(serializedData, "application/json");
        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product pro)
        {
            if (id != pro.ProductID)
            {
                return BadRequest();
            }

            var product = await _context.product
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            // Update product properties
            product.Name = pro.Name;
            product.Image = pro.Image;
            product.Rate = pro.Rate;
            product.Description1 = pro.Description1;
            product.Description2 = pro.Description2;
            product.CateID = pro.CateID;
            product.StatusID = pro.StatusID;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductVM pro)
        {
            var newProduct = new Product
            {
                Name = pro.Name,
                Rate = pro.Rate,
                Description1 = pro.Description1,
                Description2 = pro.Description2,
                CateID = pro.CateID,
                StatusID = pro.StatusID
            };

            _context.product.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = newProduct.ProductID }, newProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.product.Any(e => e.ProductID == id);
        }

    }
}
