using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalleLaboratoireTypeActivitesController : ControllerBase
    {

        private readonly ISalleLaboratoireTypeActiviteRepository _SalleLaboratoireTypeActiviteRepository;
        private readonly IMapper _mapper;

        public SalleLaboratoireTypeActivitesController(ISalleLaboratoireTypeActiviteRepository salleLaboratoireTypeActiviteRepository, IMapper mapper)
        {
            _SalleLaboratoireTypeActiviteRepository = salleLaboratoireTypeActiviteRepository;
            _mapper = mapper;
        }

        // GET: api/SalleLaboratoireTypeActivites
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SalleLaboratoireTypeActivite>))]

        public IActionResult GetSalleLaboratoireTypeActivites()
        {
            var salleLaboratoireTypeActivites = _mapper.Map<List<SalleLaboratoireTypeActiviteDto>>(_SalleLaboratoireTypeActiviteRepository.GetSalleLaboratoireTypeActivites());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoireTypeActivites);
        }
        // GET: api/SalleLaboratoireTypeActivites/55
        [HttpGet("{noSalle}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SalleLaboratoireTypeActivite>))]
        [ProducesResponseType(400)]
        public IActionResult GetSalleLaboratoireTypeActivites(int noSalle)
        {
            if (!_SalleLaboratoireTypeActiviteRepository.SalleLaboratoireExist(noSalle))
                return NotFound();

            var salleLaboratoireTypeActivites = _mapper.Map<List<SalleLaboratoireTypeActiviteDto>>(_SalleLaboratoireTypeActiviteRepository.GetSalleLaboratoireTypeActivites(noSalle));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoireTypeActivites);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSalleLaboratoireTypeActivite([FromBody] SalleLaboratoireTypeActiviteDto salleLaboratoireTypeActiviteCreate)
        {
            if (salleLaboratoireTypeActiviteCreate == null)
                return BadRequest(ModelState);

            var salleLaboratoireTypeActivite = _SalleLaboratoireTypeActiviteRepository.GetSalleLaboratoireTypeActivites()
                .Where(s => s.NoSalle == salleLaboratoireTypeActiviteCreate.NoSalle &&
                       s.NomActivite.Trim().ToUpper() == salleLaboratoireTypeActiviteCreate.NomActivite.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (salleLaboratoireTypeActivite != null)
            {
                ModelState.AddModelError("", "Le type d'activité est déjà associé à la salle!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var salleLaboratoireTypeActiviteMap = _mapper.Map<SalleLaboratoireTypeActivite>(salleLaboratoireTypeActiviteCreate);
            if (!_SalleLaboratoireTypeActiviteRepository.CreateSalleLaboratoireTypeActivite(salleLaboratoireTypeActiviteMap))
            {
                ModelState.AddModelError("", "Échec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return Ok("Succès de la création!");
        }
        [HttpDelete("{noSalle}/{nomActivite}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSalleLaboratoireTypeActivite(int noSalle, string nomActivite)
        {
            if (!_SalleLaboratoireTypeActiviteRepository.SalleLaboratoireTypeActiviteExist(noSalle,nomActivite))
            {
                return NotFound();
            }
            var salleLaboratoireTypeActiviteToDelete = _SalleLaboratoireTypeActiviteRepository.GetSalleLaboratoireTypeActivite(noSalle, nomActivite);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_SalleLaboratoireTypeActiviteRepository.DeleteSalleLaboratoireTypeActivite(salleLaboratoireTypeActiviteToDelete))
            {
                ModelState.AddModelError("", "Echec de la suppression!");
            }
            return NoContent();
        }
    }
}
