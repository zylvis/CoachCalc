using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace CoachCalcAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AthleteeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Athletee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Athletee>>> GetAthletees()
        {
          if (_context.Athletees == null)
          {
              return NotFound();
          }

            return await _context.Athletees.ToListAsync();
        }

        // GET: api/Athletee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Athletee>> GetAthletee(string id)
        {
          if (_context.Athletees == null)
          {
              return NotFound();
          }
            var athletee = await _context.Athletees.FindAsync(id);

            if (athletee == null)
            {
                return NotFound();
            }

            return athletee;
        }

        // PUT: api/Athletee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthletee(string id, Athletee athletee)
        {
            if (id != athletee.Id)
            {
                return BadRequest();
            }

            _context.Entry(athletee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Athletee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Athletee>> PostAthletee(Athletee athletee)
        {
          if (_context.Athletees == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Athletees'  is null.");
          }
            _context.Athletees.Add(athletee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAthletee", new { id = athletee.Id }, athletee);
        }

        // DELETE: api/Athletee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAthletee(string id)
        {
            if (_context.Athletees == null)
            {
                return NotFound();
            }
            var athletee = await _context.Athletees.FindAsync(id);
            if (athletee == null)
            {
                return NotFound();
            }

            _context.Athletees.Remove(athletee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AthleteeExists(string id)
        {
            return (_context.Athletees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
