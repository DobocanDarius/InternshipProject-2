using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace InternshipProject_2.Manager;

public class WatcherManager : IWatcherManager
{
    private Project2Context _dbContext;
    private readonly Mapper map;
    public WatcherManager(Project2Context dbContext)
    {
        _dbContext = dbContext;
        map = MapperConfig.InitializeAutomapper();
    }
    public async Task<WatchResponse> WatchTicket(WatchRequest request, int? userId)
    {
        var isDeleted = await _dbContext.Watchers
        .AnyAsync(w => w.UserId == userId && w.TicketId == request.TicketId && w.IsDeleted == true);

        var dbWatchTicket = await _dbContext.Watchers
            .FirstOrDefaultAsync(w => w.UserId == userId && w.TicketId == request.TicketId);

        if (dbWatchTicket != null)
        {
            if(request.isWatching) 
            {
                dbWatchTicket.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
                return new WatchResponse { Message = "Not watching anymore" };
            }
            else
            {
                dbWatchTicket.IsDeleted = false;
                await _dbContext.SaveChangesAsync();
                return new WatchResponse { Message = "Watching ticket again" };
            }
        }
        else
        {
            request.UserId = userId;

            var mappedWatcher = map.Map<Watcher>(request);
            _dbContext.Watchers.Add(mappedWatcher);
            await _dbContext.SaveChangesAsync();

            return new WatchResponse { Message = "Watching ticket" };
        }
    }

}

