using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public GuestsController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from g in _context.guests
                         join u in _context.users on g.UserId equals u.UserId
                         join r in _context.guestRecords on g.GuestId equals r.GuestId
                         select new
                         {
                             g.GuestId,
                             g.UserId,
                             g.FirstName,
                             g.LastName,
                             g.PhoneNumber,
                             g.EstimatedDate,
                             r.EntryTime,
                             r.ExitTime
                         }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Guests>>> GetById(int id)
        {
            var content = await _context.guests.FindAsync(id);
            var result = (from g in _context.guests
                          join u in _context.users on g.UserId equals u.UserId
                          join r in _context.guestRecords on g.GuestId equals r.GuestId
                          select new
                          {
                              g.GuestId,
                              g.UserId,
                              g.FirstName,
                              g.LastName,
                              g.PhoneNumber,
                              g.EstimatedDate,
                              r.EntryTime,
                              r.ExitTime
                          }).ToList();
            if (result == null)
            {
                return NotFound("Guest Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Guests>> Add([FromBody] Guests guests)
        {
            _context.guests.Add(guests);
             _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Guests guests)
        {
            if (id != guests.GuestId)
            {
                return BadRequest();
            }

            _context.Entry(guests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
                {
                    return NotFound("Guest not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Guests>>> Delete(int id, Guests guests)
        {
            try
            {
                var content = await _context.guests.FindAsync(id);
                _context.guests.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Guest Not Found");
            }


        }
        private bool GuestExists(int id)
        {
            return _context.guests.Any(e => e.GuestId == id);
        }


    }
}