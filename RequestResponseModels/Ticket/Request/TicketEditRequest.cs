namespace RequestResponseModels.Ticket.Request;

public class TicketEditRequest
{
    public string Title 
    { get; set; }

    public string Body
    { get; set; }

    public string Priority 
    { get; set; }

    public TicketEditRequest()
    {
        Title = string.Empty;
        Body = string.Empty;
        Priority = string.Empty;
    }
}