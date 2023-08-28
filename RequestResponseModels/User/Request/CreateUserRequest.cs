namespace RequestResponseModels.User.Request;

public class CreateUserRequest
{
    public CreateUserRequest(string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
        CreatedAt = DateTime.Now;
    }
    public string Username { get; set; } 

    public string Password { get; set; } 

    public string Email { get; set; } 

    public string Role { get; set; } 
    public DateTime CreatedAt { get; }
}
