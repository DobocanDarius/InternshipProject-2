using InternshipProject_2.Helpers;
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
        private readonly GetUserIdClaimValue _claim;

        public WatcherController(IWatcherManager manager, GetUserIdClaimValue claim)
        {
            _manager = manager;
            _claim = claim;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> WatchTicket(WatchRequest request)
        {
            try
            {
                var userId = _claim.GetClaimValue(HttpContext);
                request.isWatching = false;
                if (userId != null)
                {
                    
                    var result = await _manager.WatchTicket(request, userId);
                    return Ok(result.Message);
                }

                return BadRequest(new WatchResponse { Message = "You need to log in" });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("stop")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> StopWatchingTicket(WatchRequest request)
        {
            try
            {
                var userId = _claim.GetClaimValue(HttpContext);
                request.isWatching = true;
                if (userId != null)
                {
                    
                    var result = await _manager.WatchTicket(request, userId);
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
