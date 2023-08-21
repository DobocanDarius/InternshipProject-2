using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;

namespace InternshipProject_2.Manager
{
    public interface ITicketManager
    {
        Task CreateTicket(TicketRequest newTicket);
        Task DeleteTicket(int id);
        Task EditTicket(TicketEditRequest editTicket);
        Task<IEnumerable<TicketResponse>> GetTickets();
    }
}