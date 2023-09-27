using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersRecordController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public WorkersRecordController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from r in _context.workersRecord
                          join w in _context.workers on r.WorkerId equals w.WorkerId
                          select new
                          {
                              r.id,
                              w.WorkerId,
                              r.EntryTime,
                              r.ExitTime
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<WorkersRecord>>> GetById(int id)
        {
            var content = await _context.workersRecord.FindAsync(id);
            var result = (from r in _context.workersRecord
                          join w in _context.workers on r.WorkerId equals w.WorkerId
                          select new
                          {
                              r.id,
                              w.WorkerId,
                              r.EntryTime,
                              r.ExitTime
                          }).ToList();
            if (content == null)
            {
                return NotFound("Worker Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<WorkersRecord>>> Add(WorkersRecord worker)
        {
            _context.workersRecord.Add(worker);
            await _context.SaveChangesAsync();
            return Ok(worker);
        }
    }
}

