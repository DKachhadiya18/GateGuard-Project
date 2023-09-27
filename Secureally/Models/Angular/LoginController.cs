using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;

namespace Secureally.Models.Angular
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public LoginController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.login.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Login>>> GetById(int id)
        {
            var content = await _context.login.FindAsync(id);
           
            if (content == null)
            {
                return NotFound("Admin Not Found");
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Login>>> Add(Login login)
        {

            _context.login.Add(login);
            await _context.SaveChangesAsync();
            return Ok("Admin Added Successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Login>>> Edit(int id, Login login)
        {
            if (id != login.Id)
            {
                return NotFound("Admin Not Found");
            }

            _context.Entry(login).State = EntityState.Modified;
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
        public async Task<ActionResult<IEnumerable<Login>>> Delete(int id, Login login)
        {
            try
            {
                var content = await _context.login.FindAsync(id);
                _context.login.Remove(content);
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
            return _context.login.Any(e => e.Id == id);
        }
    }
}