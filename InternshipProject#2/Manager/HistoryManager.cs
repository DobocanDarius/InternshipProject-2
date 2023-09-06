using Microsoft.EntityFrameworkCore;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using InternshipProject_2.Models;

using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;

namespace InternshipProject_2.Manager
{
    public class HistoryManager : IHistoryManager
    {
        Project2Context _DbContext;
        readonly Mapper _Map;
        public HistoryManager(Project2Context dbContext)
        {
            _DbContext = dbContext;
            _Map = MapperConfig.InitializeAutomapper();
        }

        public async Task<GetHistoryResponse> GetHistory(GetHistoryRequest request)
        {
            try
            {
                List<AddHistoryRecordResponse> historyRecords = await _DbContext.Histories.Where(history => history.TicketId == request.TicketId).ProjectTo<AddHistoryRecordResponse>(_Map.ConfigurationProvider).ToListAsync();
                if(!historyRecords.Any()) 
                {
                    GetHistoryResponse errorResponse = new GetHistoryResponse
                    {
                        HistoryRecords = new List<AddHistoryRecordResponse>
                        {
                            new AddHistoryRecordResponse
                            {
                                Body = "No history records found for this ticket."
                            }
                        }
                    };
                    return errorResponse;
                }
                GetHistoryResponse response = new GetHistoryResponse
                {
                    HistoryRecords = historyRecords
                };
                return response;
            }
            catch
            {
                GetHistoryResponse error = new GetHistoryResponse
                {
                    HistoryRecords = new List<AddHistoryRecordResponse>
                   {
                       new AddHistoryRecordResponse
                       {
                           Body = "An error occurred while retrieving history records"
                       }
                   }
                };
                return error;
            }
        }
        public async Task<List<AddHistoryRecordResponse>> GetHistoryInTimeRange(DateTime startTime, DateTime endTime)
        {
            try
            {
                List<AddHistoryRecordResponse> historyRecords = await _DbContext.Histories
                    .Where(history => history.CreatedAt >= startTime && history.CreatedAt <= endTime)
                    .ProjectTo<AddHistoryRecordResponse>(_Map.ConfigurationProvider)
                    .ToListAsync();

                return historyRecords;
            }
            catch
            {
                return new List<AddHistoryRecordResponse>();
            }
        }



    }
}
