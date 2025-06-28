using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projekt_ISS.Data;
using Projekt_ISS.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projekt_ISS.Controllers
{
    public class LoginController : Controller
    {
        private readonly TaskTrackerContext _context;
        private readonly PasswordHasher _passwordHasherService; // For password hashing
        private readonly IConfiguration _configuration; // For JWT configuration

        public LoginController(TaskTrackerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/userlogin/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Find the user by email
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }
            // Validate request
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid email or password.");
            }

            // Validate password using the PasswordHasherService
            var isPasswordValid = PasswordHasher.VerifyPassword(request.Password, user.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid password.");
            }

            var token = GenerateJwtToken(user);

            // Return token along with user information and role
            return Ok(new
            {
                UserID = user.Id,
                Token = token,
                Email = user.Email,
                Password = user.Password,
            });
        }

        // GET: api/userlogin/{email}
        [HttpGet("{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new
            {
                user.Id,
                user.Email,
            });
        }

        // GET: api/userlogin/login?email=nejc.severmihelic@scv.si&password=Evx92iDLhVSaUi6TuzS3QnqeVwPcFW6dK3/qghPlBE0=
        [HttpGet("login")]
        public IActionResult GetLogin([FromQuery] string email, [FromQuery] string password)
        {
            // Validate email and password
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email and password are required.");
            }

            // Check if user exists in the database (case-insensitive comparison)
            var user = _context.Users
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                UserID = user.Id,
            });
        }

        private string GenerateJwtToken(User user)
        {
            // Set the claims for the JWT token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
            // Secret key for signing JWT - you should store this securely, not hardcoded
            var secretKey = _configuration["Authentication:Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("JWT Secret Key is missing from configuration.");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }

    // LoginRequest class for POST login method
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

}
