using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {

        private readonly SecureallyContext _context;

        public VisitorsController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from v in _context.visitors
                          join h in _context.houses on v.HouseId equals h.HouseId
                          select new
                          {
                              v.VisitorId,
                              v.FirstName,
                              v.LastName,
                              v.PhoneNumber,
                              h.HouseId,
                              v.VehicleNumber
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Society>> GetById(int id)
        {
            var content = await _context.visitors.FindAsync(id);
            var result = (from v in _context.visitors
                          join h in _context.houses on v.HouseId equals h.HouseId
                          select new
                          {
                              v.VisitorId,
                              v.FirstName,
                              v.LastName,
                              v.PhoneNumber,
                              h.HouseId,
                              v.VehicleNumber
                          }).ToList();
            if (content == null)
            {
                return BadRequest("Visitor Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Society>> AddSociety(Visitors visitor)
        {

            _context.visitors.Add(visitor);
            await _context.SaveChangesAsync();
            return Ok(await _context.visitors.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSociety(int id, Visitors visitor)
        {
            if (id != visitor.VisitorId)
            {
                return BadRequest();
            }

            _context.Entry(visitor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorExists(id))
                {
                    return NotFound("Visitor not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }





        [HttpDelete("{id}")]
        public async Task<ActionResult<Visitors>> Delete(int id)
        {
            var content = await _context.visitors.FindAsync(id);
            if (content == null)
            {
                return BadRequest("Visitor Not Found");
            }
            _context.visitors.Remove(content);
            await _context.SaveChangesAsync();
            return Ok(await _context.visitors.ToListAsync());
        }

        private bool VisitorExists(int id)
        {
            return _context.visitors.Any(e => e.VisitorId == id);
        }
    }
}
