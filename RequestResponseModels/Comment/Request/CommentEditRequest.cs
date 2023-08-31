namespace RequestResponseModels.Comment.Request
{
    public class CommentEditRequest
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}