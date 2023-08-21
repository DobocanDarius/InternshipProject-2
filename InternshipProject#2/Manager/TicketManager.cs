using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RequestResponseModels.Ticket;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace InternshipProject_2.Manager
{
    public class TicketManager : ITicketManager
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken("aB5G7HjL3kR8xY0qP9eF2wZI6mN1cV4XoE5bD9A");

            var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "userId");
            int reporterId = int.Parse(userIdClaim.Value);
            ticket.ReporterId = reporterId;

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

        public async Task DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            else
            {
                id = 999999999;
            }



            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketResponse>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();

            var map = MapperConfig.InitializeAutomapper();

            var ticket = map.Map<IEnumerable<TicketResponse>>(tickets);

            return ticket;
        }
    }
}
