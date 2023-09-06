using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using InternshipProject_2.Helpers;
using InternshipProject_2.Models;

using RequestResponseModels.Comment.Request;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;

namespace InternshipProject_2.Manager
{
    public class CommentManager : ICommentManager
    {
        readonly Project2Context _DbContext;
        readonly Mapper _Map;
        readonly HistoryBodyGenerator _HistoryBodyGenerator;
        HistoryWritter _HistoryWritter;
        private IMapper mapper;

        public CommentManager(Project2Context context)
        {
            _DbContext = context;
            _Map = MapperConfig.InitializeAutomapper();
            _HistoryBodyGenerator = new HistoryBodyGenerator();
            _HistoryWritter = new HistoryWritter(_DbContext, _HistoryBodyGenerator);
        }

        public async Task CreateComment(CommentRequest newComment)
        {
            Comment comment = _Map.Map<Comment>(newComment);
            _DbContext.Comments.Add(comment);
            await _DbContext.SaveChangesAsync();
            AddHistoryRecordRequest historyRequest = new AddHistoryRecordRequest 
            { 
                UserId = newComment.UserId, 
                TicketId = newComment.TicketId, 
                EventType = HistoryEventType.Comment 
            };
            await _HistoryWritter.AddHistoryRecord(historyRequest);
        }
        
        public async Task<IEnumerable<Comment>> GetComments(int TicketId)
        {
            List<Comment> comments = await _DbContext.Comments.Where(comment => comment.TicketId == TicketId).ToListAsync();
            return comments;
        }
        
        public async Task EditComment([FromBody] CommentEditRequest editComment, int userId)
        {
            User? dbUser = await _DbContext.Users.FindAsync(userId);
            Comment? ExistingComment = await _DbContext.Comments.FindAsync(editComment.Id);
            if(ExistingComment != null)
            {
                Comment comment = _Map.Map(editComment, ExistingComment);
                ExistingComment.Body = editComment.Body;
                ExistingComment.Id  = comment.Id;
                if(dbUser != null)
                {
                    if (dbUser.Id == ExistingComment.UserId)
                    {
                        try
                        {
                            _DbContext.Comments.Update(ExistingComment);
                            await _DbContext.SaveChangesAsync();
                        }
                        catch
                        {
                            throw new Exception("Invalid Comment");
                        }
                    }
                }
                throw new Exception("Invalid User");
            }
            throw new Exception("Invalid Comment");
        }
        public async Task DeleteComment(int CommentId, int userId)
        {
            User? dbUser = await _DbContext.Users.FindAsync(userId);
            Comment? ExistingComment = await _DbContext.Comments.FindAsync(CommentId);
            if(ExistingComment!=null)
            {
                if (dbUser != null)
                {
                    if (dbUser.Id == ExistingComment.UserId)
                    {
                        try
                        {
                            _DbContext.Comments.Remove(ExistingComment);
                            await _DbContext.SaveChangesAsync();
                        }
                        catch
                        {
                            throw new Exception("Invalid Comment");
                        }
                    }
                }
                throw new Exception("Invalid User");
            }
            throw new Exception("Invalid Comment");
        }
    }
}