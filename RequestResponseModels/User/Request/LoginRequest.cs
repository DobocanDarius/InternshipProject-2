using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.User.Request;

public class LoginRequest
{
    public LoginRequest(string username, string password, string email)
    {
        Username = username;
     Password = password;
        Email = email;
    }
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
}
