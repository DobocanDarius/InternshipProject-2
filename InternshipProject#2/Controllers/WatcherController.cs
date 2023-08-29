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
        private readonly TokenRevocation _revoke;

        public WatcherController(IWatcherManager manager, TokenRevocation revoke)
        {
            _manager = manager;
            _revoke = revoke;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> WatchTicket(WatchRequest request)
        {
            try
            {
                var token = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

                if (_revoke.IsTokenRevoked(token))
                {
                    return BadRequest(new WatchResponse { Message = "You need to log in" });
                }

                var authorizationHeader = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
                if (authorizationHeader?.Value != null)
                {
                    var userId = int.Parse(authorizationHeader.Value);
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
