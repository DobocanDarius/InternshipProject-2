﻿namespace RequestResponseModels.Ticket.Response;
public class Comment
{
    public string Body 
    { get; set; }

    public string Username 
    { get; set; }

    public DateTime CreatedAt 
    { get; set; }

    public Comment()
    {
        Body = string.Empty;
        Username = string.Empty;
    }
}
