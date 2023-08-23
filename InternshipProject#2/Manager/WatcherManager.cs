using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RequestResponseModels.Watcher.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace InternshipProject_2.Manager;

public class WatcherManager : IWatcherManager
{
    private Project2Context _dbContext;

    public WatcherManager(Project2Context dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task WatchTicket(HttpContext httpContext, WatchRequest request)
    {
        var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var key = Encoding.ASCII.GetBytes("aB5G7HjL3kR8xY0qP9eF2wZI6mN1cV4XoE5bD9A");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var userIdClaim = claimsPrincipal.FindFirst("userId");

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {

                    var existingWatchRequest = await _dbContext.Watchers.FirstOrDefaultAsync(w => w.UserId == userId && w.TicketId == request.TicketId);

                    if (existingWatchRequest == null)
                    {
                        var map = MapperConfig.InitializeAutomapper();

                        request.UserId = userId;

                        var mappedWatcher = map.Map<Watcher>(request);
                        await _dbContext.Watchers.AddAsync(mappedWatcher);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (SecurityTokenValidationException)
            {
               
            }
        }
        else
        {
            
        }
    }
}
