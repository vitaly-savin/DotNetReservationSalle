using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Routing;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresController : ControllerBase
    {
        private readonly BdreservationSalleContext _context;

        public MembresController(BdreservationSalleContext context)
        {
            _context = context;
        }

        // GET: api/Membres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membre>>> GetMembres()
        {
            return await _context.Membres
                .Include(m => m.CourrielNavigation)
                .ToListAsync();
        }

        // GET: api/Membres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Membre>> GetMembre(string id)
        {
            var membre = await _context.Membres.Include(m => m.CourrielNavigation).FirstAsync(m => m.Courriel == id);

            if (membre == null)
            {
                return NotFound();
            }

            return membre;
        }

        // PUT: api/Membres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembre(string id, Membre membre)
        {
            if (id != membre.Courriel)
            {
                return BadRequest();
            }

            _context.Entry(membre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembreExists(id))
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
        // PUT: api/Membres/a@a.com/ModifierEtat
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/ModifierEtat")]
        public IActionResult ModifierEtatMembre(string id)
        {
            var membre = _context.Membres.Find(id) as Membre;
            if (membre == null)
            {
                return BadRequest("Attention! Le membre n'existe pas");
            }
            else
            {
                membre.EstActif = !(membre.EstActif);
                _context.Membres.Update(membre);
                _context.SaveChanges();
                return Ok(membre);

            }
        }

        // POST: api/Membres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Membre>> PostMembre(Membre membre)
        {
            _context.Membres.Add(membre);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MembreExists(membre.Courriel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMembre", new { id = membre.Courriel }, membre);
        }

        // DELETE: api/Membres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembre(string id)
        {
            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
            {
                return NotFound();
            }

            _context.Membres.Remove(membre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MembreExists(string id)
        {
            return _context.Membres.Any(e => e.Courriel == id);
        }
    }
}
