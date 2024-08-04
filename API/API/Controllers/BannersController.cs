using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Model;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public BannersController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Banners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banner>>> Getbanner()
        {
            return await _context.banner.ToListAsync();
        }

        // GET: api/Banners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Banner>> GetBanner(int id)
        {
            var banner = await _context.banner.FindAsync(id);

            if (banner == null)
            {
                return NotFound();
            }

            return banner;
        }

        // PUT: api/Banners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBanner(int id, Banner banner)
        {
            if (id != banner.Id)
            {
                return BadRequest();
            }

            _context.Entry(banner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(id))
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

        // Upload file
        [HttpPost("UploadFile")]
        public async Task<ActionResult<Banner>> UploadFile([FromForm] IFormFile file, [FromForm] string title, [FromForm] byte status)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            // Đường dẫn đến thư mục
            var uploadFolder = "D:\\FPoly\\C# 6\\ImageUpload";
            var imageName = Path.GetFileName(file.FileName);
            var imagePath = Path.Combine(uploadFolder, imageName);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Lưu file vào thư mục đã chỉ định
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var newBanner = new Banner
            {
                Image = imageName,
                Title = title,
                Status = status
            };

            _context.banner.Add(newBanner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBanner", new { id = newBanner.Id }, newBanner);
        }


        // POST: api/Banners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Banner>> PostBanner(Banner banner)
        {
            _context.banner.Add(banner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBanner", new { id = banner.Id }, banner);
        }

        // DELETE: api/Banners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var banner = await _context.banner.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }

            _context.banner.Remove(banner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BannerExists(int id)
        {
            return _context.banner.Any(e => e.Id == id);
        }
    }
}
