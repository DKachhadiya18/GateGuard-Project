using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestRecordController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public GuestRecordController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {            
            var result = (from g in _context.guestRecords
                          join w in _context.workers on g.GuestId equals w.WorkerId
                          select new
                          {
                              g.GuestId,
                              GuardId = w.WorkerId,
                              g.EntryTime,
                              g.ExitTime
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GuestRecord>>> GetById(int id)
        {
            var content = await _context.guestRecords.FindAsync(id);
            var result = (from g in _context.guestRecords
                          join w in _context.workers on g.GuestId equals w.WorkerId
                          select new
                          {
                              g.GuestId,
                              GuardId = w.WorkerId,
                              g.EntryTime,
                              g.ExitTime
                          }).ToList();
            if (result == null)
            {
                return NotFound("Guest Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<GuestRecord>>> Add(GuestRecord guest)
        {
            _context.guestRecords.Add(guest);
            await _context.SaveChangesAsync();
            return Ok(guest);
        }


    }

}

