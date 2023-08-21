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
        public async Task CreateTicket(TicketRequest newTicket)
        {
            var map = MapperConfig.InitializeAutomapper();

            var ticket = map.Map<Ticket>(newTicket);

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

        }

        public async Task EditTicket(TicketEditRequest editTicket)
        {
            var map = MapperConfig.InitializeAutomapper();

            var ticket = map.Map<Ticket>(editTicket);

            _context.Tickets.Update(ticket);

            await _context.SaveChangesAsync();
        }

        public async Task Update(TicketWatchRequest watchTicket)
        {

        }
    }
}
