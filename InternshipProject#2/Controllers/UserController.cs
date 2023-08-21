﻿using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.User.Request;

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
        [HttpPost]
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
    }
}
