using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using AutoMapper;
using backend.Interfaces;
using backend.Repository;
using backend.Dto;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalleLaboratoiresController : ControllerBase
    {
        private readonly ISalleLaboratoireRepository _salleLaboratoireRepository;
        private readonly IMapper _mapper;

        public SalleLaboratoiresController(ISalleLaboratoireRepository salleLaboratoireRepository, IMapper mapper)
        {
            _salleLaboratoireRepository = salleLaboratoireRepository;
            _mapper = mapper;
        }

        // GET: api/SalleLaboratoires
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SalleLaboratoire>))]

        public IActionResult GetSalleLaboratoires()
        {
            var salleLaboratoires = _mapper.Map<List<SalleLaboratoireDto>>(_salleLaboratoireRepository.GetSalleLaboratoires());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoires);
        }
        [HttpGet("pourReservation")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SalleLaboratoire>))]

        public IActionResult GetSalleLaboratoiresPourReservation()
        {
            var salleLaboratoires = _mapper.Map<List<SalleLaboratoireDto>>(_salleLaboratoireRepository.GetSalleLaboratoiresPourReservation());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoires);
        }
        [HttpGet("pourReservationDateHeure/{DateHeureDebut}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SalleLaboratoire>))]

        public IActionResult GetSalleLaboratoiresPourReservationDateHeure(DateTime DateHeureDebut)
        {
            var salleLaboratoires = _mapper.Map<List<SalleLaboratoireDto>>(_salleLaboratoireRepository.GetSalleLaboratoiresPourReservation(DateHeureDebut));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoires);
        }
        // GET: api/SalleLaboratoires/5
        [HttpGet("{noSalle}")]
        [ProducesResponseType(200, Type = typeof(SalleLaboratoire))]
        [ProducesResponseType(400)]

        public IActionResult GetSalleLaboratoire(int noSalle)
        {
            if (!_salleLaboratoireRepository.SalleLaboratoireExist(noSalle))
            {
                return NotFound();
            }
            var salleLaboratoire = _mapper.Map<SalleLaboratoireDto>(_salleLaboratoireRepository.GetSalleLaboratoire(noSalle));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoire);
        }
        // GET: api/SalleLaboratoires/5
        [HttpGet("pourReservation/{noSalle}")]
        [ProducesResponseType(200, Type = typeof(SalleLaboratoire))]
        [ProducesResponseType(400)]

        public IActionResult GetSalleLaboratoirePourReservation(int noSalle)
        {
            if (!_salleLaboratoireRepository.SalleLaboratoireExist(noSalle))
            {
                return NotFound();
            }
            var salleLaboratoire = _mapper.Map<SalleLaboratoireDto>(_salleLaboratoireRepository.GetSalleLaboratoirePourReservation(noSalle));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoire);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSalleLaboratoire([FromQuery] String[] lstTypeActivite, [FromBody] SalleLaboratoireDto salleLaboratoireCreate)
        {
            if (salleLaboratoireCreate == null)
                return BadRequest(ModelState);

            var salleLaboratoire = _salleLaboratoireRepository.GetSalleLaboratoires()
                .Where(t => t.NoSalle == salleLaboratoireCreate.NoSalle)
                .FirstOrDefault();

            if (salleLaboratoire != null)
            {
                ModelState.AddModelError("", "La salle existe déjà!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var salleLaboratoireMap = _mapper.Map<SalleLaboratoire>(salleLaboratoireCreate);
            if (!_salleLaboratoireRepository.CreateSalleLaboratoire(lstTypeActivite, salleLaboratoireMap))
            {
                ModelState.AddModelError("", "Échec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetSalleLaboratoire", new { noSalle = salleLaboratoireMap.NoSalle }, salleLaboratoireMap);
        }
        [HttpPut("{noSalle}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSalleLaboratoire(int noSalle, [FromQuery] String[] lstTypeActivite, [FromBody] SalleLaboratoireDto updatedSalleLaboratoire)
        {
            if (updatedSalleLaboratoire == null)
                return BadRequest(ModelState);
            if (noSalle != updatedSalleLaboratoire.NoSalle)
                return BadRequest(ModelState);
            if (!_salleLaboratoireRepository.SalleLaboratoireExist(noSalle))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var salleLaboratoireMap = _mapper.Map<SalleLaboratoire>(updatedSalleLaboratoire);
            if (!_salleLaboratoireRepository.UpdateSalleLaboratoire(lstTypeActivite,salleLaboratoireMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetSalleLaboratoire", new { noSalle = salleLaboratoireMap.NoSalle }, salleLaboratoireMap);
        }

        [HttpPut("modifierEtat/{noSalle}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSalleLaboratoireEtat(int noSalle)
        {
            if (!_salleLaboratoireRepository.SalleLaboratoireExist(noSalle))
                return NotFound();
            var updatedSalleLaboratoire = _salleLaboratoireRepository.GetSalleLaboratoire(noSalle);
            if (updatedSalleLaboratoire == null)
                return BadRequest(ModelState);
            if (noSalle != updatedSalleLaboratoire.NoSalle)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
            var salleLaboratoireMap = _mapper.Map<SalleLaboratoire>(updatedSalleLaboratoire);
            salleLaboratoireMap.EstActif = !(salleLaboratoireMap.EstActif);
            if (!_salleLaboratoireRepository.UpdateSalleLaboratoireEtat(salleLaboratoireMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetSalleLaboratoire", new { noSalle = salleLaboratoireMap.NoSalle }, salleLaboratoireMap);
        }
        [HttpDelete("{noSalle}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSalleLaboratoire(int noSalle)
        {
            if (!_salleLaboratoireRepository.SalleLaboratoireExist(noSalle))
            {
                return NotFound();
            }
            var salleLaboratoireToDelete = _salleLaboratoireRepository.GetSalleLaboratoire(noSalle);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_salleLaboratoireRepository.DeleteSalleLaboratoire(salleLaboratoireToDelete))
            {
                ModelState.AddModelError("", "Echec de la suppression!");
            }
            return NoContent();
        }
    }
}
