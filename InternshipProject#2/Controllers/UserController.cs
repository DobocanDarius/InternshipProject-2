using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;

using RequestResponseModels.User.Request;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserManager _UserManager;
        readonly TokenHelper _TokenHelper;
        
        public UserController(IUserManager userManager, TokenHelper tokenHelper)
        {
            _UserManager = userManager;
            _TokenHelper = tokenHelper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest login)
        {
            try
            {
                RequestResponseModels.User.Response.LoginResponse loggedIn = await _UserManager.Login(login);
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
                RequestResponseModels.User.Response.CreateUserResponse response = await _UserManager.Create(newUser);
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
                string? token = _TokenHelper.GetToken(HttpContext);
                if(token == null)
                {
                    return BadRequest("You are not logged in");
                }
                RequestResponseModels.User.Response.LogoutResponse response = await _UserManager.Logout(new LogoutRequest { Token = token });
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}