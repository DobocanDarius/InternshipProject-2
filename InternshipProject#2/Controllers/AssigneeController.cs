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
        private readonly AssigneeManager _manager;
        public AssigneeController(AssigneeManager manager)
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
    }
}
