using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public MembersController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Members>>> GetMembers()
        {
            return Ok(await _context.members.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Members>>> GetMembersById(int id)
        {
            var content = await _context.members.FindAsync(id);
            if (content == null)
            {
                return NotFound("Member Not Found");
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Members>>> AddMembers(Members member)
        {
            _context.members.Add(member);
            await _context.SaveChangesAsync();
            return Ok(member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Members members)
        {
            if (id != members.Id)
            {
                return BadRequest();
            }

            _context.Entry(members).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound("Member not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Members>>> DeleteMembers(int id, Members members)
        {
            try
            {
                var content = await _context.members.FindAsync(id);
                _context.members.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Member Not Found");
            }


        }
        private bool MemberExists(int id)
        {
            return _context.members.Any(e => e.Id == id);
        }

    }
}
