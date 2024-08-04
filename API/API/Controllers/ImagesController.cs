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
    public class ImagesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ImagesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> Getimage()
        {
            return await _context.image.ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var image = await _context.image.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, Image image)
        {
            if (id != image.ImageID)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("UploadFile")]
        public async Task<ActionResult<Image>> UploadFile(IFormFile file, [FromForm] int productId)
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

            var newImg = new Image
            {
                Name = imageName,
                ProductID = productId
            };

            _context.image.Add(newImg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = newImg.ImageID }, newImg);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            // Tìm hình ảnh trong cơ sở dữ liệu
            var image = await _context.image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            // Xóa tệp hình ảnh khỏi thư mục
            var uploadFolder = "D:\\FPoly\\C# 6\\ImageUpload";
            var imagePath = Path.Combine(uploadFolder, image.Name);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            // Xóa hình ảnh khỏi cơ sở dữ liệu
            _context.image.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ImageExists(int id)
        {
            return _context.image.Any(e => e.ImageID == id);
        }
    }
}
