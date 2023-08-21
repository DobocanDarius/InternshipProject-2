using InternshipProject_2.Models;
using RequestResponseModels.Comment.Request;

namespace InternshipProject_2.Manager
{
    public interface ICommentManager
    {
        public Task<IEnumerable<Comment>> GetComments(int TicketId);
        public Task<int> CreateComment(CommentRequest comment);
    }
}
