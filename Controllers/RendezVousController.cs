using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationRendezVous.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReservationRendezVous.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RendezVousController : ControllerBase
    {
        // Liste en mémoire pour stocker les rendez-vous
        private static List<RendezVous> rendezVousList = new List<RendezVous>
        {
            new RendezVous { Id = 1, NomPatient = "Jean Dupont", DateHeure = DateTime.Now.AddDays(1), Motif = "Consultation générale" },
            new RendezVous { Id = 2, NomPatient = "Marie Curie", DateHeure = DateTime.Now.AddDays(2), Motif = "Examen de laboratoire" }
        };

        // GET: api/RendezVous
        [HttpGet]
        public ActionResult<IEnumerable<RendezVous>> Get()
        {
            return Ok(rendezVousList);
        }

        // GET: api/RendezVous/5
        [HttpGet("{id}")]
        public ActionResult<RendezVous> Get(int id)
        {
            var rdv = rendezVousList.FirstOrDefault(r => r.Id == id);
            if (rdv == null)
                return NotFound();
            return Ok(rdv);
        }

        // POST: api/RendezVous
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<RendezVous> Post([FromBody] RendezVous rdv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            rdv.Id = rendezVousList.Max(r => r.Id) + 1;
            rendezVousList.Add(rdv);
            return CreatedAtAction(nameof(Get), new { id = rdv.Id }, rdv);
        }

        // PUT: api/RendezVous/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Put(int id, [FromBody] RendezVous rdv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRdv = rendezVousList.FirstOrDefault(r => r.Id == id);
            if (existingRdv == null)
                return NotFound();

            existingRdv.NomPatient = rdv.NomPatient;
            existingRdv.DateHeure = rdv.DateHeure;
            existingRdv.Motif = rdv.Motif;

            return NoContent();
        }

        // DELETE: api/RendezVous/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var rdv = rendezVousList.FirstOrDefault(r => r.Id == id);
            if (rdv == null)
                return NotFound();

            rendezVousList.Remove(rdv);
            return NoContent();
        }
    }
}
