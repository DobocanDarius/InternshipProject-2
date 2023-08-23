
using RequestResponseModels.User.Request;

namespace RequestResponseModels.User.Response;

public class LoginResponse
{
    public string? Token { get; set; }
    public IEnumerable<LoginRequest> Users { get; set; }
    public LoginResponse()
    {
        Users = new List<LoginRequest>();
        Token = string.Empty;
    }
}

