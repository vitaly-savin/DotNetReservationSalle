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
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;
        public InvitationsController(IInvitationRepository invitationRepository,
                                      IMapper mapper)
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]

        public IActionResult GetInvitations()
        {
            var invitations = _mapper.Map<List<InvitationDto>>(_invitationRepository.GetInvitations());
            if (!ModelState.IsValid)
            {
                return BadRequest(invitations);
            }
            return Ok(invitations);
        }
        [HttpGet("{noReservation}/{membreCourriel}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]

        public IActionResult GetInvitation(int noReservation , string membreCourriel)
        {
            if (!_invitationRepository.InvitationExist(noReservation, membreCourriel))
            {
                return NotFound();
            }
            var invitation = _mapper.Map<InvitationDto>(_invitationRepository.GetInvitation(noReservation, membreCourriel));
            if (!ModelState.IsValid)
            {
                return BadRequest(invitation);
            }
            return Ok(invitation);
        }
        [HttpGet("membre/{membreCourriel}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]

        public IActionResult GetInvitationByMembre(string membreCourriel)
        {
            var invitations = _mapper.Map<List<InvitationDto>>(_invitationRepository.GetInvitationsByMembre(membreCourriel));
            if (!ModelState.IsValid)
            {
                return BadRequest(invitations);
            }
            return Ok(invitations);
        }
        [HttpGet("reservation/{noReservation}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]

        public IActionResult GetInvitationByReservation(int noReservation)
        {
            var invitations = _mapper.Map<List<InvitationDto>>(_invitationRepository.GetInvitationsByReservation(noReservation));
            if (!ModelState.IsValid)
            {
                return BadRequest(invitations);
            }
            return Ok(invitations);
        }
        [HttpGet("reservation/{noReservation}/InvitationsAccepte")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]

        public IActionResult GetInvitationsAccepteByReservation(int noReservation)
        {
            var invitations = _mapper.Map<List<InvitationDto>>(_invitationRepository.GetInvitationsAccepteByReservation(noReservation));
            if (!ModelState.IsValid)
            {
                return BadRequest(invitations);
            }
            return Ok(invitations);
        }
        [HttpPut("Accepter/{noReservation}/{membreCourriel}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AccepterInvitation(int noReservation, string membreCourriel)
        {
            var updatedInvitation = _invitationRepository.GetInvitation(noReservation, membreCourriel);
            if (updatedInvitation == null)
                return BadRequest(ModelState);
            if (noReservation != updatedInvitation.NoReservation)
                return BadRequest(ModelState);
            if (!_invitationRepository.InvitationExist(noReservation, membreCourriel))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var invitationMap = _mapper.Map<Invitation>(updatedInvitation);
            if (!_invitationRepository.AccepterInvitation(invitationMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetInvitation", new { noReservation = updatedInvitation.NoReservation, membreCourriel = updatedInvitation.MembreCourriel }, updatedInvitation);
        }
        [HttpPut("Refuser/{noReservation}/{membreCourriel}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RefuserInvitation(int noReservation, string membreCourriel)
        {
            var updatedInvitation = _invitationRepository.GetInvitation(noReservation, membreCourriel);
            if (updatedInvitation == null)
                return BadRequest(ModelState);
            if (noReservation != updatedInvitation.NoReservation)
                return BadRequest(ModelState);
            if (!_invitationRepository.InvitationExist(noReservation, membreCourriel))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var invitationMap = _mapper.Map<Invitation>(updatedInvitation);
            if (!_invitationRepository.RefuserInvitation(invitationMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetInvitation", new { noReservation = updatedInvitation.NoReservation, membreCourriel = updatedInvitation.MembreCourriel }, updatedInvitation);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateInvitation([FromBody] InvitationDto invitationCreate)
        {
            if (invitationCreate == null)
                return BadRequest(ModelState);

            var invitation = _invitationRepository.GetInvitations()
                .Where(r => r.NoReservation == invitationCreate.NoReservation &&
                            r.MembreCourriel == invitationCreate.MembreCourriel)
                .FirstOrDefault();

            if (invitation != null)
            {
                ModelState.AddModelError("", "L'invitation existe déjà!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var invitationMap = _mapper.Map<Invitation>(invitationCreate);
            if (!_invitationRepository.CreateInvitation(invitationMap))
            {
                ModelState.AddModelError("", "Échec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetInvitation", new {
                noReservation = invitationMap.NoReservation,
                membreCourriel = invitationMap.MembreCourriel }, invitationMap);
        }
        [HttpDelete("{noReservation}/{membreCourriel}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteInvitation(int noReservation, string membreCourriel)
        {
            if (!_invitationRepository.InvitationExist(noReservation, membreCourriel))
            {
                return NotFound();
            }
            var invitationToDelete = _invitationRepository.GetInvitation(noReservation, membreCourriel);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_invitationRepository.DeleteInvitation(invitationToDelete))
            {
                ModelState.AddModelError("", "Echec de la suppression!");
            }
            return NoContent();
        }
    }
}

