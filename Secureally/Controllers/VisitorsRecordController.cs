using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsRecordController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public VisitorsRecordController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from r in _context.visitorsRecords
                          join v in _context.visitors on r.VisitorId equals v.VisitorId
                          join w in _context.workers on r.GuardId equals w.WorkerId
                          join u in _context.users on r.PermitById equals u.UserId
                          select new
                          {
                              r.Id,
                              r.VisitorId,
                              w.WorkerId,
                              r.EntryTime,
                              r.ExitTime,
                              r.Permission,
                              u.UserId
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VisitorsRecord>>> GetById(int id, VisitorsRecord visitors)
        {
            var content = await _context.visitorsRecords.FindAsync(id);
            var result = (from r in _context.visitorsRecords
                          join v in _context.visitors on r.VisitorId equals v.VisitorId
                          join w in _context.workers on r.GuardId equals w.WorkerId
                          join u in _context.users on r.PermitById equals u.UserId
                          select new
                          {
                              r.Id,
                              r.VisitorId,
                              w.WorkerId,
                              r.EntryTime,
                              r.ExitTime,
                              r.Permission,
                              u.UserId
                          }).ToList();
            if (content == null)
            {
                return NotFound("Visitor Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<VisitorsRecord>>> Add(VisitorsRecord visitor)
        {
            _context.visitorsRecords.Add(visitor);
            await _context.SaveChangesAsync();
            return Ok(visitor);
        }

    }
}
