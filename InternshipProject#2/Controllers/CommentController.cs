using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModels.Comment.Request;

namespace InternshipProject_2.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentManager commentManager;
        public CommentController(ICommentManager commentManager)
        {
            this.commentManager = commentManager;
        }
        [HttpPost("Create")]
        public async Task<ActionResult> CreateComment(CommentRequest newComment)
        {
            await commentManager.CreateComment(newComment);
            return Ok();
        }

        [HttpGet("CommentsFromTicket")]
        public async Task<ActionResult<List<Comment>>> GetCommentsForTicket([FromQuery] int ticketId)
        {
            var commentsForTicket = await commentManager.GetComments(ticketId);
            return Ok(commentsForTicket);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> EditComment(CommentEditRequest editComment)
        {
            await commentManager.EditComment(editComment);
            return Ok(editComment);
        }
    }
}
