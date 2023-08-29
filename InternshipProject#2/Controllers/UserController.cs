using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.User.Request;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly TokenRevocation _revoke;
        
        public UserController(IUserManager userManager, TokenRevocation revoke)
        {
            _userManager = userManager;
            _revoke = revoke;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest login)
        {
            try
            {
                var loggedIn = await _userManager.Login(login);
                if (loggedIn == null)
                {
                    return Unauthorized();
                }

                return Ok(loggedIn);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("register")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> CreateUser(CreateUserRequest newUser)
        {
            try
            {
                var response = await _userManager.Create(newUser);
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                _revoke.RevokeToken(token);
                return Ok("Logged out");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
