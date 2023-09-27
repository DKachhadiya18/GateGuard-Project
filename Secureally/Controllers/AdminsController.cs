using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Secureally.Data;
using Secureally.Models;
using System.Linq;

namespace Secureally.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminsController :  ControllerBase
    {
        private readonly SecureallyContext _context;
        public AdminsController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() {

            var result = (from a in _context.admins
                          join s in _context.society
                          on a.SocietyId equals s.SocietyId
                          select new
                          {
                              a.UserId,
                              a.UserName,                             
                              s.SocietyId,
                              a.Password
                          }).ToList();

            return Ok(result);         
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Admins>>> GetById(int id)
        {
            var content = await _context.admins.FindAsync(id);
            var result = (from a in _context.admins
                          join s in _context.society
                          on a.SocietyId equals s.SocietyId
                          select new 
                          {
                              a.UserId,
                              a.UserName,
                              s.StateId,
                              a.Password
                          }).ToList();
            if (content == null)
            {
                return NotFound("Admin Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Admins>>> Add(Admins admins)
        {
            
            _context.admins.Add(admins);           
            await _context.SaveChangesAsync();
            return Ok("Admin Added Successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Admins>>> Edit(int id, Admins admins)
        {
            if (id != admins.UserId)
            {
                return NotFound("Admin Not Found");
            }
                       
            _context.Entry(admins).State = EntityState.Modified;
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
        public async Task<ActionResult<IEnumerable<Admins>>> Delete(int id, Admins admins)
        {
            try
            {
                var content = await _context.admins.FindAsync(id);
                _context.admins.Remove(content);
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
            return _context.admins.Any(e => e.UserId == id);
        }
    }
}