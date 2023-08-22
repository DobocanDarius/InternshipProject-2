using InternshipProject_2.Manager;
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
        
        [HttpPost]
        [Route("assignUser")]
        public async Task<IActionResult> AssigneeUserToTicket([FromBody] AssignUserRequest request)
        {
            try
            {
                if (Request.Cookies.TryGetValue("access_token", out var tokenFromCookie))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.ReadJwtToken(tokenFromCookie);

                    var userId = token.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                    var userRole = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    if (!string.Equals(userRole, "manager", StringComparison.OrdinalIgnoreCase))
                    {
                        return Unauthorized();
                    }
                    var response = await _manager.AssignUserToTicket(request);
                    return Ok(response.Message);
                }
                else return BadRequest("User not connected");
                
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
        [HttpDelete]
        [Route("removeAssignedUser")]

        public async Task<IActionResult> RemoveAssignedUser([FromQuery] RemoveAssignedUserRequest request)
        {
            try
            {
                var response = await _manager.RemoveAssignedUser(request);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
