using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public VendorsController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from v in _context.vendors
                          join h in _context.houses on v.HouseId equals h.HouseId
                          select new
                          {
                              v.VendorId,
                              v.FirstName,
                              v.LastName,
                              v.PhoneNumber,
                              h.HouseId,
                              v.Category,
                              v.Company
                          }).ToList();  
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Vendors>>> GetById(int id)
        {
            var content = await _context.vendors.FindAsync(id);
            var result = (from v in _context.vendors
                          join h in _context.houses on v.HouseId equals h.HouseId
                          select new
                          {
                              v.VendorId,
                              v.FirstName,
                              v.LastName,
                              v.PhoneNumber,
                              h.HouseId,
                              v.Category,
                              v.Company
                          }).ToList();
            if (content == null)
            {
                return NotFound("Vendors Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Vendors>>> Add(Vendors vendor)
        {
            _context.vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return Ok(vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Vendors vendor)
        {
            if (id != vendor.VendorId)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
                {
                    return NotFound("Vendor not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Vendors>>> Delete(int id, Vendors vendor)
        {
            try
            {
                var content = await _context.vendors.FindAsync(id);
                _context.vendors.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Vendor Not Found");
            }


        }
        private bool VendorExists(int id)
        {
            return _context.vendors.Any(e => e.VendorId == id);

        }
    }
}
