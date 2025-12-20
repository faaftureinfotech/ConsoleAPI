using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConstructionFinance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _cfg;
        public AuthController(IConfiguration cfg) { _cfg = cfg; }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            // demo: accept admin/admin
            if (dto.Username == "admin" && dto.Password == "admin")
            {
                var jwt = _cfg.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"] ?? "REPLACE"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[] { new Claim(ClaimTypes.Name, dto.Username) };
                var token = new JwtSecurityToken(jwt["Issuer"], jwt["Audience"], claims, expires: DateTime.UtcNow.AddHours(2), signingCredentials: creds);
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }
    }

    public class LoginDto { public string Username { get; set; } = null!; public string Password { get; set; } = null!; }
}
