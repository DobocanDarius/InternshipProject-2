using AutoMapper;

using Microsoft.EntityFrameworkCore;

using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;

namespace InternshipProject_2.Helpers;

public class HistoryWritter
{
    Project2Context _DbContext;
    readonly Mapper _Map;
    readonly HistoryBodyGenerator _HistoryBodyGenerator;
    public HistoryWritter(Project2Context dbcontext, HistoryBodyGenerator historyBodyGenerator)
    {
        _DbContext = dbcontext;
        _Map = MapperConfig.InitializeAutomapper();
        _HistoryBodyGenerator = historyBodyGenerator;
    }

    public async Task<AddHistoryRecordResponse> AddHistoryRecord(AddHistoryRecordRequest request)
    {
        try
        {
            User? user = await _DbContext.Users.FindAsync(request.UserId);
            if (user == null)
            {
                AddHistoryRecordResponse response = new AddHistoryRecordResponse { Body = "User not found!" };
                return response;
            }
            Ticket? ticket = await _DbContext.Tickets.FindAsync(request.TicketId);
            if (ticket == null)
            {
                AddHistoryRecordResponse response = new AddHistoryRecordResponse { Body = "Ticket not found!" };
                return response;
            }

            if (request.EventType == HistoryEventType.Assign)
            {
                Assignee? assignment = await _DbContext.Assignees
                .SingleOrDefaultAsync(a => a.TicketId == request.TicketId);
                if (assignment == null)
                {
                    AddHistoryRecordResponse response = new AddHistoryRecordResponse { Body = "Assignment not found!" };
                    return response;
                }
            }
            if (request.EventType == HistoryEventType.Comment)
            {
                Comment? comment = await _DbContext.Comments
                .FirstOrDefaultAsync(c => c.TicketId == request.TicketId && c.UserId == request.UserId);
                if (comment == null)
                {
                    var response = new AddHistoryRecordResponse { Body = "Comment not found" };
                    return response;
                }
            }
            string historyBody = _HistoryBodyGenerator.GenerateHistoryBody(request.EventType, request.UserId);
            History historyRecord = _Map.Map<History>(request);
            historyRecord.Body = historyBody;
            historyRecord.CreatedAt = DateTime.Now;
            _DbContext.Histories.Add(historyRecord);
            await _DbContext.SaveChangesAsync();

            return new AddHistoryRecordResponse { 
                TicketId = request.TicketId,
                Body = historyBody,
                CreatedAt = DateTime.Now
            };
        }
        catch
        {
            return new AddHistoryRecordResponse { Body = "Error adding history record" };
        }
    }
}
