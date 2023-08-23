using Microsoft.AspNetCore.Mvc.Filters;
using RequestResponseModels.Watcher.Request;

namespace InternshipProject_2.Manager
{
    public interface IWatcherManager
    {
        Task WatchTicket(HttpContext httpContext, WatchRequest request);
    }
}