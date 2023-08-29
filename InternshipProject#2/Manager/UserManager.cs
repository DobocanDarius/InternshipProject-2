using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Ticket.Response;
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

    public UserManager(Project2Context dbContext, PasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Login(LoginRequest user)
    {
        string hashedPsw = _passwordHasher.HashPassword(user.Password);
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

        if (foundUser != null)
        {

            LoginResponse loginResponse = new LoginResponse { Token = _tokenGenerator.Generate(foundUser) };

            return loginResponse;
        }
        return null;
    }


    public async Task<CreateUserResponse> Create(CreateUserRequest newUser)
    {
        if(await _dbContext.Users.AnyAsync(u => u.Email == newUser.Email))
        {
            return new CreateUserResponse { Message = "User with this email already exists" };
        }

        var map = MapperConfig.InitializeAutomapper();

        newUser.Password = _passwordHasher.HashPassword(newUser.Password);

        var user = map.Map<Models.User>(newUser);

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        var response = new CreateUserResponse { Message = "Registration successful" };

        return response;
    }
}
