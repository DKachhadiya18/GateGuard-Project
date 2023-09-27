using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public StatesController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<States>>> Get()
        {
            return Ok(await _context.states.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<States>>> GetById(int id)
        {
            var content = await _context.states.FindAsync(id);
            if (content == null)
            {
                return NotFound("State Not Found");
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<States>>> Add(States state)
        {
            _context.states.Add(state);
            await _context.SaveChangesAsync();
            return Ok(state);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, States state)
        {
            if (id != state.StateId)
            {
                return BadRequest();
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
                {
                    return NotFound("State not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<States>>> Delete(int id, States state)
        {
            try
            {
                var content = await _context.states.FindAsync(id);
                _context.states.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("State Not Found");
            }


        }
        private bool StateExists(int id)
        {
            return _context.states.Any(e => e.StateId == id);

        }

    }
}