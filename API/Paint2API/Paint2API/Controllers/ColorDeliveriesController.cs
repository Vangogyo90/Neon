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
    public class ColorDeliveriesController : ControllerBase
    {
        private readonly PaintContext _context;

        public ColorDeliveriesController(PaintContext context)
        {
            _context = context;
        }

        // GET: api/ColorDeliveries
        [Authorize(Roles = "2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorDelivery>>> GetColorDeliveries()
        {
          if (_context.ColorDeliveries == null)
          {
              return NotFound();
          }
            return await _context.ColorDeliveries.ToListAsync();
        }

        [HttpGet("GetData/{deliveryID}")]
        public IActionResult GetData(int deliveryID)
        {
            if (_context.ColorDeliveries == null)
            {
                return NotFound();
            }

            IQueryable<ColorDelivery> data = _context.ColorDeliveries
                .Include(a => a.Colors.TypeApplications)
                .Include(a => a.Colors.TempPulverizations)
                .Include(a => a.Colors.Shines)
                .Include(a => a.Colors.TypeSurfaces)
                .Include(a => a.Colors.RalCatalog);


            if (deliveryID > 0)
            {
                data = data.Where(delivery => delivery.DeliveryId == deliveryID);
            }

            var result = data.ToList()
    .GroupBy(a => a.ColorId)
    .Select(g => new ColorDelivery
    {
        ColorId = g.Key,
        Quantity = g.First().Quantity_Of_Color,
        Quantity_Of_Color = g.First().Quantity_Of_Color,
        Colors = g.First().Colors,
        IdColorDelivery = g.First().IdColorDelivery,
        DeliveryId = g.First().DeliveryId,
        Deliverys = g.First().Deliverys
    });

            return Ok(result);
        }

        // GET: api/ColorDeliveries/5
        [Authorize(Roles = "6")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorDelivery>> GetColorDelivery(int? id)
        {
          if (_context.ColorDeliveries == null)
          {
              return NotFound();
          }
            var colorDelivery = await _context.ColorDeliveries.FindAsync(id);

            if (colorDelivery == null)
            {
                return NotFound();
            }

            return colorDelivery;
        }

        // PUT: api/ColorDeliveries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "2")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColorDelivery(int? id, ColorDelivery colorDelivery)
        {
            if (id != colorDelivery.IdColorDelivery)
            {
                return BadRequest();
            }

            _context.Entry(colorDelivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorDeliveryExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ColorDelivery>> PostColorDelivery(ColorDelivery colorDelivery)
        {
          if (_context.ColorDeliveries == null)
          {
              return Problem("Entity set 'PaintContext.ColorDeliveries'  is null.");
          }
            _context.ColorDeliveries.Add(colorDelivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColorDelivery", new { id = colorDelivery.IdColorDelivery }, colorDelivery);
        }

        // DELETE: api/ColorDeliveries/5
        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColorDelivery(int? id)
        {
            if (_context.ColorDeliveries == null)
            {
                return NotFound();
            }
            var colorDelivery = await _context.ColorDeliveries.FindAsync(id);
            if (colorDelivery == null)
            {
                return NotFound();
            }

            _context.ColorDeliveries.Remove(colorDelivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "3")]
        [HttpDelete("DeleteColorDeliveryByColorId/{id}")]
        public async Task<IActionResult> DeleteColorDeliveryByColorId(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id is null");
            }

            var colorDeliveries = await _context.ColorDeliveries.Where(a => a.ColorId == id).ToListAsync();

            if (colorDeliveries == null || colorDeliveries.Count == 0)
            {
                return NotFound();
            }

            foreach (var colorDelivery in colorDeliveries)
            {
                _context.ColorDeliveries.Remove(colorDelivery);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorDeliveryExists(int? id)
        {
            return (_context.ColorDeliveries?.Any(e => e.IdColorDelivery == id)).GetValueOrDefault();
        }
    }
}
