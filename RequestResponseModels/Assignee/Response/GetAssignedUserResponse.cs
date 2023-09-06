﻿namespace RequestResponseModels.Assignee.Response;

public class GetAssignedUserResponse
{
    public string Username 
    { get; set; }

    public string Role 
    { get; set; }

    public DateTime CreatedAt 
    { get; set; }

    public GetAssignedUserResponse()
    {
        Username = string.Empty;
        Role = string.Empty;
    }

}