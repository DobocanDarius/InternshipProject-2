﻿namespace RequestResponseModels.User.Request;

public class LoginRequest
{
    public LoginRequest()
    {
    }

    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}