using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;

namespace InternshipProject_2.Manager
{
    public interface ITicketManager
    {
        Task<TicketCreateResponse> CreateTicketAsync(TicketCreateRequest newTicket, int reporterId);
        Task<TicketEditResponse> DeleteTicketAsync(int id, int reporterId);
        Task<TicketEditResponse> EditTicketAsync(TicketEditRequest editTicket, int id, int reporterId);
        Task<IEnumerable<TicketGetResponse>> GetTicketsAsync();
        Task<TicketStatusResponse> ChangeTicketsStatus(TicketStatusRequest ticketStatus, int reporterId, int id);
    }
}
