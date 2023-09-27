using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;
namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyController : ControllerBase
    {
        private readonly Data.SecureallyContext _context;

        public SocietyController(Data.SecureallyContext context)
        {            
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = from s in _context.society
                         join t in _context.states on s.StateId equals t.StateId
                         join c in _context.cities on s.CityId equals c.CityId
                         select new
                         {
                             s.SocietyId,
                             s.SocietyName,
                             s.Address,
                             s.Area,
                             c.CityName,
                             t.StateName
                         };
            
            return Ok(_context.society.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Society>> GetSocietyById(int id)
        {
            var content = await _context.society.FindAsync(id);
            var result = from s in _context.society
                         join t in _context.states on s.StateId equals t.StateId
                         join c in _context.cities on s.CityId equals c.CityId
                         where content.SocietyId == id  
                         select new
                         {
                             s.SocietyId,
                             s.SocietyName,
                             s.Address,
                             s.Area,
                             c.CityName,
                             t.StateName
                         };
            if (content == null)
            {
                return BadRequest("Society Not Found");
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<Society>> AddSociety(Society society)
        {
            var result = from s in _context.society
                         join t in _context.states on s.StateId equals t.StateId
                         join c in _context.cities on s.CityId equals c.CityId
                         select new
                         {
                             s.SocietyId,
                             s.SocietyName,
                             s.Address,
                             s.Area,
                             c.CityName,
                             t.StateName
                         };
            _context.society.Add(society);
            await _context.SaveChangesAsync();
            return Ok(await _context.society.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSociety(int id, Society society)
        {
            if (id != society.SocietyId)
            {
                return BadRequest();
            }

            _context.Entry(society).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound("Society not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Society>> DeleteSociety(int id)
        {
            var content = await _context.society.FindAsync(id);
            if (content == null)
            {
                return BadRequest("Society Not Found");
            }
            _context.society.Remove(content);
            await _context.SaveChangesAsync();
            return Ok(await _context.society.ToListAsync());
        }
        private bool MemberExists(int id)
        {
            return _context.members.Any(e => e.Id == id);
        }
    }
}
