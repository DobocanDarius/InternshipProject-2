namespace RequestResponseModels.History.Response;

public class GetHistoryResponse
{
    public List<AddHistoryRecordResponse> HistoryRecords 
    { get; set; }

    public GetHistoryResponse()
    {
        HistoryRecords = new List<AddHistoryRecordResponse>();
    }
}
