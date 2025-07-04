using ElearningBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElearningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ElearningDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ElearningDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Utilisateurs.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized("Utilisateur non trouvé.");

            // ici tu peux vérifier le mot de passe, à sécuriser avec du hash plus tard
            if (request.Password != "1234") return Unauthorized("Mot de passe incorrect.");

            var role = user switch
            {
                Admin => "Admin",
                Formateur => "Formateur",
                Client => "Client",
                _ => "Utilisateur"
            };

            var token = GenerateJwtToken(user, role);
            return Ok(new AuthResponse { Token = token, Role = role });
        }

        private string GenerateJwtToken(Utilisateur user, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
