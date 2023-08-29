using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;

namespace InternshipProject_2.Manager
{
    public interface IHistoryManager
    {
        public Task<GetHistoryResponse> GetHistory(GetHistoryRequest request);
        public Task<List<AddHistoryRecordResponse>> GetHistoryInTimeRange(DateTime startTime, DateTime endTime);
    }
}
