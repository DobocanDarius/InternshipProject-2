using Microsoft.EntityFrameworkCore;

using AutoMapper;

using InternshipProject_2.Helpers;
using InternshipProject_2.Models;

using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace InternshipProject_2.Manager;

public class UserManager : IUserManager
{
    readonly Project2Context _DbContext;
    readonly PasswordHasher _PasswordHasher;
    readonly TokenHelper _TokenHelper;
    readonly Mapper _Map;

    public UserManager(Project2Context dbContext, PasswordHasher passwordHasher, TokenHelper tokenHelper)
    {
        _DbContext = dbContext;
        _PasswordHasher = passwordHasher;
        _TokenHelper = tokenHelper;
        _Map = MapperConfig.InitializeAutomapper();
    }

    public UserManager(Project2Context dbContext, PasswordHasher passwordHasher)
    {
        _DbContext = dbContext;
        _PasswordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Login(LoginRequest user)
    {
        string hashedPsw = _PasswordHasher.HashPassword(user.Password);
        var foundUser = await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == hashedPsw);

        if (foundUser != null)
        {

            LoginResponse loginResponse = new LoginResponse { Token = _TokenHelper.GenerateToken(foundUser) };

            return loginResponse;
        }
        return new LoginResponse(); 
    }
    public async Task<CreateUserResponse> Create(CreateUserRequest newUser)
    {
        if(await _DbContext.Users.AnyAsync(u => u.Email == newUser.Email))
        {
            return new CreateUserResponse { Message = "User with this email already exists" };
        }

        newUser.Password = _PasswordHasher.HashPassword(newUser.Password);

        var user = _Map.Map<Models.User>(newUser);

        _DbContext.Users.Add(user);

        await _DbContext.SaveChangesAsync();

        var response = new CreateUserResponse { Message = "Registration successful" };

        return response;
    }
    public async Task<LogoutResponse> Logout(LogoutRequest request)
    {
        var token = _Map.Map<Models.InactiveToken>(request);

        _DbContext.Add(token);

        await _DbContext.SaveChangesAsync();

        var response = new LogoutResponse { Message = "Logout successful" };

        return response;
    }
}
