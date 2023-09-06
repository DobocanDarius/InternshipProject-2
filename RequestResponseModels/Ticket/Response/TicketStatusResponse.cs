
namespace RequestResponseModels.Ticket.Response;

public class TicketStatusResponse
{
    public string Message 
    { get; set; }

    public TicketStatusResponse()
    {
        Message = string.Empty;
    }
}
