using backend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtatReservationController : ControllerBase
    {
        private readonly BdreservationSalleContext _context;
        public EtatReservationController(BdreservationSalleContext context)
        {
            _context = context;
        }
        // GET: api/<EtatReservationController>
        [HttpGet]
        public IActionResult Get()
        {
            var listEtatReservation = _context.EtatReservations.ToList();
            if (listEtatReservation != null)
            {
                return Ok(listEtatReservation);
            }
            else
            {
                return BadRequest("Attention une erreur est survenue");
            }
        }

        // GET api/<EtatReservationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EtatReservationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EtatReservationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EtatReservationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
