using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using System.Net;

namespace InternshipProject_2.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicketController : ControllerBase
    {
        private readonly TicketManager _ticket;

        public TicketController(TicketManager ticket)
        {
            _ticket = ticket;
        }

        [HttpPost("new")]
        public Task NewTicket([FromBody] TicketRequest ticket)
        {
            var claim = HttpContext.User.Claims;
            var userClaim = claim.FirstOrDefault(x => x.Type.Equals("userId"));
            int reporterId = int.Parse(userClaim.Value);
            ticket.ReporterId = reporterId;

            return _ticket.CreateTicket(ticket);
        }

        [HttpPut("id")]
        public Task EditTicket([FromBody] TicketEditRequest ticket)
        {
            var claim = HttpContext.User.Claims;
            var userClaim = claim.FirstOrDefault(y => y.Type.Equals("userId"));
            int reporterId = int.Parse(userClaim.Value);

            if (ticket.ReporterId == reporterId) 
            {
                return _ticket.EditTicket(ticket);
            }
            else
            {
                return Task.CompletedTask;
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
