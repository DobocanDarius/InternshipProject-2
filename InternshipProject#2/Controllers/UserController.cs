using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest login)
        {
            try
            {
                var foundUser = await _context.Users
               .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == _hash.HashPassword(login.Password));
                if (foundUser != null)
                {
                    var token = _token.Generate(foundUser);
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        HttpOnly = true,
                    };

                    Response.Cookies.Append("access_token", token, cookieOptions);

                    return Ok(new { Token = token });
                }

                else return BadRequest("Email or password is wrong");

            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
