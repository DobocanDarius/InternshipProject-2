namespace RequestResponseModels.Comment.Response;

public class CommentResponse
{
    public int Id 
    { get; set; }

    public string Body 
    { get; set; }

    public int UserId 
    { get; set; }

    public int TicketId 
    { get; set; }

    public DateTime CreatedAt 
    { get; set; }

    public DateTime UpdatedAt 
    {  get; set; }

    public CommentResponse(int id, string body, int userId, int tickerId, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Body = body;
        UserId = userId;
        TicketId = tickerId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    public CommentResponse()
    {
        Body = string.Empty;
    }
}