using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElearningBackend.Models;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public ClientController(ElearningDbContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Utilisateurs
                .OfType<Client>()
                .Include(c => c.Paiements)
                .Include(c => c.CoursConsultes)
                .ToListAsync();
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Utilisateurs
                .OfType<Client>()
                .Include(c => c.Paiements)
                .Include(c => c.CoursConsultes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
                return NotFound();

            return client;
        }

        // POST: api/Client
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Utilisateurs.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        // PUT: api/Client/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Id)
                return BadRequest();

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Utilisateurs.OfType<Client>().Any(c => c.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Utilisateurs.OfType<Client>().FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
                return NotFound();

            _context.Utilisateurs.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Client/{clientId}/inscrire/{coursId}
        [HttpPost("{clientId}/inscrire/{coursId}")]
        public async Task<IActionResult> S_Inscrire(int clientId, int coursId)
        {
            var client = await _context.Utilisateurs
                .OfType<Client>()
                .Include(c => c.CoursConsultes)
                .FirstOrDefaultAsync(c => c.Id == clientId);

            var cours = await _context.Cours.FindAsync(coursId);

            if (client == null || cours == null)
                return NotFound("Client ou cours introuvable.");

            if (client.CoursConsultes.Any(c => c.Id == coursId))
                return BadRequest("Déjà inscrit à ce cours.");

            if (cours.EstPayant)
            {
                return BadRequest(new
                {
                    message = "Ce cours est payant. Veuillez acheter le contenu.",
                    redirect = $"/api/client/{clientId}/acheter/{coursId}"
                });
            }

            client.CoursConsultes.Add(cours);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Inscription réussie au cours gratuit.",
                cours = cours.Titre
            });
        }

        // POST: api/Client/{clientId}/acheter/{coursId}
        [HttpPost("{clientId}/acheter/{coursId}")]
        public async Task<IActionResult> AcheterContenu(int clientId, int coursId)
        {
            var client = await _context.Utilisateurs
                .OfType<Client>()
                .Include(c => c.CoursConsultes)
                .FirstOrDefaultAsync(c => c.Id == clientId);

            var cours = await _context.Cours.FindAsync(coursId);

            if (client == null || cours == null)
                return NotFound("Client ou cours introuvable.");

            if (client.CoursConsultes.Any(c => c.Id == coursId))
                return BadRequest("Ce cours est déjà acheté.");

            if (!cours.EstPayant)
                return BadRequest("Ce cours est gratuit. Utilisez /inscrire au lieu de /acheter.");

            var paiement = new Paiement
            {
                ClientId = client.Id,
                CoursId = cours.Id,
                Montant = cours.Prix,
                DatePaiement = DateTime.UtcNow
            };

            _context.Paiements.Add(paiement);
            client.CoursConsultes.Add(cours);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Paiement effectué avec succès.",
                cours = cours.Titre,
                montant = cours.Prix,
                date = paiement.DatePaiement
            });
        }
    }
}
