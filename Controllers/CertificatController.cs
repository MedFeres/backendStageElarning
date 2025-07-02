using ElearningBackend.Models;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.Metadata;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/certificats")]
    public class CertificatController : ControllerBase
    {
        [HttpGet("telecharger")]
        public IActionResult TelechargerCertificat([FromQuery] string nomClient, [FromQuery] string prenomClient, [FromQuery] string titreCours, [FromQuery] int score)
        {
            var date = DateTime.Now;
            var pdfBytes = GenererPdf(nomClient, prenomClient, titreCours, date, score);

            return File(pdfBytes, "application/pdf", "certificat.pdf");
        }

        private byte[] GenererPdf(string nom, string prenom, string cours, DateTime date, int score)
        {
            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("CERTIFICAT DE RÉUSSITE")
                        .SemiBold().FontSize(30).AlignCenter().FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(50).Column(column =>
                    {
                        column.Spacing(15);

                        column.Item().Text($"Délivré à :").Bold();
                        column.Item().Text($"{prenom} {nom}").FontSize(26).Bold().FontColor(Colors.Green.Darken1);

                        column.Item().Text($"Pour la réussite du cours :").Bold();
                        column.Item().Text($"{cours}").Italic();

                        column.Item().Text($"Score : {score}%");

                        column.Item().Text($"Date : {date:dd/MM/yyyy}");
                    });

                    page.Footer().AlignCenter().Text("Plateforme e-learning © 2025");
                });
            }).GeneratePdf();
        }
    }
}
