using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.Assignee.Request;
using System.Runtime.CompilerServices;

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
                return BadRequest($"An error occured: {ex.Message}");
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
                return BadRequest($"An error occured: {ex.Message}");
            }
        }
    }
}
