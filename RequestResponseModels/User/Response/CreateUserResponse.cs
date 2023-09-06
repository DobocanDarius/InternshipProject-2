namespace RequestResponseModels.User.Response;

public class CreateUserResponse
{
    public string Message 
    { get; set; }

    public CreateUserResponse()
    {
        Message = string.Empty;
    }
}
