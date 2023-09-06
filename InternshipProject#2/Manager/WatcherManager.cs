using Microsoft.EntityFrameworkCore;

using AutoMapper;

using InternshipProject_2.Models;

using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace InternshipProject_2.Manager;

public class WatcherManager : IWatcherManager
{
    Project2Context _DbContext;
    public HttpContext? httpContext;
    readonly Mapper _Map;

    public WatcherManager(Project2Context dbContext)
    {
        _DbContext = dbContext;
        _Map = MapperConfig.InitializeAutomapper();
    }
    public async Task<WatchResponse> WatchTicket(WatchRequest request, int userId)
    {
        var watcher = await _DbContext.Watchers
            .FirstOrDefaultAsync(w => w.UserId == userId && w.TicketId == request.TicketId);

        if (watcher != null)
        {
            if(request.IsWatching) 
            {
                watcher.IsDeleted = true;
                await _DbContext.SaveChangesAsync();
                return new WatchResponse 
                { 
                    Message = "Not watching anymore" 
                };
            }
            else
            {
                watcher.IsDeleted = false;
                await _DbContext.SaveChangesAsync();
                return new WatchResponse 
                { 
                    Message = "Watching ticket again" 
                };
            }
        }
        else
        {
            request.UserId = userId;

            var mappedWatcher = _Map.Map<Watcher>(request);
            _DbContext.Watchers.Add(mappedWatcher);
            await _DbContext.SaveChangesAsync();

            return new WatchResponse 
            { 
                Message = "Watching ticket" 
            };
        }
    }

}

