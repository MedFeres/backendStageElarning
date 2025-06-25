using Microsoft.AspNetCore.Mvc;
using ElearningBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public CoursController(ElearningDbContext context)
        {
            _context = context;
        }

        // ?? GET: Tous les cours
        [HttpGet]
        public async Task<IActionResult> GetAllCours()
        {
            var coursList = await _context.Cours
                .Include(c => c.Formateur)
                .ToListAsync();

            return Ok(coursList);
        }

        // ?? GET: Un cours par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCours(int id)
        {
            var cours = await _context.Cours
                .Include(c => c.Formateur)
                .Include(c => c.Contenus)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cours == null)
                return NotFound(new { Message = "Cours introuvable." });

            return Ok(cours);
        }

        // ?? PUT: Modifier un cours
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifierCours(int id, [FromBody] Cours updatedCours)
        {
            var cours = await _context.Cours.FindAsync(id);
            if (cours == null)
                return NotFound(new { Message = "Cours introuvable." });

            cours.Titre = updatedCours.Titre;
            cours.EstPayant = updatedCours.EstPayant;
            cours.Prix = updatedCours.Prix;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Cours mis à jour.", cours });
        }

        // ?? DELETE: Supprimer un cours
        [HttpDelete("{id}")]
        public async Task<IActionResult> SupprimerCours(int id)
        {
            var cours = await _context.Cours.FindAsync(id);
            if (cours == null)
                return NotFound();

            _context.Cours.Remove(cours);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cours supprimé avec succès." });
        }

        // ?? POST: Ajouter un contenu (vidéo, résumé, quiz)
        [HttpPost("{id}/ajouter-contenu")]
        public async Task<IActionResult> AjouterContenu(int id, [FromBody] Contenu contenu)
        {
            var cours = await _context.Cours.FindAsync(id);
            if (cours == null)
                return NotFound(new { Message = "Cours introuvable." });

            contenu.CoursId = id;
            _context.Contenus.Add(contenu);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Contenu ajouté avec succès.", contenu });
        }
    }
}
