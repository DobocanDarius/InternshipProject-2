using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;

using RequestResponseModels.Comment.Request;
using RequestResponseModels.Watcher.Response;

namespace InternshipProject_2.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    readonly ICommentManager _CommentManager;
    readonly TokenHelper _TokenHelper;
    public CommentController(ICommentManager commentManager, TokenHelper tokenHelper)
    {
        _CommentManager = commentManager;
        _TokenHelper = tokenHelper;
    }
    [HttpPost("Create")]
    public async Task<ActionResult> CreateComment(CommentRequest newComment)
    {
        await _CommentManager.CreateComment(newComment);
        return Ok();
    }

    [HttpGet("CommentsFromTicket")]
    public async Task<ActionResult<List<Comment>>> GetCommentsForTicket([FromQuery] int ticketId)
    {
        IEnumerable<Comment> commentsForTicket = await _CommentManager.GetComments(ticketId);
        return Ok(commentsForTicket);
    }

    [HttpPut("Update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> EditComment(CommentEditRequest editComment)
    {
        int? userId = _TokenHelper.GetClaimValue(HttpContext);
        if (userId == null)
        {
            return BadRequest(new WatchResponse { Message = "You need to log in" });
        }
        await _CommentManager.EditComment(editComment, userId.Value);
        return Ok(editComment); 
    }

    [HttpDelete("Delete")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> DeleteComment(int CommentID)
    {
        int? userId = _TokenHelper.GetClaimValue(HttpContext);
        if (userId == null)
        {
            return BadRequest(new WatchResponse { Message = "You need to log in" });
        }
        await _CommentManager.DeleteComment(CommentID, userId.Value);
        return Ok("The comment was deleted succesfully!");
    }
}
