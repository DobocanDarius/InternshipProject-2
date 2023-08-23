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
    private readonly PasswordHash _hash;
    private readonly TokenGenerator _token;
    public UserManager(Project2Context context, PasswordHash hash, TokenGenerator token)
    {
        _context = context;
        _hash = hash;
        _token = token;

    }

    public async Task<LoginResponse> Login(LoginRequest user)
    {
        var foundUser = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == _hash.HashPassword(user.Password));



        if (foundUser != null)
        {
            var map = MapperConfig.InitializeAutomapper();
            var loginResponse = map.Map<LoginResponse>(foundUser);
            loginResponse.Token = _token.Generate(foundUser);

            return loginResponse;
        }

        return null;
    }
}
