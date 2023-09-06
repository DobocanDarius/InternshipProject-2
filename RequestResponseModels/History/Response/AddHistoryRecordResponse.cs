namespace RequestResponseModels.History.Response;

public class AddHistoryRecordResponse
{
   public int TicketId 
   { get; set; }

   public string Body 
   { get; set; }

   public DateTime CreatedAt 
   { get; set; }

    public AddHistoryRecordResponse()
    {
        Body = string.Empty;
    }
}
