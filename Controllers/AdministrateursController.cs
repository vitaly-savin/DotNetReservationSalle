using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrateursController : ControllerBase
    {
        private readonly BdreservationSalleContext _context;

        public AdministrateursController(BdreservationSalleContext context)
        {
            _context = context;
        }

        // GET: api/Administrateurs
        [HttpGet]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Administrateur>>> GetAdministrateurs()
        {
            return await _context.Administrateurs.ToListAsync();
        }

        // GET: api/Administrateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrateur>> GetAdministrateur(string id)
        {
            var administrateur = await _context.Administrateurs.FindAsync(id);

            if (administrateur == null)
            {
                return NotFound();
            }

            return administrateur;
        }

        // PUT: api/Administrateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrateur(string id, Administrateur administrateur)
        {
            if (id != administrateur.Courriel)
            {
                return BadRequest();
            }

            _context.Entry(administrateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministrateurExists(id))
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

        // POST: api/Administrateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Administrateur>> PostAdministrateur(Administrateur administrateur)
        {
            _context.Administrateurs.Add(administrateur);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdministrateurExists(administrateur.Courriel))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdministrateur", new { id = administrateur.Courriel }, administrateur);
        }

        // DELETE: api/Administrateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrateur(string id)
        {
            var administrateur = await _context.Administrateurs.FindAsync(id);
            if (administrateur == null)
            {
                return NotFound();
            }

            _context.Administrateurs.Remove(administrateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool AdministrateurExists(string id)
        {
            return _context.Administrateurs.Any(e => e.Courriel == id);
        }
     
        
    }
}
