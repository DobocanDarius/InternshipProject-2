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
    private readonly TokenHelper _tokenHelper;
    private readonly Mapper map;
    public UserManager(Project2Context dbContext, PasswordHasher passwordHasher, TokenHelper tokenHelper)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _tokenHelper = tokenHelper;
        map = MapperConfig.InitializeAutomapper();
    }

    public UserManager(Project2Context dbContext, PasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Login(LoginRequest user)
    {
        string hashedPsw = _passwordHasher.HashPassword(user.Password);
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == hashedPsw);

        if (foundUser != null)
        {

            LoginResponse loginResponse = new LoginResponse { Token = _tokenHelper.Generate(foundUser) };

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

        newUser.Password = _passwordHasher.HashPassword(newUser.Password);

        var user = map.Map<Models.User>(newUser);

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        var response = new CreateUserResponse { Message = "Registration successful" };

        return response;
    }

    public async Task<LogoutResponse> Logout(LogoutRequest request)
    {
        var token = map.Map<Models.InactiveToken>(request);

        _dbContext.Add(token);

        await _dbContext.SaveChangesAsync();

        var response = new LogoutResponse { Message = "Logout successful" };

        return response;
    }
}
