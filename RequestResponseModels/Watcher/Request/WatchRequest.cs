namespace RequestResponseModels.Watcher.Request;
public class WatchRequest
{
    public int UserId
    { get; set; }

    public int TicketId
    { get; set; }

    public bool IsWatching
    { get; set; }

    public WatchRequest(int userId, int ticketId, bool isWatching)
    {
        UserId = userId;
        TicketId = ticketId;
        IsWatching = isWatching;
    }
    
}
