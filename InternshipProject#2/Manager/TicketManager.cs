using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using RequestResponseModels.Watcher.Request;

namespace InternshipProject_2.Manager
{
    public class TicketManager : ITicketManager
    {
        private readonly Project2Context _context;
        private readonly HistoryBodyGenerator historyBodyGenerator;
        private HistoryWritter historyWritter;
        private readonly TicketStatusHelper _statusHandler;
        private readonly Mapper map;
        public TicketManager(Project2Context context, TicketStatusHelper statusHandler)
        {
            _context = context;
            historyBodyGenerator = new HistoryBodyGenerator();
            historyWritter = new HistoryWritter(context, historyBodyGenerator);
            _statusHandler = statusHandler;
            map = MapperConfig.InitializeAutomapper();
        }
        public TicketManager(Project2Context context)
        {
            _context = context;
            map = MapperConfig.InitializeAutomapper();
            _statusHandler = new TicketStatusHelper();
        }
        public async Task<TicketCreateResponse> CreateTicketAsync(TicketCreateRequest newTicket, int reporterId)
        {
            var ticket = map.Map<Ticket>(newTicket);
            ticket.ReporterId = reporterId;
            ticket.CreatedAt = DateTime.Now;
            ticket.Reporter = await _context.Users.FindAsync(ticket.ReporterId);
            ticket.Status = 1;
            var watcher = new Models.Watcher { UserId = reporterId, TicketId = ticket.Id };
            ticket.Watchers.Add(watcher);
            var historyRequest = new AddHistoryRecordRequest { UserId = reporterId, TicketId = ticket.Id, EventType = HistoryEventType.Create };
            await historyWritter.AddHistoryRecord(historyRequest);
            var response = new TicketCreateResponse { Message = "You succsessfully posted a new ticket!" };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
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

        public async Task<IEnumerable<TicketGetResponse>> GetTicketsAsync()
        {
            var map = MapperConfig.InitializeAutomapper();
            var dbTicket = await _context.Tickets.Include(i => i.Reporter).Include(i => i.Comments).Include(i => i.Histories).Include(i => i.Watchers).Include(i=>i.Attachements).ToListAsync();
            var dbTicket = await _context.Tickets.Include(i => i.Reporter).Include(i => i.Comments).Include(i => i.Histories).Include(i => i.Watchers.Where(w => w.IsDeleted == false)).ToListAsync();
            List<TicketGetResponse> response = new List<TicketGetResponse>();
            dbTicket.ForEach(t => response.Add(map.Map<TicketGetResponse>(t)));

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