using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.IdentityModel.Tokens;
using RequestResponseModels.Ticket;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
namespace InternshipProject_2.Manager
{
    public class TicketManager
    {
        private readonly Project2Context _context;

        public TicketManager(Project2Context context)
        {
            _context = context;
        }
        public Task CreateTicket(TicketRequest newTicket)
        {
            var map = MapperConfig.InitializeAutomapper();

            var ticket = map.Map<Ticket>(newTicket);

            _context.Tickets.Add(ticket);

            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
