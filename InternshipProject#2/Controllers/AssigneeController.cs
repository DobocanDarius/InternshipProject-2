using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using InternshipProject_2.Manager;

using RequestResponseModels.Assignee.Request;

namespace InternshipProject_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssigneeController : ControllerBase
{
    readonly IAssigneeManager _Manager;

    public AssigneeController(IAssigneeManager manager)
    {
        _Manager = manager;
    }

    [Authorize(Roles = "manager")]
    [HttpPost]
    [Route("assignUser")]
    public async Task<IActionResult> AssigneeUserToTicket([FromBody] AssignUserRequest request)
    {
        try
        {
            RequestResponseModels.Assignee.Response.AssignUserResponse response = await _Manager.AssignUserToTicket(request);
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
            RequestResponseModels.Assignee.Response.GetAssignedUserResponse response = await _Manager.GetAssignedUser(request);
            return Ok(response);
        }
        catch 
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
            RequestResponseModels.Assignee.Response.RemoveAssignedUserResponse response = await _Manager.RemoveAssignedUser(request);
            return Ok(response);
        }
        catch
        {
            return NotFound();
        }
    }

}
