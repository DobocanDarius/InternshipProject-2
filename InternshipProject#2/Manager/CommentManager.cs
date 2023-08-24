using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.User.Request;
using System.ComponentModel.Design;

namespace InternshipProject_2.Manager
{
    public class CommentManager : ICommentManager
    {
        private readonly Project2Context _context;
        public CommentManager(Project2Context context)
        {
            _context = context;
        }

        public async Task CreateComment(CommentRequest newComment)
        {
            var map = MapperConfig.InitializeAutomapper();
            var comment = map.Map<Comment>(newComment);
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}