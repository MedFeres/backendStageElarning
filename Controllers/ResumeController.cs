using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElearningBackend.Models;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public ResumeController(ElearningDbContext context)
        {
            _context = context;
        }

        // GET: api/Resume
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resume>>> GetResumes()
        {
            return await _context.Resumes.ToListAsync();
        }

        // GET: api/Resume/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resume>> GetResume(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);
            if (resume == null)
                return NotFound();

            return resume;
        }

        // POST: api/Resume
        [HttpPost]
        public async Task<ActionResult<Resume>> PostResume(Resume resume)
        {
            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResume), new { id = resume.Id }, resume);
        }

        // PUT: api/Resume/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResume(int id, Resume resume)
        {
            if (id != resume.Id)
                return BadRequest();

            _context.Entry(resume).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Resumes.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Resume/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResume(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);
            if (resume == null)
                return NotFound();

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
