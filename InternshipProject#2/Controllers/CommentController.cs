using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipProject_2.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly  ILogger<CommentController>  logger;
        private static  List<Comment> comments = new List<Comment>();
        private readonly IMapper mapper;
        public CommentController(ILogger<CommentController> logger)
        {
            this.logger = logger;
        }
    }
}
