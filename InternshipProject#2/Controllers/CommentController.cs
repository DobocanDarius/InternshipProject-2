using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipProject_2.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly  ILogger<CommentController>  Logger;
        private static  List<Comment> Comments = new List<Comment>();
        private readonly IMapper Mapper;
        public CommentController(ILogger<CommentController> Logger, IMapper Mapper)
        {
            this.Logger = Logger;
            this.Mapper = Mapper;
        }
        [HttpGet("{ticketId}")]
        public ActionResult<List<Comment>> GetCommentsForTicket([FromQuery] int ticketId)
        {
            var commentsForTicket = Comments.Where(comment => comment.TicketId == ticketId).ToList();
            return commentsForTicket;
        }
    }
}
