﻿using Microsoft.AspNetCore.Mvc.Filters;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace InternshipProject_2.Manager
{
    public interface IWatcherManager
    {
        Task WatchTicket(HttpContext httpContext, WatchRequest request);
        Task<WatchResponse> WatchTicket(HttpContext httpContext, WatchRequest request);
    }
}