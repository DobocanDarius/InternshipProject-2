using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace InternshipProject_2.Manager;

public class UserManager : IUserManager
{
    private readonly Project2Context _dbContext;
    private readonly PasswordHasher _passwordHasher;
    private readonly TokenGenerator _tokenGenerator;
    public UserManager(Project2Context dbContext, PasswordHasher passwordHasher, TokenGenerator tokenGenerator)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
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
