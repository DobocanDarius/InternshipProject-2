using InternshipProject_2.Models;
using RequestResponseModels.Comment.Request;

namespace InternshipProject_2.Manager
{
    public interface ICommentManager
    {   
        public Task<IEnumerable<Comment>> GetComments(int TicketId);
        public Task CreateComment(CommentRequest newComment);
        public Task EditComment(CommentEditRequest editComment);
    }
}
