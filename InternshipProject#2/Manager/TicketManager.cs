using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
namespace InternshipProject_2.Manager
{
    public class TicketManager : ITicketManager
    {
        private readonly Project2Context _context;
        private readonly HistoryBodyGenerator historyBodyGenerator;
        private HistoryWritter historyWritter;
        private readonly TicketStatusHelper _statusHandler;
        public TicketManager(Project2Context context, TicketStatusHelper statusHandler)
        {
            _context = context;
            historyBodyGenerator = new HistoryBodyGenerator();
            historyWritter = new HistoryWritter(context, historyBodyGenerator);
            _statusHandler = statusHandler;
        }
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
            var historyRequest = new AddHistoryRecordRequest { UserId = reporterId, TicketId = ticket.Id, EventType = HistoryEventType.Create };
            await historyWritter.AddHistoryRecord(historyRequest);
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
                    var historyRequest = new AddHistoryRecordRequest { UserId = reporterId, TicketId = dbTicket.Id, EventType = HistoryEventType.Edit };
                    await historyWritter.AddHistoryRecord(historyRequest);
                    var succesResponse = new TicketEditResponse { Message = "You succesfully edited this ticket!" };
                    return succesResponse;
                }
                var failResponse = new TicketEditResponse { Message = "You did not edit this ticket! You are not the owner of this ticket!" };
                return failResponse;
            }
            var response = new TicketEditResponse { Message = "You did not edit this ticket! This ticket doesnt exist!" };
            return response;
        }

        public async Task<TicketEditResponse> DeleteTicketAsync(int id, int reporterId)
        {
            var dbTicket = await _context.Tickets.FindAsync(id);
            if (dbTicket != null)
            {
                dbTicket.Id = id;
                if(dbTicket.ReporterId == reporterId)
                {
                    _context.Tickets.Remove(dbTicket);
                    await _context.SaveChangesAsync();
                    var succesResponse = new TicketEditResponse { Message = "You succesfully deleted this ticket!" };
                    return succesResponse;
                }
                var failResponse = new TicketEditResponse { Message = "You did not delete this ticket! You are not the owner of this ticket!" };
                return failResponse;
            }
            var response = new TicketEditResponse { Message = "You did not delete this ticket! This ticket doesnt exist!" };
            return response;
        }
        public async Task<TicketStatusResponse> ChangeTicketsStatus(TicketStatusRequest ticketStatus, int reporterId, int ticketId)
        {
            var dbUser = await _context.Users.FindAsync(reporterId);
            var dbTicket = await _context.Tickets.FindAsync(ticketId);
            if (dbTicket != null && dbUser != null && dbTicket.ReporterId == dbUser.Id)
            {
                return await _statusHandler.HandleStatusChange(dbTicket, dbUser, ticketStatus);
            }
            return new TicketStatusResponse { Message = "FAIL" };
        }
    }
}