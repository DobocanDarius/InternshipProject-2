namespace RequestResponseModels.Ticket.Response;

public class TicketCreateResponse
{
    public string Message
    { get; set; }

    public TicketCreateResponse()
    {
        Message = string.Empty;
    }
}

