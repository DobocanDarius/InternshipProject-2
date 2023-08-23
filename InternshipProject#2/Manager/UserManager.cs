using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using RequestResponseModels.User.Request;

namespace InternshipProject_2.Manager;

public class UserManager : IUserManager
{
    private readonly Project2Context _context;
    private readonly PasswordHash _hash;
    public UserManager(Project2Context context, PasswordHash hash)
    {
        _context = context;
        _hash = hash;
    }

    public async Task Create(CreateUserRequest newUser)
    {
        var map = MapperConfig.InitializeAutomapper();

        newUser.Password = _hash.HashPassword(newUser.Password);

        var user = map.Map<User>(newUser);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }
}
