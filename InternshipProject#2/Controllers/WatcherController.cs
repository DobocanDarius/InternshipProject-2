using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.Watcher.Request;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatcherController : ControllerBase
    {
       /* private readonly IWatcherManager _manager;

        public WatcherController(IWatcherManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> WatchTicket(HttpContext ,WatchRequest request)
        {
            try
            {
                var response = await _manager.WatchTicket(request);
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }*/

    }
}
