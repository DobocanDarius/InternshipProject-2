namespace RequestResponseModels.Watcher.Request;

public class WatchRequest
{
    public WatchRequest(int userId, int ticketId, bool isWatching)
    {
        UserId = userId;
        TicketId = ticketId;
        this.isWatching = isWatching;
    }
    public int UserId { get; set; }

    public int TicketId { get; set; }

    public bool isWatching { get; set; }
}
