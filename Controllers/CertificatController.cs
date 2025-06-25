using ElearningBackend.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/certificats")]
public class CertificatController : ControllerBase
{
    [HttpPost("creer")]
    public ActionResult<Certificat> CreerCertificat([FromBody] string nomClient)
    {
        var certificat = new Certificat
        {
            NomClient = nomClient,
            DateObtention = DateTime.Now
        };
        return Ok(certificat);
    }

    [HttpGet("{nomClient}")]
    public ActionResult<Certificat> ObtenirCertificat(string nomClient)
    {
        var certificat = new Certificat
        {
            NomClient = nomClient,
            DateObtention = DateTime.Now
        };
        return Ok(certificat);
    }
}
