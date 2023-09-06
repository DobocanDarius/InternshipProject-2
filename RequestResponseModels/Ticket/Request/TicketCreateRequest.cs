namespace RequestResponseModels.Ticket.Request;

public class TicketCreateRequest
{
    public string Title 
    { get; set; }

    public string Body 
    { get; set; }

    public string Type 
    { get; set; }

    public string Priority 
    { get; set; }

    public string Component 
    { get; set; }

    public TicketCreateRequest()
    {
        Title = string.Empty;
        Body = string.Empty;
        Type = string.Empty;
        Priority = string.Empty;
        Component = string.Empty;
    }
}
