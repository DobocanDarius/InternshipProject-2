using AutoMapper;
using Azure.Core;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.User.Request;
using System.ComponentModel.Design;

namespace InternshipProject_2.Manager
{
    public class CommentManager : ICommentManager
    {
        private readonly Project2Context _context;
        private readonly IMapper _mapper;
        private readonly HistoryBodyGenerator historyBodyGenerator;
        private HistoryWritter historyWritter;
        public CommentManager(Project2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            historyBodyGenerator = new HistoryBodyGenerator();
            historyWritter = new HistoryWritter(_context, historyBodyGenerator);
        }
        public CommentManager(Project2Context context)
        {
            _context = context;
            historyBodyGenerator = new HistoryBodyGenerator();
            historyWritter = new HistoryWritter(_context, historyBodyGenerator);
        }
        public async Task CreateComment(CommentRequest newComment)
        {
            var map = MapperConfig.InitializeAutomapper();
            var comment = map.Map<Comment>(newComment);
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            var historyRequest = new AddHistoryRecordRequest { UserId = newComment.UserId, TicketId = newComment.TicketId, EventType = HistoryEventType.Comment };
            await historyWritter.AddHistoryRecord(historyRequest);
        }
        
        public async Task<IEnumerable<Comment>> GetComments(int TicketId)
        {
            var comments = await _context.Comments.Where(comment => comment.TicketId == TicketId).ToListAsync();
            return comments;
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
            catch (Exception ex)
            {
                throw new Exception("Invalid Comment");
            }
        }
    }
}