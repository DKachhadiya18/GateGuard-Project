using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly SecureallyContext _context;

        public CitiesController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var result = (from c in _context.cities
                          join s in _context.states
                          on c.StateId equals s.StateId
                          select new
                          {
                              s.StateId,
                              c.CityId,
                              s.StateName,
                              c.CityName
                          }).ToList();
   
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cities>>> GetCitiesById(int id)
        {
            var content = await _context.cities.FindAsync(id);
            var result=(from c in _context.cities
                       join s in _context.states
                       on c.StateId equals s.StateId
                       select new
                       {
                         s.StateId,
                         c.CityId,
                         s.StateName,
                         c.CityName
                        }).ToList();

            if (content == null)
            {
                return NotFound("Cities Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Cities>>> AddCities(Cities cities)
        {
            _context.cities.Add(cities);
            await _context.SaveChangesAsync();
            return Ok(cities);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCities(int id, Cities cities)
        {
            if (id != cities.CityId)
            {
                return BadRequest();
            }

            _context.Entry(cities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitiesExists(id))
                {
                    return NotFound("Cities not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Cities>>> DeleteCities(int id, Cities cities)
        {
            try
            {
                var content = await _context.cities.FindAsync(id);
                _context.cities.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Cities Not Found");
            }


        }

        private bool CitiesExists(int id)
        {
            return _context.cities.Any(e => e.CityId == id);
        }

    }
}

