﻿using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.Assignee.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneeController : ControllerBase
    {
        private readonly IAssigneeManager _manager;
        public AssigneeController(IAssigneeManager manager)
        {
            _manager = manager;
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        [Route("assignUser")]
        public async Task<IActionResult> AssigneeUserToTicket([FromBody] AssignUserRequest request)
        {
            try
            {
                var response = await _manager.AssignUserToTicket(request);
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("getAssignedUser")]
        public async Task<IActionResult> GetAssignedUser([FromQuery] GetAssignedUserRequest request)
        {
            try
            {
                var response = await _manager.GetAssignedUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Authorize(Roles ="manager")]
        [HttpDelete]
        [Route("removeAssignedUser")]

        public async Task<IActionResult> RemoveAssignedUser([FromQuery] RemoveAssignedUserRequest request)
        {
            try
            {
                if (!HttpContext.Items.TryGetValue("UserRole", out var userRole))
                {
                    return BadRequest("User not connected");
                }
                if (!string.Equals(userRole as string, "manager", StringComparison.OrdinalIgnoreCase))
                {
                    return Unauthorized();
                }
                var response = await _manager.RemoveAssignedUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
