using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;

namespace InternshipProject_2.Manager
{
    public class HistoryManager : IHistoryManager
    {
        private Project2Context _dbContext;
        private readonly Mapper map;
        private readonly HistoryBodyGenerator _historyBodyGenerator;
        public HistoryManager(Project2Context dbContext, HistoryBodyGenerator historyBodyGenerator)
        {
            _dbContext = dbContext;
            map = MapperConfig.InitializeAutomapper();
            _historyBodyGenerator = historyBodyGenerator;
        }
        public async Task<AddHistoryRecordResponse> AddHistoryRecord(AddHistoryRecordRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(request.UserId);
                if (user == null)
                {
                    var response = new AddHistoryRecordResponse { Body = "User not found!" };
                    return response;
                }
                var ticket = await _dbContext.Tickets.FindAsync(request.TicketId);
                if(ticket == null)
                {
                    var response = new AddHistoryRecordResponse { Body = "Ticket not found!" };
                }

                if(request.EventType == HistoryEventType.Assign)
                {
                    var assignment = await _dbContext.Assignees.FindAsync(request.TicketId);
                    if(assignment == null)
                    {
                        var response = new AddHistoryRecordResponse { Body = "Assignment not found!" };
                    }
                }
                if(request.EventType == HistoryEventType.Comment)
                {
                    var comment = await _dbContext.Comments.FindAsync(request.TicketId,request.UserId);
                    if(comment == null)
                    {
                        var response = new AddHistoryRecordResponse { Body = "Comment not found" };
                        return response;
                    }
                }
                var historyBody = _historyBodyGenerator.GenerateHistoryBody(request.EventType, request.UserId);
                var historyRecord = map.Map<History>(request);
                historyRecord.Body = historyBody;
                historyRecord.CreatedAt = DateTime.Now;
                _dbContext.Histories.Add(historyRecord);
                await _dbContext.SaveChangesAsync();

                return new AddHistoryRecordResponse { Body = historyBody };
            }
            catch (Exception ex)
            {
                return new AddHistoryRecordResponse { Body = "Error adding history record" };
            }
        }
    }
}
