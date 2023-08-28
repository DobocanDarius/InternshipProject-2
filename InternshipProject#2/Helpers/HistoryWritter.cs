using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;

namespace InternshipProject_2.Helpers
{
    public class HistoryWritter
    {
        private Project2Context _dbContext;
        private readonly Mapper map;
        private readonly HistoryBodyGenerator _historyBodyGenerator;
        public HistoryWritter(Project2Context dbcontext, HistoryBodyGenerator historyBodyGenerator)
        {
            _dbContext = dbcontext;
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
                if (ticket == null)
                {
                    var response = new AddHistoryRecordResponse { Body = "Ticket not found!" };
                    return response;
                }

                if (request.EventType == HistoryEventType.Assign)
                {
                    var assignment = await _dbContext.Assignees
                    .SingleOrDefaultAsync(a => a.TicketId == request.TicketId);
                    if (assignment == null)
                    {
                        var response = new AddHistoryRecordResponse { Body = "Assignment not found!" };
                        return response;
                    }
                }
                if (request.EventType == HistoryEventType.Comment)
                {
                    var comment = await _dbContext.Comments
                    .FirstOrDefaultAsync(c => c.TicketId == request.TicketId && c.UserId == request.UserId);
                    if (comment == null)
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

                return new AddHistoryRecordResponse { 
                    Body = historyBody,
                    CreatedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new AddHistoryRecordResponse { Body = "Error adding history record" };
            }
        }
    }
}
