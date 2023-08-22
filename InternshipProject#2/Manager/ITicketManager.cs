using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;

namespace InternshipProject_2.Manager
{
    public interface ITicketManager
    {
        Task CreateTicket(TicketRequest newTicket, int reporterId);
        Task DeleteTicket(int id);
        Task EditTicket(TicketEditRequest editTicket, int ticketId, int reporterId);
        Task<IEnumerable<TicketResponse>> GetTickets();
    }
}