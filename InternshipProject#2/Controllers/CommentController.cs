using AutoMapper;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.User.Request;

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
        [HttpGet("CommentsFromTicket")]
        public async Task<ActionResult<List<Comment>>> GetCommentsForTicket([FromQuery] int ticketId)
        {
            var commentsForTicket = await commentManager.GetComments(ticketId);
            return Ok(commentsForTicket); 
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
        [HttpPut("Update")]
        public async Task<ActionResult> EditComment(CommentEditRequest editComment)
        {
            try
            {
                await commentManager.EditComment(editComment);
                return Ok(editComment);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteComment(int CommentID)
        {
            try
            {
                await commentManager.DeleteComment(CommentID);
                return Ok("The comment was deleted succesfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpDelete("DeleteAllCommentsFromATicket")]
        public async Task<ActionResult> DeleteCommentsFromTicket(int TicketId)
        {
            try
            {
                await commentManager.DeleteCommentsByTicketId(TicketId);
                return Ok("The comments are deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
