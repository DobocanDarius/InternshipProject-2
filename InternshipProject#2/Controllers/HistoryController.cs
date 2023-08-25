using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.Assignee.Request;
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
        [HttpPost]
        [Route("addHistoryRecord")]
        public async Task<IActionResult> AddHistoryRecord(AddHistoryRecordRequest request)
        {
            try
            {
                var response = await _historyManager.AddHistoryRecord(request);
                return Ok(response.Body);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    
    }
}
