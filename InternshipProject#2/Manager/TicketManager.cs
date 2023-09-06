using Microsoft.EntityFrameworkCore;

using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;

namespace InternshipProject_2.Manager;

public class TicketManager : ITicketManager
{
    readonly Project2Context _Context;
    readonly HistoryBodyGenerator _HistoryBodyGenerator;
    private HistoryWritter _HistoryWritter;
    private Project2Context project2Context;
    readonly TicketStatusHelper _StatusHandler;
    readonly Mapper _Map;
    public TicketManager(Project2Context context, TicketStatusHelper statusHandler)
    {
        _Context = context;
        _HistoryBodyGenerator = new HistoryBodyGenerator();
        _HistoryWritter = new HistoryWritter(context, _HistoryBodyGenerator);
        _StatusHandler = statusHandler;
        _Map = MapperConfig.InitializeAutomapper();
    }

    public TicketManager(Project2Context project2Context)
    {
        this.project2Context = project2Context;
    }

    public async Task<TicketCreateResponse> CreateTicketAsync(TicketCreateRequest newTicket, int reporterId)
    {
        Ticket ticket = _Map.Map<Ticket>(newTicket);
        ticket.ReporterId = reporterId;
        ticket.CreatedAt = DateTime.Now;
        Models.User? user = await _Context.Users.FindAsync(ticket.ReporterId);
        if(user != null)
        {
            ticket.Reporter = user;
            ticket.Status = 1;
            Models.Watcher watcher = new Models.Watcher { UserId = reporterId, TicketId = ticket.Id };
            ticket.Watchers.Add(watcher);
            AddHistoryRecordRequest historyRequest = new AddHistoryRecordRequest { UserId = reporterId, TicketId = ticket.Id, EventType = HistoryEventType.Create };
            await _HistoryWritter.AddHistoryRecord(historyRequest);
            TicketCreateResponse response = new TicketCreateResponse { Message = "You succsessfully posted a new ticket!" };
            _Context.Tickets.Add(ticket);
            await _Context.SaveChangesAsync();
            return response;
        }
        return new TicketCreateResponse { Message = "Invalid user" };
    }

    public async Task<TicketEditResponse> EditTicketAsync(TicketEditRequest editTicket, int id, int reporterId)
    {
        Ticket? dbTicket = await _Context.Tickets.FindAsync(id);
        if (dbTicket != null)
        {
            Ticket ticket = _Map.Map(editTicket, dbTicket);
            dbTicket.Id = id;
            dbTicket.UpdatedAt = DateTime.Now;
            if (dbTicket.ReporterId == reporterId)
            {
                _Context.Tickets.Update(ticket);
                await _Context.SaveChangesAsync();
                AddHistoryRecordRequest historyRequest = new AddHistoryRecordRequest { UserId = reporterId, TicketId = dbTicket.Id, EventType = HistoryEventType.Edit };
                await _HistoryWritter.AddHistoryRecord(historyRequest);
                TicketEditResponse succesResponse = new TicketEditResponse { Message = "You succesfully edited this ticket!" };
                return succesResponse;
            }
            TicketEditResponse failResponse = new TicketEditResponse { Message = "You did not edit this ticket! You are not the owner of this ticket!" };
            return failResponse;
        }
        TicketEditResponse response = new TicketEditResponse { Message = "You did not edit this ticket! This ticket doesnt exist!" };
        return response;
    }

    public async Task<TicketEditResponse> DeleteTicketAsync(int id, int reporterId)
    {
        Ticket? dbTicket = await _Context.Tickets.FindAsync(id);
        if (dbTicket != null)
        {
            dbTicket.Id = id;
            if(dbTicket.ReporterId == reporterId)
            {
                _Context.Tickets.Remove(dbTicket);
                await _Context.SaveChangesAsync();
                TicketEditResponse succesResponse = new TicketEditResponse { Message = "You succesfully deleted this ticket!" };
                return succesResponse;
            }
            TicketEditResponse failResponse = new TicketEditResponse { Message = "You did not delete this ticket! You are not the owner of this ticket!" };
            return failResponse;
        }
        TicketEditResponse response = new TicketEditResponse { Message = "You did not delete this ticket! This ticket doesnt exist!" };
        return response;
    }

    public async Task<IEnumerable<TicketGetResponse>> GetTicketsAsync()
    {

        List<Ticket> dbTicket = await _Context.Tickets.Include(i => i.Reporter).Include(i => i.Comments).Include(i => i.Histories).Include(i => i.Watchers.Where(w => w.IsDeleted == false)).ToListAsync();
        List<TicketGetResponse> response = new List<TicketGetResponse>();
        dbTicket.ForEach(t => response.Add(_Map.Map<TicketGetResponse>(t)));

        return response;
    }
    public async Task<TicketStatusResponse> ChangeTicketsStatus(TicketStatusRequest ticketStatus, int reporterId, int ticketId)
    {
        Models.User? dbUser = await _Context.Users.FindAsync(reporterId);
        Ticket? dbTicket = await _Context.Tickets.FindAsync(ticketId);
        if (dbTicket != null && dbUser != null && dbTicket.ReporterId == dbUser.Id)
        {
            return await _StatusHandler.HandleStatusChange(dbTicket, dbUser, ticketStatus);
        }
        return new TicketStatusResponse { Message = "FAIL" };
    }
}