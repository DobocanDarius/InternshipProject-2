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

        [HttpPost("register")]
        public async Task<ActionResult> CreateUser(CreateUserRequest newUser)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserRole", out var userRoleObj))
                {
                    var userRole = userRoleObj.ToString();
                    if (userRole == "manager")
                    {
                        await _userManager.Create(newUser);
                        return Ok();
                    }
                    else return BadRequest("User needs to be a manager");
                }
                else return BadRequest("Manager needs to be logged in");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
