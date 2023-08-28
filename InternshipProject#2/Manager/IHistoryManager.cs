using RequestResponseModels.History.Response;

namespace InternshipProject_2.Manager
{
    public interface IHistoryManager
    {
        public Task<GetHistoryResponse> GetHistory(int ticketId);
    }
}
