namespace RequestResponseModels.User.Request;

public class LogoutRequest
{
    public string Token 
    { get; set; }

    public LogoutRequest()
    {
        Token = string.Empty;
    }
}
