using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsRecordController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public VendorsRecordController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from r in _context.vendorsRecords
                          join v in _context.vendors on r.VendorId equals v.VendorId
                          join w in _context.workers on r.GuardId equals w.WorkerId 
                          join u in _context.users on r.PermitById equals u.UserId
                          select new
                          {
                              r.Id,
                              r.VendorId,
                              w.WorkerId,
                              r.EntryTime,
                              r.ExitTime,
                              r.Permission,
                              u.UserId
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VendorsRecord>>> GetById(int id)
        {
            var content = await _context.vendorsRecords.FindAsync(id);
            var result = (from r in _context.vendorsRecords
                          join v in _context.vendors on r.VendorId equals v.VendorId
                          join w in _context.workers on r.GuardId equals w.WorkerId
                          join u in _context.users on r.PermitById equals u.UserId
                          select new
                          {
                              r.Id,
                              r.VendorId,
                              w.WorkerId,
                              r.EntryTime,
                              r.ExitTime,
                              r.Permission,
                              u.UserId
                          }).ToList();
            if (content == null)
            {
                return NotFound("Vendor Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<VendorsRecord>>> Add(VendorsRecord vendor)
        {
            _context.vendorsRecords.Add(vendor);
            await _context.SaveChangesAsync();
            return Ok(vendor);
        }


    }
}
