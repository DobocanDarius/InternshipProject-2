namespace RequestResponseModels.Ticket.Response;

public class TicketGetResponse
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

    public int ReporterId 
    { get; set; }

    public DateTime CreatedAt 
    { get; set; }

    public DateTime? UpdatedAt 
    { get; set; }

    public User Reporter 
    { get; set; } = null!;

    public ICollection<Comment>? Comments 
    { get; set; }

    public ICollection<History>? Histories 
    { get; set; }

    public ICollection<Watcher>? Watchers 
    { get; set; }
    public ICollection<Attachement>? Attachements 
    { get; set; }

    public TicketGetResponse()
    {
        Title = string.Empty;
        Body = string.Empty;
        Type = string.Empty;
        Priority = string.Empty;
        Component = string.Empty;
    }
}
