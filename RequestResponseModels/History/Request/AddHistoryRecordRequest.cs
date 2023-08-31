using RequestResponseModels.History.Enum;

namespace RequestResponseModels.History.Request
{
    public class AddHistoryRecordRequest
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public HistoryEventType EventType { get; set; }
    }
}
