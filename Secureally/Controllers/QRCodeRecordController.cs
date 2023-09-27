using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeRecordController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public QRCodeRecordController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from g in _context.qrCodeRecord
                          join w in _context.workers on g.GuardId equals w.WorkerId
                          join c in _context.qrCode on g.QRCodeId equals c.QRCodeId
                          select new 
                          {
                            g.Id,
                            w.WorkerId,
                            c.QRCodeId,
                            g.Time
                          }).ToList();

            return Ok(result);
        }

        [HttpGet("/QRcode/{id}")]
        public async Task<ActionResult<IEnumerable<QRCodeRecord>>> GetBycodeId(int id,QRCodeRecord code)
        {
            var content = await _context.qrCodeRecord.FindAsync(code.Id);
            var result = (from g in _context.qrCodeRecord
                          join w in _context.workers on g.GuardId equals w.WorkerId
                          join c in _context.qrCode on g.QRCodeId equals c.QRCodeId
                          select new
                          {
                              g.Id,
                              w.WorkerId,
                              c.QRCodeId,
                              g.Time
                          }).ToList();
            if (content == null)
            {
                return NotFound("Record Not Found");
            }
            return Ok(result);
        }
        

        [HttpPost]
        public async Task<ActionResult<IEnumerable<QRCodeRecord>>> Add(QRCodeRecord qrcode)
        {
            _context.qrCodeRecord.Add(qrcode);
            await _context.SaveChangesAsync();
            return Ok(qrcode);
        }



    }

}
