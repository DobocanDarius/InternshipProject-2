namespace RequestResponseModels.Watcher.Request;

public class WatchRequest
{
    public WatchRequest(int? userId, int ticketId)
    {
        UserId = userId;
        TicketId = ticketId;
    }
    public int? UserId { get; set; }

    public int TicketId { get; set; }

    public bool isWatching { get; set; }
}
