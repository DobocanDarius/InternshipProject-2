using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;
using System.IdentityModel.Tokens.Jwt;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatcherController : ControllerBase
    {
        private readonly IWatcherManager _watcherManager;
        private readonly TokenHelper _token;

        public WatcherController(IWatcherManager manager, TokenHelper token)
        {
            _watcherManager = manager;
            _token = token;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> WatchTicket(WatchRequest request)
        {
            try
            {
                var userId = (int)_token.GetClaimValue(HttpContext);
                if (userId != null)
                {  
                    var result = await _watcherManager.WatchTicket(request, userId);
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
