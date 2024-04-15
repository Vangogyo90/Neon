using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoPartsAPI.Hash;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paint2API.Models;

namespace Paint2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly PaintContext _context;

        public DeliveriesController(PaintContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            if (_context.Deliveries == null)
            {
                return NotFound();
            }
            return await _context.Deliveries.ToListAsync();
        }

        [HttpGet("GetData")]
        public IActionResult GetData()
        {
            if (_context.Deliveries == null)
            {
                return NotFound();
            }

            List<Delivery> data = _context.Deliveries
                .Include(a => a.Cites)
                .Include(a => a.Users)
                .Include(a => a.StatusDeliveres)
                .ToList();

            return Ok(data);
        }

        [HttpGet("GetDataByUser")]
        public IActionResult GetDataByUser(int idUser)
        {
            if (_context.Deliveries == null)
            {
                return NotFound();
            }

            IQueryable<Delivery> data = _context.Deliveries
                .Include(a => a.Cites)
                .Include(a => a.Users)
                .Include(a => a.StatusDeliveres);

            if (idUser > 0)
            {
                data = data.Where(delivery => delivery.UserId == idUser);
            }

            return Ok(data.ToList());
        }

        [HttpGet("GetDeliveryByStatus/{status}")]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveryByStatus(string status)
        {

            if (_context.Deliveries == null)
            {
                return NotFound();
            }

            if (status.Equals("All"))
                return await _context.Deliveries.Include(a => a.Cites).Include(a => a.Users).Include(a => a.StatusDeliveres).ToListAsync();
            else
                return await _context.Deliveries.Include(a => a.Cites).Include(a => a.Users).Include(a => a.StatusDeliveres)
                    .Where(a => a.StatusDeliveres.NameStatusOrder.Equals(status)).ToListAsync();
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int? id)
        {
            if (_context.Deliveries == null)
            {
                return NotFound();
            }
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }
            else
                delivery.Adress = HashWithSalt.Encrypt(delivery.Adress);

            return delivery;
        }

        // PUT: api/Deliveries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int? id, Delivery delivery)
        {
            if (id != delivery.IdDelivery)
            {
                return BadRequest();
            }

            _context.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
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
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {
            if (_context.Deliveries == null)
            {
                return Problem("Entity set 'PaintContext.Deliveries'  is null.");
            }

            delivery.Salt = Convert.ToBase64String(GetSalt(20));
            delivery.Adress = HashWithSalt.Decrypt(delivery.Adress);  

            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelivery", new { id = delivery.IdDelivery }, delivery);
        }

        // DELETE: api/Deliveries/5
        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int? id)
        {
            if (_context.Deliveries == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryExists(int? id)
        {
            return (_context.Deliveries?.Any(e => e.IdDelivery == id)).GetValueOrDefault();
        }

        public static byte[] GetSalt(int length)
        {
            byte[] salt = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
