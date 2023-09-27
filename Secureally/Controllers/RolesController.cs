using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public RolesController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> Get()
        {
            return Ok(await _context.roles.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Roles>>> GetById(int id)
        {
            var content = await _context.roles.FindAsync(id);
            if (content == null)
            {
                return NotFound("Role Not Found");
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Roles>>> Add(Roles role)
        {
            _context.roles.Add(role);
            await _context.SaveChangesAsync();
            return Ok(role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Roles role)
        {
            if (id != role.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound("Role not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Roles>>> Delete(int id, Roles role)
        {
            try
            {
                var content = await _context.roles.FindAsync(id);
                _context.roles.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Role Not Found");
            }


        }
        private bool RoleExists(int id)
        {
            return _context.roles.Any(e => e.RoleId == id);
        }


    }
}
