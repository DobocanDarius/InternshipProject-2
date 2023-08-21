using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Comment.Request;

namespace InternshipProject_2.Manager
{
    public class CommentManager : ICommentManager
    {
        private readonly Project2Context _context;
        public CommentManager(Project2Context context)
        {
            _context = context;
        }

        public async Task<int> CreateComment(CommentRequest commentRequest)
        {
            var comment = new Comment
            {
                Body = commentRequest.Body,
                UserId = commentRequest.UserId,
                TicketId = commentRequest.TicketId,
                CreatedAt = commentRequest.CreatedAt
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<IEnumerable<Comment>> GetComments(int TicketId)
        {
            var comments = await _context.Comments
                 .Where(comment => comment.TicketId == TicketId)
                 .ToListAsync();
            return comments;
        }
    }
}
