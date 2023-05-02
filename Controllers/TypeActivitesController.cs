using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Interfaces;
using AutoMapper;
using backend.Dto;
using Microsoft.CodeAnalysis.Diagnostics;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeActivitesController : ControllerBase
    {
        private readonly ITypeActiviteRepository _typeActiviteRepository;
        private readonly IMapper _mapper;

        public TypeActivitesController(ITypeActiviteRepository typeActiviteRepository, IMapper mapper)
        {
            _typeActiviteRepository = typeActiviteRepository;
            _mapper = mapper;
        }

        // GET: api/TypeActivites
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TypeActivite>))]

        public IActionResult GetTypeActivites()
        {
            var typeActivites = _mapper.Map<List<TypeActiviteDto>>(_typeActiviteRepository.GetTypeActivites());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(typeActivites);
        }
        // GET: api/TypeActivites/Actifs
        [HttpGet("Actifs")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TypeActivite>))]
        
        public IActionResult GetTypeActivitesActifs()
        {
            var typeActivites = _mapper.Map<List<TypeActiviteDto>>(_typeActiviteRepository.GetTypeActivitesActifs());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(typeActivites);
        }
        // GET: api/TypeActivites/A1
        [HttpGet("{nomActivite}")]
        [ProducesResponseType(200, Type = typeof(TypeActivite))]
        [ProducesResponseType(400)]

        public IActionResult GetTypeActivite(string nomActivite)
        {
           if (!_typeActiviteRepository.TypeActiviteExist(nomActivite))
            {
                return NotFound();
            }
            var typeActivite = _mapper.Map<TypeActiviteDto>(_typeActiviteRepository.GetTypeActivite(nomActivite));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(typeActivite);
        }
        // GET: api/TypeActivites/salleLaboratoire/A1
        [HttpGet("salleLaboratoire/{nomActivite}")]
        [ProducesResponseType(200, Type = typeof(SalleLaboratoire))]
        [ProducesResponseType(400)]
        public IActionResult GetSalleLaboratoiresByTypeActivite(string nomActivite)
        {
            var salleLaboratoires = _mapper.Map<List<SalleLaboratoireDto>>(
                _typeActiviteRepository.GetSalleLaboratoiresByTypeActivite(nomActivite));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(salleLaboratoires);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTypeActvite([FromBody]TypeActiviteDto typeActiviteCreate)
        {
            if (typeActiviteCreate == null)
                    return BadRequest(ModelState);

            var typeActivite = _typeActiviteRepository.GetTypeActivites()
                .Where(t => t.NomActivite.Trim().ToUpper() == typeActiviteCreate.NomActivite.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(typeActivite != null)
            {
                ModelState.AddModelError("", "Le type d'activité existe déjà!");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var typeActiviteMap = _mapper.Map<TypeActivite>(typeActiviteCreate);
            if (!_typeActiviteRepository.CreateTypeActivite(typeActiviteMap)) 
            {
                ModelState.AddModelError("", "Échec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetTypeActivite", new { nomActivite = typeActiviteCreate.NomActivite }, typeActiviteCreate);
        }
        [HttpPut("{nomActivite}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTypeActivite(string nomActivite, [FromBody] TypeActiviteDto updatedTypeActivite)
        {
            if(updatedTypeActivite == null) 
                    return BadRequest(ModelState);  
            if(nomActivite != updatedTypeActivite.NomActivite)
                return BadRequest(ModelState);
            if (!_typeActiviteRepository.TypeActiviteExist(nomActivite))
                return NotFound();
            if (!ModelState.IsValid) 
                return BadRequest();
            var typeActiviteMap = _mapper.Map<TypeActivite>(updatedTypeActivite);
            if(!_typeActiviteRepository.UpdateTypeActivite(typeActiviteMap))
            {
                ModelState.AddModelError("", "Echec de la saubegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetTypeActivite", new { nomActivite = updatedTypeActivite.NomActivite }, updatedTypeActivite);
        }
        [HttpPut("modifierEtat/{nomActivite}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTypeActiviteEtat(string nomActivite)
        {
            if (!_typeActiviteRepository.TypeActiviteExist(nomActivite))
                return NotFound();
            var updatedTypeActivite = _typeActiviteRepository.GetTypeActivite(nomActivite);
            if (updatedTypeActivite == null)
                return BadRequest(ModelState);
            if (nomActivite != updatedTypeActivite.NomActivite)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
            var typeActiviteMap = _mapper.Map<TypeActivite>(updatedTypeActivite);
            typeActiviteMap.EstActif = !(typeActiviteMap.EstActif);
            if (!_typeActiviteRepository.UpdateTypeActivite(typeActiviteMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetTypeActivite", new { nomActivite = updatedTypeActivite.NomActivite }, updatedTypeActivite);
        }

        [HttpDelete("{nomActivite}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTypeActivite(string nomActivite)
        {
            if (!_typeActiviteRepository.TypeActiviteExist(nomActivite))
            {
                return NotFound();
            }
            var typeActiviteToDelete = _typeActiviteRepository.GetTypeActivite(nomActivite);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_typeActiviteRepository.DeleteTypeActivite(typeActiviteToDelete))
            {
                ModelState.AddModelError("", "Echec de la suppression!");
            }
            return NoContent();
        }
    }

}
