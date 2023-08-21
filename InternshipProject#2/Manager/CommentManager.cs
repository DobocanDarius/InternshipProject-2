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

        public async Task DeleteComment(int CommentId)
        {
            var ExistingComment = await _context.Comments.FindAsync(CommentId);
            try
            {
                _context.Comments.Remove(ExistingComment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Comment");
            }
        }

        public async Task EditComment(CommentEditRequest editComment)
        {
            var ExistingComment = await _context.Comments.FindAsync(editComment.Id);
            ExistingComment.Body = editComment.Body;
            try 
            {
                _context.Comments.Update(ExistingComment);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid Comment");
            }
            
        }
        public async Task<IEnumerable<Comment>> GetComments(int TicketId)
        {
            var comments = await _context.Comments.Where(comment => comment.TicketId == TicketId).ToListAsync();
            return comments;
        }
    }
}
