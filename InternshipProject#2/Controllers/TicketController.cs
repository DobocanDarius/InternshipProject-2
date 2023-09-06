using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

using InternshipProject_2.Manager;
using InternshipProject_2.Helpers;

using RequestResponseModels.Ticket.Request;


namespace InternshipProject_2.Controllers;

[Route("api/ticket")]
[ApiController]
public class TicketController : ControllerBase
{
    readonly ITicketManager _TicketAcces;
    readonly TokenHelper _TokenHelper;

    public TicketController(ITicketManager ticket, TokenHelper tokenHelper)
    {
        _TicketAcces = ticket;
        _TokenHelper = tokenHelper;
    }

    [HttpPost("create")]
    [Authorize(Roles = "tester,manager")]
    public async Task<IActionResult> NewTicket([FromBody] TicketCreateRequest ticket)
    {
        int? userId = _TokenHelper.GetClaimValue(HttpContext);
        try
        {
            if (userId != null)
            {
                int reporterId = userId.Value;
                await _TicketAcces.CreateTicketAsync(ticket, reporterId);
                return Ok();
            }
            else return BadRequest("You need to be logged in");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpPut("edit/{ticketId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> EditTicket([FromBody] TicketEditRequest ticket, int ticketId)
    {
        int? userId = _TokenHelper.GetClaimValue(HttpContext);
        try
        {
            if (userId != null)
            {
                int reporterId = userId.Value;
                if (reporterId != 0)
                {
                    await _TicketAcces.EditTicketAsync(ticket, ticketId, reporterId);
                    return Ok();
                }
                else return BadRequest("You did not post this!");
            }
            else return BadRequest("You are not logged in!");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetTickets()
    {
        try
        {
            IEnumerable<RequestResponseModels.Ticket.Response.TicketGetResponse> response = await _TicketAcces.GetTicketsAsync();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("delete/{ticketId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteTicketAsync(int ticketId)
    {
        try
        {
            int? userId = _TokenHelper.GetClaimValue(HttpContext);
            if (userId != null)
            {
                int reporterId = userId.Value;
                if (reporterId != 0)
                {
                    await _TicketAcces.DeleteTicketAsync(ticketId, reporterId);
                    return Ok();
                }
                else return BadRequest("You are not logged in!");
            }
            else return BadRequest("You are not logged in!");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpPut("editTicketsStatus/{ticketId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> EditTicketsStatus([FromBody] TicketStatusRequest ticket, int ticketId)
    {
        try
        {
            System.Security.Claims.Claim? TakeUserIdFromClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
            if (TakeUserIdFromClaim != null)
            {
                int reporterId = int.Parse(TakeUserIdFromClaim.Value);
                if (reporterId != 0)
                {
                    await _TicketAcces.ChangeTicketsStatus(ticket, reporterId, ticketId);
                    return Ok();
                }
                else return BadRequest("You did not post this!");
            }
            else return BadRequest("You are not logged in!");
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}
   


