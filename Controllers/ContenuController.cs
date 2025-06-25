using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElearningBackend.Models;

namespace ElearningBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContenuController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public ContenuController(ElearningDbContext context)
        {
            _context = context;
        }

        // GET: api/Contenu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contenu>>> GetContenus()
        {
            return await _context.Contenus.ToListAsync();
        }

        // GET: api/Contenu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contenu>> GetContenu(int id)
        {
            var contenu = await _context.Contenus.FindAsync(id);

            if (contenu == null)
                return NotFound();

            return contenu;
        }

        // GET: api/Contenu/by-cours/3
        [HttpGet("by-cours/{coursId}")]
        public async Task<ActionResult<IEnumerable<Contenu>>> GetContenusByCours(int coursId)
        {
            return await _context.Contenus
                .Where(c => c.CoursId == coursId)
                .ToListAsync();
        }

        // POST: api/Contenu
        [HttpPost]
        public async Task<ActionResult<Contenu>> CreateContenu(Contenu contenu)
        {
            _context.Contenus.Add(contenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContenu), new { id = contenu.Id }, contenu);
        }

        // PUT: api/Contenu/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContenu(int id, Contenu contenu)
        {
            if (id != contenu.Id)
                return BadRequest();

            _context.Entry(contenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contenus.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Contenu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContenu(int id)
        {
            var contenu = await _context.Contenus.FindAsync(id);
            if (contenu == null)
                return NotFound();

            _context.Contenus.Remove(contenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
