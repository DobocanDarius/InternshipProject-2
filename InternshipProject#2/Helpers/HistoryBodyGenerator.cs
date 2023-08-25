using RequestResponseModels.History.Enum;

namespace InternshipProject_2.Helpers
{
    public class HistoryBodyGenerator
    {
        public string GenerateHistoryBody(HistoryEventType eventType, int userId)
        {
            switch (eventType)
            {
                case HistoryEventType.Create:
                    return $"User {userId} created a ticket";
                case HistoryEventType.Assign:
                    return $"User {userId} was assigned to this ticket";
                case HistoryEventType.Comment:
                    return $"User {userId} added a comment";
                case HistoryEventType.Edit:
                    return $"User {userId} edited the ticket";
                case HistoryEventType.Close:
                    return $"User {userId} closed the ticket";
                default:
                    return "Unknown event";
            }
        }
    }
}
