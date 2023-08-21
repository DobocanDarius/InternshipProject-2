using InternshipProject_2.Models;

namespace InternshipProject_2.Manager
{
    public interface ICommentManager
    {
        public Task<IEnumerable<Comment>> GetComments(int TicketId);
    }
}
