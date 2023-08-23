
using RequestResponseModels.User.Request;

namespace RequestResponseModels.User.Response;

public class LoginResponse
{
    public string? Token { get; set; }
    public LoginRequest User { get; set; }
    public LoginResponse()
    {
        User = new LoginRequest();
        Token = string.Empty;
    }
}

