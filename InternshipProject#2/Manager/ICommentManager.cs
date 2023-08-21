using InternshipProject_2.Models;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;

namespace InternshipProject_2.Manager
{
    public interface ICommentManager
    {
        public Task<IEnumerable<Comment>> GetComments(int TicketId);
        public Task CreateComment(CommentRequest newComment);
    }
}
