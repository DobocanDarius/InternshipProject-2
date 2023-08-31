using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipProject_2.Middleware;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Project2Context _dbContext; 
    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
        _dbContext = new Project2Context();
        
    }

    public async Task Invoke(HttpContext context, TokenHelper _tokenHelper)
    {
        var token = _tokenHelper.GetToken(context);

        if (!string.IsNullOrEmpty(token))
        {
            var tokenExists = await _dbContext.InactiveTokens.AnyAsync(t => t.Token == token);

            if (tokenExists)
            {
                context.Response.StatusCode = 401; 
                return;
            }
        }

        await _next(context);
    }

}

public static class TokenValidationMiddlewareExtension
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenValidationMiddleware>();
    }
}
