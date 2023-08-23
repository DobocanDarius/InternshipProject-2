namespace RequestResponseModels.Watcher.Response;

public class WatchResponse
{
    public WatchResponse(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}
