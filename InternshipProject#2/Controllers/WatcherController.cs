using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;
using System.Net.Http;

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
        [Authorize]
        public async Task<ActionResult> WatchTicket([FromBody] WatchRequest request)
        {
            try
            {
                await _manager.WatchTicket(HttpContext, request);
                var response = new WatchResponse { Message = "Watching ticket" };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
