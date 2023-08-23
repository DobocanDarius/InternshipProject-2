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
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;
    public DateTime CreatedAt { get; }
}
