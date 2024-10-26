using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using project9_cohort4.Server.DTOs;
using project9_cohort4.Server.Models;

namespace project9_cohort4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin == true ? "Admin" : "User")
            };

            // Make sure the key is long enough for HMAC-SHA512
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            // Use HMAC-SHA256 instead of HMAC-SHA512
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = "your_issuer", // Add your issuer
                Audience = "your_audience" // Add your audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO model)
        {
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                return BadRequest("Username already exists");
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                return BadRequest("Email already exists");
            }

            // Create password hash
            string passwordHash;
            using (var hmac = new HMACSHA256())
            {
                passwordHash = Convert.ToBase64String(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password))
                );
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsAdmin = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var token = GenerateJwtToken(user);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Username = user.Username,
                IsAdmin = user.IsAdmin ?? false
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            // Verify password
            string passwordHash;
            using (var hmac = new HMACSHA256())
            {
                passwordHash = Convert.ToBase64String(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password))
                );
            }

            if (user.PasswordHash != passwordHash)
            {
                return BadRequest("Invalid username or password");
            }

            var token = GenerateJwtToken(user);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Username = user.Username,
                IsAdmin = user.IsAdmin ?? false
            });
        }
    }
}