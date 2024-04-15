using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoPartsAPI.Hash;
using AutoPartsAPI.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paint2API.Models;

namespace Paint2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PaintContext _context;

        public UsersController(PaintContext context)
        {
            _context = context;
        }

        [HttpGet("GetUserWithRole")]
        public IActionResult GetUserWithRole()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.Include(a => a.Roles).ToList();

            return Ok(user);
        }

        [HttpGet("RoleByLogin/{Login}")]
        public async Task<ActionResult<int>> GetRoleByLogin(string Login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Login == Login);
            if (user == null)
            {
                return BadRequest("Invalid Login");
            }
            return user.RoleId;
        }

        [HttpGet("VerificationbyLogin/{Login}")]
        public async Task<ActionResult<bool>> VerificationbyLogin(string Login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Login == Login);
            if (user == null)
            {
                return BadRequest("Invalid Login");
            }
            return user.Verification;
        }

        [HttpGet("UserIDByLogin/{Login}")]
        public async Task<ActionResult<int>> UserIDByLogin(string Login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Login == Login);
            if (user == null)
            {
                return BadRequest("Invalid Login");
            }
            return user.IdUser;
        }

        [HttpGet("{Login}/{Password}")]
        public async Task<ActionResult<string>> GetTokenAuth(string Login, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Login == Login);

            if (user == null)
            {
                return BadRequest("Invalid Login");
            }

            byte[] saltbytes = Convert.FromBase64String(user.Salt);
            byte[] PasswordBytes = Encoding.UTF8.GetBytes(Password);

            Password = Convert.ToBase64String(HashWithSalt.GenerateSaltedHash(PasswordBytes, saltbytes));

            if (user.Password != Password)
            {
                return BadRequest("Invalid Password");
            }

            string token;
            Models.Token trueToken;

            trueToken = await _context.Tokens.FirstOrDefaultAsync(p => p.UserId == user.IdUser);

            if (trueToken == null)
            {
                token = NewToken.GetToken(Login, user.RoleId.ToString());
                Models.Token tokk = new Models.Token();
                tokk.Token1 = token;
                tokk.UserId = user.IdUser;
                _context.Tokens.Add(tokk);
                await _context.SaveChangesAsync();
            }
            else
                token = trueToken.Token1;

            return token;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int? id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int? id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            byte[] Salt = GetSalt(20);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(user.Password);
            user.Salt = Convert.ToBase64String(Salt);
            user.Password = Convert.ToBase64String(HashWithSalt.GenerateSaltedHash(passwordBytes, Salt));

            if (_context.Users == null)
            {
                return Problem("Entity set 'Paint.Users'  is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int? id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
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
