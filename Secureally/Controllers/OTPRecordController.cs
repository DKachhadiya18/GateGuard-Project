using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPRecordController : ControllerBase
    {
        private readonly SecureallyContext _context;
        public OTPRecordController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = (from o in _context.otpRecord
                          join u in _context.users
                          on o.UserId equals u.UserId
                          select new
                          {
                              o.Id,
                              u.UserId,
                              o.PhoneNumber,
                              o.EmailID,
                              o.Otp,
                              o.RequestTime,
                              o.SentTime
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, OTPRecord otp)
        {
            var content =  _context.otpRecord.FindAsync(otp.UserId);
            var result = (from o in _context.otpRecord
                          join u in _context.users
                          on o.UserId equals u.UserId
                          select new
                          {
                              o.Id,
                              u.UserId,
                              o.PhoneNumber,
                              o.EmailID,
                              o.Otp,
                              o.RequestTime,
                              o.SentTime
                          }).ToList();
            if (content == null)
            {
                return NotFound("Record Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<OTPRecord>>> Add(OTPRecord oTPRecord)
        {
            _context.otpRecord.Add(oTPRecord);
            await _context.SaveChangesAsync();
            return Ok(oTPRecord);
        }


       

    }

}

