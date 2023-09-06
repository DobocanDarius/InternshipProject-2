namespace RequestResponseModels.Assignee.Response;

public class RemoveAssignedUserResponse
{
    public string Message 
    { get; set; }

    public RemoveAssignedUserResponse()
    {
        Message = string.Empty;
    }
}