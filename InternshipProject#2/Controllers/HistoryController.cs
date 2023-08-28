using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.History.Request;

namespace InternshipProject_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryManager _historyManager;

        public HistoryController(IHistoryManager historyManager)
        {
            _historyManager = historyManager;
        }

        [HttpGet]
        [Route("getHistory")]
        public async Task<IActionResult> GetHistory([FromQuery] GetHistoryRequest request)
        {
            try
            {
                var response = await _historyManager.GetHistory(request);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
