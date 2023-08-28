using AutoMapper;
using AutoMapper.QueryableExtensions;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<GetHistoryResponse> GetHistory(int ticketId)
        {
            try
            {
                var historyRecords = await _dbContext.Histories.Where(history => history.TicketId == ticketId).ProjectTo<AddHistoryRecordResponse>(map.ConfigurationProvider).ToListAsync();
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
        

        
    }
}
