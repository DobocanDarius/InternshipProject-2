using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
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
    }
}
