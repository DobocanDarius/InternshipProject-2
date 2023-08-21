﻿using AutoMapper;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Comment.Request;

namespace InternshipProject_2.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly  ILogger<CommentController>  Logger;
        private static  List<Comment> Comments = new List<Comment>();
        private readonly IMapper Mapper;
        private readonly ICommentManager commentManager;
        public CommentController(ILogger<CommentController> Logger, IMapper Mapper, ICommentManager commentManager)
        {
            this.Logger = Logger;
            this.Mapper = Mapper;
            this.commentManager = commentManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetCommentsForTicket([FromQuery] int ticketId)
        {
            var commentsForTicket = await commentManager.GetComments(ticketId);
            return Ok(commentsForTicket); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequest comment)
        {
            try
            {
                int createdCommentId = await commentManager.CreateComment(comment);
                return CreatedAtAction("GetComment", new { id = createdCommentId }, comment); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the comment.");
            }
        }
    }
}
