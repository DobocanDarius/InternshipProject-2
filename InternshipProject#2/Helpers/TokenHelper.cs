using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using InternshipProject_2.Models;

namespace InternshipProject_2.Helpers;

public class TokenHelper
{
    readonly IConfiguration _Config;
    const short tokenExpirationDay = 1;
    public TokenHelper(IConfiguration config)
    {
        _Config = config;
    }
    public string GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
           new Claim("userId", user.Id.ToString()),
           new Claim(ClaimTypes.Role, user.Role),
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_Config.GetSection("JwtSettings:SecretKey").Value));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(tokenExpirationDay),
            signingCredentials: creds
            );
        string jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public int? GetClaimValue(HttpContext httpContext)
    {
        Claim? userIdClaimString = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));

        if (userIdClaimString?.Value != null && int.TryParse(userIdClaimString.Value, out int userIdClaimInt))
        {
            return userIdClaimInt;
        }
        return null;
    }

    public string? GetToken(HttpContext httpContext)
    {
        string? authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            string userToken = authorizationHeader.Substring("Bearer ".Length).Trim();
            return userToken;
        }
        else
        {
            return null;
        }
    }
}


