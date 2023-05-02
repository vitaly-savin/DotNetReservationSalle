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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationRepository reservationRepository,
                                      IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        // GET: api/Reservations
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]

        public IActionResult GetReservations()
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservations());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }
        // GET: api/Reservations
        [HttpGet("NonTraiter")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]

        public IActionResult GetReservationsNonTraiter()
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservationsNonTraiter());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }
        // GET: api/Reservations
        [HttpGet("membre/{membreCourriel}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]

        public IActionResult GetReservationsByMembre(string membreCourriel)
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservationsByMembre(membreCourriel));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }
        // GET: api/Reservations
        [HttpGet("membre/invite/{membreCourriel}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]

        public IActionResult GetReservationsByMembreInvite(string membreCourriel)
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservationsByMembreInvite(membreCourriel));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }
        // GET: api/Reservations
        [HttpGet("AVenirMembre/{membreCourriel}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]
        //Reservation accepté ou non-traité
        public IActionResult GetReservationsAVenirByMembre(string membreCourriel)
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservationsAVenirByMembre(membreCourriel));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }
        // GET: api/Reservations/salle/55
        [HttpGet("salle/{noSalle}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]
        [ProducesResponseType(400)]
        public IActionResult GetReservationsBySalle(int noSalle)
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservationsBySalle(noSalle));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }

        // GET: api/Reservations/5
        [HttpGet("{noReservation}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]

        public IActionResult GetReservation(int noReservation)
        {
            if (!_reservationRepository.ReservationExist(noReservation))
            {
                return NotFound();
            }
            var reservation = _mapper.Map<ReservationDto>(_reservationRepository.GetReservation(noReservation));
            if (!ModelState.IsValid)
            {
                return BadRequest(reservation);
            }
            return Ok(reservation);
        }

        [HttpPut("{noReservation}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReservation(int noReservation, [FromBody] ReservationDto updatedReservation)
        {
            if (updatedReservation == null)
                return BadRequest(ModelState);
            if (noReservation != updatedReservation.NoReservation)
                return BadRequest(ModelState);
            if (!_reservationRepository.ReservationExist(noReservation))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var reservationMap = _mapper.Map<Reservation>(updatedReservation);
            if (!_reservationRepository.UpdateReservation(reservationMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetReservation", new { noReservation = updatedReservation.NoReservation }, updatedReservation);
        }
        [HttpPut("Annuler/{noReservation}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AnnulerReservation(int noReservation)
        {
            var updatedReservation = _reservationRepository.GetReservation(noReservation);
            if (updatedReservation == null)
                return BadRequest(ModelState);
            if (noReservation != updatedReservation.NoReservation)
                return BadRequest(ModelState);
            if (!_reservationRepository.ReservationExist(noReservation))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var reservationMap = _mapper.Map<Reservation>(updatedReservation);
            if (!_reservationRepository.AnnulerReservation(reservationMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetReservation", new { noReservation = updatedReservation.NoReservation }, updatedReservation);
        }
        [HttpPut("Accepter/{noReservation}/{accepterPar}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AccepterReservation(int noReservation, string accepterPar)
        {
            var updatedReservation = _reservationRepository.GetReservation(noReservation);
            if (updatedReservation == null)
                return BadRequest(ModelState);
            if (noReservation != updatedReservation.NoReservation)
                return BadRequest(ModelState);
            if (!_reservationRepository.ReservationExist(noReservation))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var reservationMap = _mapper.Map<Reservation>(updatedReservation);
            if (!_reservationRepository.AccepterReservation(accepterPar, reservationMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetReservation", new { noReservation = updatedReservation.NoReservation }, updatedReservation);
        }
        [HttpPut("Refuser/{noReservation}/{refuserPar}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RefuserReservation(int noReservation, string refuserPar)
        {
            var updatedReservation = _reservationRepository.GetReservation(noReservation);
            if (updatedReservation == null)
                return BadRequest(ModelState);
            if (noReservation != updatedReservation.NoReservation)
                return BadRequest(ModelState);
            if (!_reservationRepository.ReservationExist(noReservation))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var reservationMap = _mapper.Map<Reservation>(updatedReservation);
            if (!_reservationRepository.RefuserReservation(refuserPar, reservationMap))
            {
                ModelState.AddModelError("", "Echec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetReservation", new { noReservation = updatedReservation.NoReservation }, updatedReservation);
        }
        // POST: api/Reservations
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReservation([FromQuery] String[] lstIntivites, [FromBody] ReservationDto reservationCreate)
        {
            if (reservationCreate == null)
                return BadRequest(ModelState);

            var reservation = _reservationRepository.GetReservations()
                .Where(r => r.NoReservation == reservationCreate.NoReservation)
                .FirstOrDefault();

            if (reservation != null)
            {
                ModelState.AddModelError("", "La reservation existe déjà!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservationMap = _mapper.Map<Reservation>(reservationCreate);
            if (!_reservationRepository.CreateReservation(lstIntivites,reservationMap))
            {
                ModelState.AddModelError("", "Échec de la sauvegarde!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction("GetReservation", new { noReservation = reservationMap.NoReservation }, reservationMap);
        }

        [HttpDelete("{noReservation}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReservation(int noReservation)
        {
            if (!_reservationRepository.ReservationExist(noReservation))
            {
                return NotFound();
            }
            var salleLaboratoireToDelete = _reservationRepository.GetReservation(noReservation);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reservationRepository.DeleteReservation(salleLaboratoireToDelete))
            {
                ModelState.AddModelError("", "Echec de la suppression!");
            }
            return NoContent();
        }
    }
}
