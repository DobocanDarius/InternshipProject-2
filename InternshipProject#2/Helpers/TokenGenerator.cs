<<<<<<<< HEAD:InternshipProject#2/Helpers/GenerateToken.cs
using InternshipProject_2.Models;
========
﻿using InternshipProject_2.Models;
>>>>>>>> master:InternshipProject#2/Helpers/TokenGenerator.cs
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InternshipProject_2.Helpers;

<<<<<<<< HEAD:InternshipProject#2/Helpers/GenerateToken.cs
public class GenerateToken
{
    private readonly IConfiguration _config;

    public GenerateToken(IConfiguration config)
========
public class TokenGenerator
{
    private readonly IConfiguration _config;

    public TokenGenerator(IConfiguration config)
>>>>>>>> master:InternshipProject#2/Helpers/TokenGenerator.cs
    {
        _config = config;
    }
    public string Generate(User user)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:SecretKey").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
<<<<<<<< HEAD:InternshipProject#2/Helpers/GenerateToken.cs

}

========
}
>>>>>>>> master:InternshipProject#2/Helpers/TokenGenerator.cs
