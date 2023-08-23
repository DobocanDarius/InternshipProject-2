using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Http;
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
            try
            {
                await commentManager.CreateComment(newComment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }

}
