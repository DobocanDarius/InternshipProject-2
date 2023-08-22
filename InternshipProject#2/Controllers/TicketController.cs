using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using System.Net;

namespace InternshipProject_2.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager _ticket;

        public TicketController(ITicketManager ticket)
        {
            _ticket = ticket;
        }

        [HttpPost]
        public async Task<IActionResult> NewTicket([FromBody] TicketRequest ticket, int reporterId)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj))
                {
                    reporterId = int.Parse(userIdObj.ToString());
                    if (reporterId != 0)
                    {
                        await _ticket.CreateTicket(ticket,reporterId);
                        return Ok();
                    }
                    else return BadRequest("User needs to be logged in");
                }
                else return BadRequest("Manager needs to be logged in");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("edit/{ticketId}")]
        public async Task<IActionResult> EditTicket([FromBody] TicketEditRequest ticket, int ticketId,int reporterId)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("UserId", out var userIdObj))
                {
                    reporterId = int.Parse(userIdObj.ToString());
                    if (reporterId != 0)
                    {
                        await _ticket.EditTicket(ticket, ticketId, reporterId);
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

        [HttpDelete("id")]
        public Task DeleteTicket(int id) 
        {
            return _ticket.DeleteTicket(id);
        }

        [HttpGet]
        public Task<IEnumerable<TicketResponse>> GetAllTickets()
        {
            return _ticket.GetTickets();
        }

    }
}
