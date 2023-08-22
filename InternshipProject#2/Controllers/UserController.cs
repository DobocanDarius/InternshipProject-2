using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.User.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly Token _token;
        private readonly PasswordHash _hash;
        private readonly Project2Context _context;
        public UserController(IUserManager userManager, Token token, Project2Context context, PasswordHash hash)
        {
            _userManager = userManager;
            _token = token;
            _context = context;
            _hash = hash;
        }
        [HttpPost("register")]
        public async Task<ActionResult> CreateUser(CreateUserRequest newUser)
        {
            try
            {
                await _userManager.Create(newUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest login)
        {
            try
            {
                var foundUser = await _context.Users
               .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == _hash.HashPassword(login.Password));

                var token = _token.Generate(foundUser);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    HttpOnly = true,
                };

                Response.Cookies.Append("access_token", token, cookieOptions);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {
            if (Request.Cookies.TryGetValue("access_token", out var tokenFromCookie))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(tokenFromCookie);

                // Extract claims from the token
                var userId = token.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                var userRole = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                return Ok(new { UserId = userId, UserRole = userRole });
            }
            return BadRequest();
           
        }
    }
}
