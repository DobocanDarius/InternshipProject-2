using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.Ticket.Request;

namespace InternshipProject_2.Manager
{
    public interface ICommentManager
    {
        public Task<IEnumerable<Comment>> GetComments(int TicketId);
        public Task CreateComment(CommentRequest newComment);
        public Task EditComment(CommentEditRequest editComment);
        public Task DeleteComment(int CommentId);
        public Task DeleteCommentsByTicketId(int TicketId);
    }
}
