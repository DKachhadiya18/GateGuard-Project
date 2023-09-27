using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Angular;
using Secureally.Data;

namespace Secureally.Models.Angular
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public RegisterController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {           
            return Ok(_context.register.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Admins>>> GetById(int id)
        {
            var content = await _context.admins.FindAsync(id);
            
            if (content == null)
            {
                return NotFound("Admin Not Found");
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Register>>> Add(Register signup)
        {

            _context.register.Add(signup);
            await _context.SaveChangesAsync();
            return Ok("Admin Registered Successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Register>>> Edit(int id, Register register)
        {
            if (id != register.Id)
            {
                return NotFound("Admin Not Found");
            }

            _context.Entry(register).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminsExists(id))
                {
                    return NotFound("Admin Not Found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Register>>> Delete(int id)
        { 
            try
            {
                var content = await _context.register.FindAsync(id);
                _context.register.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Admin Deleted Successfully");
            }
            catch
            {
                return NotFound("Admin Not Found");
            }


        }


        private bool AdminsExists(int id)
        {
            return _context.register.Any(e => e.Id == id);
        }
    }
}

