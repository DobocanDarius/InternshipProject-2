using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> EditComment(CommentEditRequest editComment)
        {
                await commentManager.EditComment(editComment, int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId")).Value));
                return Ok(editComment); 
        }
        [HttpDelete("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteComment(int CommentID)
        {
            await commentManager.DeleteComment(CommentID, int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId")).Value));
            return Ok("The comment was deleted succesfully!");
        }
    }
}
