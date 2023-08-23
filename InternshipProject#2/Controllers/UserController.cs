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

        [HttpPost("register")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> CreateUser(CreateUserRequest newUser)
        {
            try
            {
                await _userManager.Create(newUser);
                var response = new CreateUserResponse { Message = "Registration successful" };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
