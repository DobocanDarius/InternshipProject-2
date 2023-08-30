using AutoMapper;
using AutoMapper.QueryableExtensions;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;

namespace InternshipProject_2.Manager
{
    public class HistoryManager : IHistoryManager
    {
        private Project2Context _dbContext;
        private readonly Mapper map;
        public HistoryManager(Project2Context dbContext)
        {
            _dbContext = dbContext;
            map = MapperConfig.InitializeAutomapper();
        }

        public async Task<GetHistoryResponse> GetHistory(GetHistoryRequest request)
        {
            try
            {
                var historyRecords = await _dbContext.Histories.Where(history => history.TicketId == request.TicketId).ProjectTo<AddHistoryRecordResponse>(map.ConfigurationProvider).ToListAsync();
                if(!historyRecords.Any()) {
                    var errorResponse = new GetHistoryResponse
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
                var response = new GetHistoryResponse
                {
                    HistoryRecords = historyRecords
                };
                return response;
            }
            catch (Exception ex)
            {
                var error = new GetHistoryResponse
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
                var historyRecords = await _dbContext.Histories
                    .Where(history => history.CreatedAt >= startTime && history.CreatedAt <= endTime)
                    .ProjectTo<AddHistoryRecordResponse>(map.ConfigurationProvider)
                    .ToListAsync();

                return historyRecords;
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return new List<AddHistoryRecordResponse>();
            }
        }



    }
}
