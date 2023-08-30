using Microsoft.AspNetCore.Mvc.Filters;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace InternshipProject_2.Manager
{
    public interface IWatcherManager
    {
        Task<WatchResponse> WatchTicket(WatchRequest request, int? userId);
    }
}