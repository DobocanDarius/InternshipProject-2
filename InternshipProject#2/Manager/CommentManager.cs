using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipProject_2.Manager
{
    public class CommentManager : ICommentManager
    {
        private readonly Project2Context _context;
        public CommentManager(Project2Context context)
        {
            _context = context;
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
