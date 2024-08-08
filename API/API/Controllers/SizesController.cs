using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SizesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Sizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size>>> Getsize()
        {
            return await _context.size.ToListAsync();
        }

        // GET: api/Sizes1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Size>> GetSize(int id)
        {
            var size = await _context.size.FindAsync(id);

            if (size == null)
            {
                return NotFound();
            }

            return size;
        }

        private bool SizeExists(int id)
        {
            return _context.size.Any(e => e.SizeID == id);
        }
    }
}
