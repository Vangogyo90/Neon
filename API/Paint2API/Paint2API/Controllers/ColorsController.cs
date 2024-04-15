using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paint2API.Models;

namespace Paint2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly PaintContext _context;

        public ColorsController(PaintContext context)
        {
            _context = context;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
          if (_context.Colors == null)
          {
              return NotFound();
          }
            return await _context.Colors.ToListAsync();
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(int? id)
        {
          if (_context.Colors == null)
          {
              return NotFound();
          }
            var color = await _context.Colors.FindAsync(id);

            if (color == null)
            {
                return NotFound();
            }

            return color;
        }

        [HttpGet("GetData")]
        public IActionResult GetData()
        {
            if (_context.Colors == null)
            {
                return NotFound();
            }

            List<Color> data = _context.Colors
                .Include(a => a.TypeApplications)
                .Include(a => a.TempPulverizations)
                .Include(a => a.Shines)
                .Include(a => a.TypeSurfaces)
                .Include(a => a.RalCatalog)
                .ToList();

            return Ok(data);
        }

        // PUT: api/Colors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "3")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(int? id, Color color)
        {
            if (id != color.IdColor)
            {
                return BadRequest();
            }

            _context.Entry(color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
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

        [Authorize(Roles = "3")]
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color)
        {
            if (_context.Colors == null)
            {
                return Problem("Entity set 'PaintContext.Colors' is null.");
            }

            if (_context.Colors.Any(c =>
                c.Priority == color.Priority &&
                c.TypeApplicationId == color.TypeApplicationId &&
                c.TempPulverizationId == color.TempPulverizationId &&
                c.ShineId == color.ShineId &&
                c.TypeSurfaceId == color.TypeSurfaceId &&
                c.RalCatalogId == color.RalCatalogId))
            {
                return Conflict("A color with the same properties already exists.");
            }

            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColor", new { id = color.IdColor }, color);
        }


        // DELETE: api/Colors/5
        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int? id)
        {
            if (_context.Colors == null)
            {
                return NotFound();
            }
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorExists(int? id)
        {
            return (_context.Colors?.Any(e => e.IdColor == id)).GetValueOrDefault();
        }
    }
}
