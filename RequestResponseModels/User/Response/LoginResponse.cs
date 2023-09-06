namespace RequestResponseModels.User.Response;

public class LoginResponse
{
    public string Token 
    { get; set; }

    public LoginResponse()
    {
        Token = string.Empty;
    }
}