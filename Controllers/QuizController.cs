using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElearningBackend.Models;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public QuizController(ElearningDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuiz()
        {
            return await _context.Quizs.ToListAsync();
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizs.FindAsync(id);
            if (quiz == null)
                return NotFound();

            return quiz;
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            _context.Quizs.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
        }

        // PUT: api/Quiz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
                return BadRequest();

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Quizs.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizs.FindAsync(id);
            if (quiz == null)
                return NotFound();

            _context.Quizs.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Quiz/{id}/soumettre
        [HttpPost("{id}/soumettre")]
        public async Task<IActionResult> SoumettreResultat(int id, [FromBody] ResultatQuiz input)
        {
            var quiz = await _context.Quizs.Include(q => q.Cours).FirstOrDefaultAsync(q => q.Id == id);
            var client = await _context.Clients.FindAsync(input.ClientId);

            if (quiz == null || client == null)
                return NotFound();

            input.QuizId = id;
            input.DateSoumission = DateTime.Now;

            _context.ResultatsQuiz.Add(input);

            // Générer certificat si score ≥ 80
            if (input.Score >= 80)
            {
                var admin = await _context.Admins.FirstOrDefaultAsync();
                if (admin != null)
                {
                    var certif = new Certificat
                    {
                        NomClient = $"{client.Prenom} {client.Nom}",
                        DateObtention = DateTime.Now,
                        AdminId = admin.Id
                    };
                    _context.Certificats.Add(certif);
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Résultat enregistré.");
        }
    }
}
