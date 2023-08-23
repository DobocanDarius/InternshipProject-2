using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace InternshipProject_2.Manager;

public class UserManager : IUserManager
{
    private readonly Project2Context _context;
    private readonly PasswordHasher _passwordHasher;
    private readonly TokenGenerator _token;
    private readonly Mapper map;
    public UserManager(Project2Context context, PasswordHasher passwordHasher, TokenGenerator token)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _token = token;
        map = MapperConfig.InitializeAutomapper();
    }

    public async Task<LoginResponse> Login(LoginRequest user)
    {
        string hashedPsw = _passwordHasher.HashPassword(user.Password);
        var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == hashedPsw);

        if (foundUser != null)
        {
            LoginResponse loginResponse = map.Map<LoginResponse>(foundUser);
            loginResponse.Token = _token.Generate(foundUser);

            return loginResponse;
        }

        return null;
    }
}
