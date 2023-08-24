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
    private readonly IConfiguration _configuration;

    public WatcherManager(Project2Context dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    public async Task<WatchResponse> WatchTicket(HttpContext httpContext, WatchRequest request)
    {
        var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings:SecretKey").Value);

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
                        return new WatchResponse { Message = "Watching ticket" };
                    }
                    else
                    {
                        return new WatchResponse { Message = "Already watching ticket" };
                    }
                }
            }
            catch (SecurityTokenValidationException ex)
            {
               return new WatchResponse { Message = ex.Message };
            }
        }
         return new WatchResponse { Message = "Need to log in" };
    }
}
