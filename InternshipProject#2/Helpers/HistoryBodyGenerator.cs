using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;

namespace InternshipProject_2.Helpers
{
    public class HistoryBodyGenerator
    {
        private readonly Project2Context dbContext = new Project2Context();
        public string GenerateHistoryBody(HistoryEventType eventType, int userId)
        {
            var username = findUsername(userId);
            switch (eventType)
            {
                case HistoryEventType.Create:
                    return $"{username} created a ticket";
                case HistoryEventType.Assign:
                    return $"{username} was assigned to this ticket";
                case HistoryEventType.Comment:
                    return $"{username} added a comment";
                case HistoryEventType.Edit:
                    return $"{username} edited the ticket";
                case HistoryEventType.Close:
                    return $"{username} closed the ticket";
                default:
                    return "Unknown event";
            }
        }
        
        public string findUsername(int userId)
        {
            var user = dbContext.Users.Find(userId);
            var username = user.Username;
            return username;
        }
    }
}
