using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace InternshipProject_2.Manager;

public class WatcherManager : IWatcherManager
{
    private Project2Context _dbContext;
    public HttpContext httpContext;

    public WatcherManager(Project2Context dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<WatchResponse> WatchTicket(WatchRequest request, int userId)
    {
        var existingWatchRequest = await _dbContext.Watchers
        .FirstOrDefaultAsync(w => w.UserId == userId && w.TicketId == request.TicketId);

        if (existingWatchRequest == null)
        {
            var map = MapperConfig.InitializeAutomapper();

            request.UserId = userId;

            var mappedWatcher = map.Map<Watcher>(request);
            await _dbContext.Watchers.AddAsync(mappedWatcher);
            await _dbContext.SaveChangesAsync();

            return new WatchResponse { Message = "Watching ticket" };
        }
        else
        {
            return new WatchResponse { Message = "Already watching ticket" };
        }
    }
}
