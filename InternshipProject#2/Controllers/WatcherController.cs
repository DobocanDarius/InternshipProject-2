using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;
using System.IdentityModel.Tokens.Jwt;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatcherController : ControllerBase
    {
        private readonly IWatcherManager _manager;

        public WatcherController(IWatcherManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> WatchTicket(WatchRequest request)
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
                {
                    var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(token);

                    if (jwtToken.Payload.TryGetValue("userId", out var userIdClaim))
                    {
                        var userId = int.Parse(userIdClaim.ToString());
                        var result = await _manager.WatchTicket(request, userId);
                        return Ok(result.Message);
                    }

                    return BadRequest("User ID not found");
                }

                return BadRequest(new WatchResponse { Message = "You need to log in" });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost ("stop")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> StopWatching(WatchRequest request)
        {
            try
            {
                var authorizationHeader = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
                if (authorizationHeader?.Value != null)
                {
                    
                        var userId = int.Parse(authorizationHeader.Value);
                        var result = await _manager.StopWatching(request, userId);
                        return Ok(result.Message);
                }
                return BadRequest(new WatchResponse { Message = "You need to log in" });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
