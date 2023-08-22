using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RequestResponseModels.Ticket;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;

namespace InternshipProject_2.Manager
{
    public class TicketManager : ITicketManager
    {
        private readonly Project2Context _context;

        public TicketManager(Project2Context context)
        {
            _context = context;
        }
        public async Task CreateTicket(TicketRequest newTicket, int reporterId)
        {

            var map = MapperConfig.InitializeAutomapper();

            var ticket = map.Map<Ticket>(newTicket);
            
            ticket.ReporterId = reporterId;

            ticket.CreatedAt = DateTime.Now;

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

        }

        public async Task EditTicket(TicketEditRequest editTicket, int id, int reporterId)
        {
            var dbTicket = await _context.Tickets.FindAsync(id);

            if (dbTicket != null) 
            {
                var map = MapperConfig.InitializeAutomapper();

                var ticket = map.Map(editTicket, dbTicket);

                dbTicket.Id = id;

                dbTicket.UpdatedAt = DateTime.Now;

                if (dbTicket.ReporterId == reporterId)
                {
                    _context.Tickets.Update(ticket);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    await Task.CompletedTask;
                }
            }
            await Task.CompletedTask;
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
