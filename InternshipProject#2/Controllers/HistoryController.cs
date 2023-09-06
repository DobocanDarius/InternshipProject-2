using Microsoft.AspNetCore.Mvc;

using InternshipProject_2.Manager;

using RequestResponseModels.History.Request;

namespace InternshipProject_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoryController : ControllerBase
{
    private readonly IHistoryManager _HistoryManager;

    public HistoryController(IHistoryManager historyManager)
    {
        _HistoryManager = historyManager;
    }

    [HttpGet]
    [Route("getHistory")]
    public async Task<IActionResult> GetHistory([FromQuery] GetHistoryRequest request)
    {
        try
        {
            RequestResponseModels.History.Response.GetHistoryResponse response = await _HistoryManager.GetHistory(request);
            return Ok(response);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
