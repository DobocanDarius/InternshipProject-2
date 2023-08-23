using RequestResponseModels.Watcher.Request;

namespace InternshipProject_2.Manager
{
    public interface IWatcherManager
    {
        Task WatchTicket(WatchRequest request);
    }
}