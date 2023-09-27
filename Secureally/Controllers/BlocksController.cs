using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Secureally.Data;
using Secureally.Models;

namespace Secureally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly SecureallyContext _context; 
        public BlocksController(SecureallyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBlocks()
        {
            var result = (from b in _context.blocks
                         join h in _context.houses on b.BlockId equals h.BlockId
                         join s in _context.society on b.SocietyId equals s.SocietyId
                         select new
                         {
                             s.SocietyId,
                             b.BlockId,
                             b.Block,
                             h.HouseId,
                             h.House
                         }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Blocks>>> GetBlockById(int id)
        {
            var content = await _context.blocks.FindAsync(id);
            var result = (from b in _context.blocks
                         join h in _context.houses on b.BlockId equals h.BlockId
                         join s in _context.society on b.SocietyId equals s.SocietyId
                         where b.BlockId==id
                         select new
                         {
                             s.SocietyId,
                             b.BlockId,
                             b.Block,
                             h.HouseId,
                             h.House
                         }).ToList();
            if(content == null)
            {
                return NotFound("Block Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Blocks>>> AddBlocks(int blockId ,Blocks blocks)
        {
            Blocks b= new Blocks();
            Houses h = new Houses();

            _context.blocks.Add(blocks);
           // _context.houses.Add(house);
            await _context.SaveChangesAsync();
            return Ok(blocks);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Blocks>>> EditBLocks(int id, Blocks blocks)
        {
            if(id != blocks.BlockId)
            {
                return NotFound("Block Not Found");
            }
            _context.Entry(blocks).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!BlocksExists(id))
                {
                    return NotFound("Block Not Found");
                }
                else
                {
                    throw;
                }
            }
            return Ok("Updated Successfully");            
        }

      

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Blocks>>> DeleteBlocks(int id, Blocks blocks)
        {
            try
            {
                var content = await _context.blocks.FindAsync(id); 
                _context.blocks.Remove(content);
                await _context.SaveChangesAsync();
                return Ok("Block Deleted Successfully");   
            }
            catch
            {
                return NotFound("Block Not Found");
            }             
            
            
        }


        private bool BlocksExists(int id)
        {
            return _context.blocks.Any(e => e.BlockId == id);
        } 
    }
}
