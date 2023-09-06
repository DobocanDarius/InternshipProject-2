using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;

namespace InternshipProject_2.Helpers;

public class HistoryBodyGenerator
{
    readonly Project2Context _DbContext = new Project2Context();
    public string GenerateHistoryBody(HistoryEventType eventType, int userId)
    {
        string username = FindUsername(userId);
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
    
    public string FindUsername(int userId)
    {
        User? user = _DbContext.Users.Find(userId);
        if (user != null)
        {
            string username = user.Username;
            return username;
        }

        return "User does not exist";
    }
}
