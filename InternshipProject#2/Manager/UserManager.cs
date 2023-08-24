using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using RequestResponseModels.User.Request;

namespace InternshipProject_2.Manager;

public class UserManager : IUserManager
{
    private readonly Project2Context _dbContext;
    private readonly PasswordHash _passwordHasher;
    public UserManager(Project2Context dbContext, PasswordHash passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task Create(CreateUserRequest newUser)
    {
        var map = MapperConfig.InitializeAutomapper();

        newUser.Password = _passwordHasher.Hash(newUser.Password);

        var user = map.Map<User>(newUser);

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();
    }
}
