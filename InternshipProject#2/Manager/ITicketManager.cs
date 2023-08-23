using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;

namespace InternshipProject_2.Manager
{
    public interface ITicketManager
    {
        Task CreateTicket(TicketCreateRequest newTicket, int reporterId);
    }
}
