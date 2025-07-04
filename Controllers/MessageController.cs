using ElearningBackend;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/messages")]
public class MessageController : ControllerBase
{
    private readonly ElearningDbContext _context;

    public MessageController(ElearningDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> EnvoyerMessage([FromBody] Message msg)
    {
        msg.DateEnvoi = DateTime.Now;
        _context.Messages.Add(msg);
        await _context.SaveChangesAsync();
        return Ok("Message envoyé.");
    }

    [HttpGet("entre/{id1}/{id2}")]
    public async Task<IActionResult> GetMessages(int id1, int id2)
    {
        var messages = await _context.Messages
            .Where(m => (m.ExpediteurId == id1 && m.DestinataireId == id2) ||
                        (m.ExpediteurId == id2 && m.DestinataireId == id1))
            .OrderBy(m => m.DateEnvoi)
            .ToListAsync();

        return Ok(messages);
    }
}
