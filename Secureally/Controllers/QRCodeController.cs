using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public QRCodeController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QRCode>>> Get()
        {
            return Ok(await _context.qrCode.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<QRCode>>> GetById(int id)
        {
            var content = await _context.qrCode.FindAsync(id);
            if (content == null)
            {
                return NotFound("Code Not Found");
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<QRCode>>> Add(QRCode qrcode)
        {
            try
            {
                _context.qrCode.Add(qrcode);
                await _context.SaveChangesAsync();
                return Ok(qrcode);
            }
            catch
            {
                return BadRequest("Error Adding Data");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, QRCode qrcode)
        {
            if (id != qrcode.QRCodeId)
            {
                return BadRequest();
            }

            _context.Entry(qrcode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QRCodeExists(id))
                {
                    return NotFound("Code not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<QRCode>>> Delete(int id, QRCode qrcode)
        {
            try
            {
                var content = await _context.qrCode.FindAsync(id);
                _context.qrCode.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Deleted Successfully");
            }
            catch
            {
                return BadRequest("Code Not Found");
            }


        }
        private bool QRCodeExists(int id)
        {
            return _context.qrCode.Any(e => e.QRCodeId == id);
        }

    }
}
