namespace RequestResponseModels.Comment.Request
{
    public class CommentRequest
    {
        public string Body { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}