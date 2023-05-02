using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Numerics;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaintesController : ControllerBase
    {
        private readonly BdreservationSalleContext _context;

        public PlaintesController(BdreservationSalleContext context)
        {
            _context = context;
        }

        // GET: api/Plaintes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plainte>>> GetPlaintes()
        {
          if (_context.Plaintes == null)
          {
              return NotFound();
          }
            return await _context.Plaintes.ToListAsync();
        }

        // GET: api/Plaintes/5
        [HttpGet("{noReservation}/{membreCourriel}")]
        public async Task<ActionResult<Plainte>> GetPlainte(int noReservation, string membreCourriel)
        {
            if (_context.Plaintes == null)
            {
              return NotFound();
          }
            var plainte = await _context.Plaintes.FindAsync(noReservation, membreCourriel);

            if (plainte == null)
            {
                return NotFound();
            }

            return plainte;
        }

        // PUT: api/Plaintes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{noReservation}/{membreCourriel}")]
        public async Task<IActionResult> PutPlainte(int noReservation, string membreCourriel, Plainte plainte)
        {
            if (noReservation != plainte.NoReservation)
            {
                return BadRequest();
            }

            _context.Entry(plainte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlainteExists(noReservation, membreCourriel))
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

        // POST: api/Plaintes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plainte>> PostPlainte(Plainte plainte)
        {
          if (_context.Plaintes == null)
          {
              return Problem("Entity set 'BdreservationSalleContext.Plaintes'  is null.");
          }
            _context.Plaintes.Add(plainte);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlainteExists(plainte.NoReservation, plainte.MembreCourriel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlainte", new { noReservation = plainte.NoReservation, membreCourriel = plainte.MembreCourriel }, plainte);
        }

        // DELETE: api/Plaintes/5
        [HttpDelete("{noReservation}/{membreCourriel}")]
        public async Task<IActionResult> DeletePlainte(int noReservation, string membreCourriel)
        {
            if (_context.Plaintes == null)
            {
                return NotFound();
            }
            var plainte = await _context.Plaintes.FindAsync(noReservation, membreCourriel);
            if (plainte == null)
            {
                return NotFound();
            }

            _context.Plaintes.Remove(plainte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlainteExists(int noReservation, string membreCourriel)
        {
            return (_context.Plaintes?.Any(e => e.NoReservation == noReservation && e.MembreCourriel == membreCourriel)).GetValueOrDefault();
        }
    }
}
