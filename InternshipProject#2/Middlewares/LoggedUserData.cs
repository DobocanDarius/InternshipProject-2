using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InternshipProject_2.Middlewares;

public class LoggedUserData
{
    private readonly RequestDelegate _next;

    public LoggedUserData(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("access_token", out var tokenFromCookie))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(tokenFromCookie);

            var userId = token.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var userRole = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Add user claims to HttpContext for downstream controllers
            context.Items["UserId"] = userId;
            context.Items["UserRole"] = userRole;
        }

        await _next(context);
    }
}
