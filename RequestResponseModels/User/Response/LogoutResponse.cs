namespace RequestResponseModels.User.Response;

public class LogoutResponse
{
    public string Message 
    { get; set; }

    public LogoutResponse()
    {
        Message = string.Empty;
    }
}
