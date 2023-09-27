using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public UserRoleController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from u in _context.userRole
                          join r in _context.roles on u.RoleId equals r.RoleId
                          join s in _context.users on u.UserId equals s.UserId
                          select new
                          {
                              u.Id,
                              r.RoleId,
                              s.UserId
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetById(int id)
        {
            var content = await _context.userRole.FindAsync(id);
            var result = (from u in _context.userRole
                          join r in _context.roles on u.RoleId equals r.RoleId
                          join s in _context.users on u.UserId equals s.UserId
                          select new
                          {
                              u.Id,
                              r.RoleId,
                              s.UserId
                          }).ToList();
            if (content == null)
            {
                return NotFound("Not Found");
            }
            return Ok(result);
        }
        
        

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserRole>>> Add(UserRole user)
        {
            _context.userRole.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }



        ////put by role id
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, UserRole user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound("Not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }


        //Delete by UserId
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<UserRole>>> Delete(int id, UserRole user)
        {
            try
            {
                var content = await _context.userRole.FindAsync(id);
                _context.userRole.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Not Found");
            }
        }


        private bool UserExists(int id)
        {
            return _context.userRole.Any(e => e.UserId == id);

        }
        private bool RoleExists(int id)
        {
            return _context.userRole.Any(e => e.RoleId == id);

        }



    }
}
