using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;

using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatcherController : ControllerBase
    {
        readonly IWatcherManager _WatcherManager;
        readonly TokenHelper _TokenHelper;

        public WatcherController(IWatcherManager manager, TokenHelper tokenHelper)
        {
            _WatcherManager = manager;
            _TokenHelper = tokenHelper;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> WatchTicket(WatchRequest request)
        {
            try
            {
                int? userId = _TokenHelper.GetClaimValue(HttpContext);
                if (userId == null)
                {
                    return BadRequest(new WatchResponse 
                    { 
                        Message = "You need to log in" 
                    });
                }
                WatchResponse result = await _WatcherManager.WatchTicket(request, userId.Value);
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        
    }
}
