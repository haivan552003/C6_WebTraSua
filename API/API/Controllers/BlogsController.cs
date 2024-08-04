﻿using System;
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
    public class BlogsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public BlogsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> Getblog()
        {
            return await _context.blog.ToListAsync();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _context.blog.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
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

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            _context.blog.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.blog.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("UploadFile")]
        public async Task<ActionResult<Blog>> UploadFile([FromForm] IFormFile file, [FromForm] string title, [FromForm] string decription)
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

            var newBanner = new Blog
            {
                Image = imageName,
                Title = title,
                Decription = decription
            };

            _context.blog.Add(newBanner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("blog", new { id = newBanner.Id }, newBanner);
        }
        private bool BlogExists(int id)
        {
            return _context.blog.Any(e => e.Id == id);
        }
    }
}
