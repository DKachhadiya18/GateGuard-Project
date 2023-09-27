using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : ControllerBase
    {

        private readonly SecureallyContext _context;
        public HousesController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from b in _context.blocks
                          join h in _context.houses on b.BlockId equals h.BlockId
                          join s in _context.society on b.SocietyId equals s.SocietyId
                          select new
                          {
                              s.SocietyId,
                              b.BlockId,
                              b.Block,
                              h.HouseId,
                              h.House
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Houses>>> GetById(int id)
        {
            var content = await _context.houses.FindAsync(id);
            var result = (from b in _context.blocks
                          join h in _context.houses on b.BlockId equals h.BlockId
                          join s in _context.society on b.SocietyId equals s.SocietyId
                          select new
                          {
                              s.SocietyId,
                              b.BlockId,
                              b.Block,
                              h.HouseId,
                              h.House
                          }).ToList();
            if (content == null)
            {
                return NotFound("House Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Houses>>> Add(int houseId,Houses houses)
        {
            
            _context.houses.Add(houses);
            await _context.SaveChangesAsync();
            return Ok(houses);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Houses houses)
        {
            if (id != houses.HouseId)
            {
                return BadRequest();
            }

            _context.Entry(houses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HouseExists(id))
                {
                    return NotFound("House not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Houses>>> Delete(int id, Houses houses)
        {
            try
            {
                var content = await _context.houses.FindAsync(id);
                _context.houses.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("House Not Found");
            }


        }

        private bool HouseExists(int id)
        {
            return _context.houses.Any(e => e.HouseId == id);
        }

    }
}