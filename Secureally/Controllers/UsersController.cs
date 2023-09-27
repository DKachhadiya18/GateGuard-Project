using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public UsersController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from h in _context.houses
                          join u in _context.users on h.HouseId equals u.HouseId
                          join b in _context.blocks on h.BlockId equals b.BlockId
                          select new
                          {
                              u.UserId,
                              u.FirstName,
                              u.LastName,
                              u.PhoneNumber,
                              u.EmailId,
                              h.HouseId
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Users>>> GetById(int id)
        {
            var content = await _context.users.FindAsync(id);
            var result = (from h in _context.houses
                          join u in _context.users on h.HouseId equals u.HouseId
                          join b in _context.blocks on h.BlockId equals b.BlockId
                          select new
                          {
                              u.UserId,
                              u.FirstName,
                              u.LastName,
                              u.PhoneNumber,
                              u.EmailId,
                              h.HouseId
                          }).ToList();
            if (content == null)
            {
                return NotFound("User Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Users>>> Add(Users user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Users user)
        {
            if (id != user.UserId)
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
                    return NotFound("User not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Users>>> Delete(int id, Users user)
        {
            try
            {
                var content = await _context.users.FindAsync(id);
                _context.users.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("User Not Found");
            }


        }
        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.UserId == id);

        }

    }
}
