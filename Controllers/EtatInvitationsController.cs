using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtatInvitationsController : ControllerBase
    {
        private readonly BdreservationSalleContext _context;

        public EtatInvitationsController(BdreservationSalleContext context)
        {
            _context = context;
        }

        // GET: api/EtatInvitations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EtatInvitation>>> GetEtatInvitations()
        {
          if (_context.EtatInvitations == null)
          {
              return NotFound();
          }
            return await _context.EtatInvitations.ToListAsync();
        }

        // GET: api/EtatInvitations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EtatInvitation>> GetEtatInvitation(int id)
        {
          if (_context.EtatInvitations == null)
          {
              return NotFound();
          }
            var etatInvitation = await _context.EtatInvitations.FindAsync(id);

            if (etatInvitation == null)
            {
                return NotFound();
            }

            return etatInvitation;
        }

        // PUT: api/EtatInvitations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtatInvitation(int id, EtatInvitation etatInvitation)
        {
            if (id != etatInvitation.IdEtatInvitation)
            {
                return BadRequest();
            }

            _context.Entry(etatInvitation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtatInvitationExists(id))
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

        // POST: api/EtatInvitations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EtatInvitation>> PostEtatInvitation(EtatInvitation etatInvitation)
        {
          if (_context.EtatInvitations == null)
          {
              return Problem("Entity set 'BdreservationSalleContext.EtatInvitations'  is null.");
          }
            _context.EtatInvitations.Add(etatInvitation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEtatInvitation", new { id = etatInvitation.IdEtatInvitation }, etatInvitation);
        }

        // DELETE: api/EtatInvitations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtatInvitation(int id)
        {
            if (_context.EtatInvitations == null)
            {
                return NotFound();
            }
            var etatInvitation = await _context.EtatInvitations.FindAsync(id);
            if (etatInvitation == null)
            {
                return NotFound();
            }

            _context.EtatInvitations.Remove(etatInvitation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtatInvitationExists(int id)
        {
            return (_context.EtatInvitations?.Any(e => e.IdEtatInvitation == id)).GetValueOrDefault();
        }
    }
}
