using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public PaiementController(ElearningDbContext context)
        {
            _context = context;
        }

        [HttpPost("effectuer")]
        public async Task<IActionResult> EffectuerPaiement([FromBody] Paiement paiement)
        {
            var cours = await _context.Cours.Include(c => c.Formateur).FirstOrDefaultAsync(c => c.Id == paiement.CoursId);
            if (cours == null)
                return NotFound("Cours introuvable.");

            paiement.DatePaiement = DateTime.UtcNow;
            _context.Paiements.Add(paiement);

            float montantFormateur = paiement.Montant * 0.7f;
            float montantAdmin = paiement.Montant * 0.3f;

            // Mise à jour du compte bancaire fictif
            cours.Formateur.CompteBancaire += $" +{montantFormateur}";
            var admin = _context.Admins.FirstOrDefault();
            if (admin != null)
                admin.CompteBancaire += $" +{montantAdmin}";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Paiement réussi avec répartition",
                montantTotal = paiement.Montant,
                formateur = montantFormateur,
                admin = montantAdmin
            });
        }
    }

}
