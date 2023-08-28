using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
namespace InternshipProject_2.Manager
{
    public class TicketManager : ITicketManager
    {
        private readonly Project2Context _context;

        public TicketManager(Project2Context context)
        {
            _context = context;
        }
        public async Task<TicketCreateResponse> CreateTicketAsync(TicketCreateRequest newTicket, int reporterId)
        {
            var map = MapperConfig.InitializeAutomapper();
            var ticket = map.Map<Ticket>(newTicket);
            ticket.ReporterId = reporterId;
            ticket.CreatedAt = DateTime.Now;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            var response = new TicketCreateResponse { Message = "You succsessfully posted a new ticket!" };
            return response;
        }

        public async Task<TicketEditResponse> EditTicketAsync(TicketEditRequest editTicket, int id, int reporterId)
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
                    var succesResponse = new TicketEditResponse { Message = "You succesfully edited this ticket!" };
                    return succesResponse;
                }
                var failResponse = new TicketEditResponse { Message = "You did not edit this ticket! You are not the owner of this ticket!" };
                return failResponse;
            }
            var response = new TicketEditResponse { Message = "You did not edit this ticket! This ticket doesnt exist!" };
            return response;
        }
    }
}