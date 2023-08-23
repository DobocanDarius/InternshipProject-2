namespace RequestResponseModels.User.Request;

public class LoginRequest
{
    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;

    }


    public string Password { get; set; } 

    public string Email { get; set; } 

}

