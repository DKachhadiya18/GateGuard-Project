using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public WorkersController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from w in _context.workers
                          join h in _context.houses on w.HouseId equals h.HouseId
                          select new
                          {
                              w.WorkerId,
                              w.FirstName,
                              w.LastName,
                              w.PhoneNumber,
                              w.Category,
                              h.HouseId,
                              w.VehicleNumber,
                              w.Picture
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Workers>>> GetById(int id)
        {
            var content = await _context.workers.FindAsync(id);
            var result = (from w in _context.workers
                          join h in _context.houses on w.HouseId equals h.HouseId
                          select new
                          {
                              w.WorkerId,
                              w.FirstName,
                              w.LastName,
                              w.PhoneNumber,
                              w.Category,
                              h.HouseId,
                              w.VehicleNumber,
                              w.Picture
                          }).ToList();
            if (content == null)
            {
                return NotFound("Worker Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Workers>>> Add(Workers worker)
        {
            _context.workers.Add(worker);
            await _context.SaveChangesAsync();
            return Ok(worker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Workers worker)
        {
            if (id != worker.WorkerId)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkersExists(id))
                {
                    return NotFound("Worker not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Workers>>> Delete(int id, Workers worker)
        {
            try
            {
                var content = await _context.workers.FindAsync(id);
                _context.workers.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Workers Not Found");
            }


        }
        private bool WorkersExists(int id)
        {
            return _context.workers.Any(e => e.WorkerId == id);

        }

    }
}
