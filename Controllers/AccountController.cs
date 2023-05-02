using backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using backend.Repository;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly BdreservationSalleContext _context;
        public AccountController(IAccountRepository accountRepository,
                BdreservationSalleContext context)
        {
            _accountRepository = accountRepository;
            _context = context;

        }
        [HttpPost("enregistrement")]
        public async Task<IActionResult> Enregistrement([FromBody] EnregistrementPersonne enregistrementPersonne)
        {
            var result = await _accountRepository.SignUpAsync(enregistrementPersonne);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();

        }
        [HttpPost("EnregistrementMembre")]
        public async Task<IActionResult> EnregistrementMembre([FromBody] EnregistrementPersonne enregistrementPersonne)
        {
            var result = await _accountRepository.SignUpAsync(enregistrementPersonne);
            if (result.Succeeded)
            {
                //Ajouter Personne
                Personne personne = new Personne();
                personne.Courriel = enregistrementPersonne.Email;
                personne.Nom = enregistrementPersonne.LastName;
                personne.Prenom = enregistrementPersonne.FirstName;
                PersonnesController p = new PersonnesController(_context);
                p.PostPersonne(personne);
                //Ajouter Membre
                Membre membre = new Membre();
                membre.Courriel = enregistrementPersonne.Email;
                membre.Adresse = enregistrementPersonne.Adresse;
                membre.Province = enregistrementPersonne.Province;
                membre.CodePostal = enregistrementPersonne.CodePostal;
                membre.Telephone = enregistrementPersonne.Telephone;
                membre.EstActif = true;
                MembresController m = new MembresController(_context);
                m.PostMembre(membre);

                return Ok(result.Succeeded);
            }
            return Unauthorized();

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ConnecterPersonne connecterPersonne)
        {
            var result = await _accountRepository.LoginAsync(connecterPersonne);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);

        }

    }
}
