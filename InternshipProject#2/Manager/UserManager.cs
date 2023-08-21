using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.User.Request;

namespace InternshipProject_2.Manager;

public class UserManager : IUserManager
{
    private readonly Project2Context _context;
    private readonly PasswordHash _hash;
    private readonly Token _token;
    public UserManager(Project2Context context, PasswordHash hash, Token token)
    {
        _context = context;
        _hash = hash;
        _token = token;

    }
    public async Task Create(CreateUserRequest newUser)
    {
        var map = MapperConfig.InitializeAutomapper();

        newUser.Password = _hash.HashPassword(newUser.Password);

        var user = map.Map<User>(newUser);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task Login(LoginRequest user)
    {
        var map = MapperConfig.InitializeAutomapper();

        var foundUser = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == _hash.HashPassword(user.Password));

        string token = _token.Generate(foundUser);
    }
}
