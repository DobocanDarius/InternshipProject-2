using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
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
                // Log the exception for debugging
                return new AddHistoryRecordResponse { Body = "Error adding history record" };
            }
        }
    }
}
