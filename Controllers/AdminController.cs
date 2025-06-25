using ElearningBackend.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    [HttpPost("generer-certificat")]
    public ActionResult<Certificat> GenererCertificat([FromBody] string nomClient)
    {
        var certificat = new Certificat
        {
            NomClient = nomClient,
            DateObtention = DateTime.Now
        };
        return Ok(certificat);
    }

    [HttpPost("valider-formateur")]
    public ActionResult ValiderFormateur([FromBody] Formateur formateur)
    {
        if (formateur != null)
        {
            formateur.EstValide = true;
            return Ok("Formateur validé avec succès.");
        }
        return BadRequest("Formateur invalide.");
    }

    [HttpGet("statistiques")]
    public ActionResult<string> VoirStatistiques()
    {
        int totalUtilisateurs = 100;
        int totalFormateurs = 20;
        int totalClients = 80;
        int totalCours = 50;

        string stats = $"Utilisateurs: {totalUtilisateurs}, Formateurs: {totalFormateurs}, Clients: {totalClients}, Cours: {totalCours}";
        return Ok(stats);
    }
}