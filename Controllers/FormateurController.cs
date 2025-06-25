using Microsoft.AspNetCore.Mvc;
using ElearningBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormateurController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public FormateurController(ElearningDbContext context)
        {
            _context = context;
        }

        // ?? GET: Liste des formateurs
        [HttpGet]
        public async Task<IActionResult> GetAllFormateurs()
        {
            var formateurs = await _context.Formateurs
                .Include(f => f.CoursCree)
                .ToListAsync();
            return Ok(formateurs);
        }

        // ?? GET: Un formateur par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormateur(int id)
        {
            var formateur = await _context.Formateurs
                .Include(f => f.CoursCree)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (formateur == null)
                return NotFound(new { Message = "Formateur introuvable." });

            return Ok(formateur);
        }

        // ?? POST: Déposer CV et diplôme
        [HttpPost("{id}/deposer-documents")]
        public async Task<IActionResult> DeposerDocuments(int id, [FromBody] Formateur input)
        {
            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur == null)
                return NotFound(new { Message = "Formateur introuvable." });

            formateur.Cv = input.Cv;
            formateur.Diplome = input.Diplome;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Documents déposés avec succès.", formateur });
        }

        // ?? POST: Ajouter un cours
        [HttpPost("{id}/ajouter-cours")]
        public async Task<IActionResult> AjouterCours(int id, [FromBody] Cours cours)
        {
            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur == null)
                return NotFound(new { Message = "Formateur introuvable." });

            cours.FormateurId = id;
            _context.Cours.Add(cours);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCours", "Cours", new { id = cours.Id }, cours);
        }

        // ?? DELETE: Supprimer un formateur
        [HttpDelete("{id}")]
        public async Task<IActionResult> SupprimerFormateur(int id)
        {
            var formateur = await _context.Formateurs.FindAsync(id);
            if (formateur == null)
                return NotFound();

            _context.Formateurs.Remove(formateur);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Formateur supprimé avec succès." });
        }
    }
}
