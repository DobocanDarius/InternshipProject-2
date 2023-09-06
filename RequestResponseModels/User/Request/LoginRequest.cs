namespace RequestResponseModels.User.Request;

public class LoginRequest
{
    public string Email
    { get; set; }

    public string Password
    { get; set; }

    public LoginRequest()
    {
        Email = string.Empty;
        Password = string.Empty;
    }

    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}