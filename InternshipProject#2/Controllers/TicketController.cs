using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RequestResponseModels.Ticket.Request;
using InternshipProject_2.Helpers;

namespace InternshipProject_2.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager _ticketAcces;
        private readonly TokenHelper _tokenHelper;

        public TicketController(ITicketManager ticket, TokenHelper tokenHelper)
        {
            _ticketAcces = ticket;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("create")]
        [Authorize(Roles = "tester,manager")]
        public async Task<IActionResult> NewTicket([FromBody] TicketCreateRequest ticket)
        {
            var userId = _tokenHelper.GetClaimValue(HttpContext);
            try
            {
                if (userId != null)
                {
                    int reporterId = userId.Value;
                    await _ticketAcces.CreateTicketAsync(ticket, reporterId);
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
            var userId = _tokenHelper.GetClaimValue(HttpContext);
            try
            {
                if (userId != null)
                {
                    int reporterId = userId.Value;
                    if (reporterId != 0)
                    {
                        await _ticketAcces.EditTicketAsync(ticket, ticketId, reporterId);
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
                var response = await _ticketAcces.GetTicketsAsync();
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
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj))
                {
                    int reporterId = int.Parse(userIdObj.ToString());
                    if (reporterId != 0)
                    {
                        await _ticketAcces.DeleteTicketAsync(ticketId, reporterId);
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
                var TakeUserIdFromClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
                if (TakeUserIdFromClaim != null)
                {
                    int reporterId = int.Parse(TakeUserIdFromClaim.Value);
                    if (reporterId != 0)
                    {
                        await _ticketAcces.ChangeTicketsStatus(ticket, reporterId, ticketId);
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
}
       
    

