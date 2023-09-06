using Microsoft.EntityFrameworkCore;

using InternshipProject_2.Helpers;
using InternshipProject_2.Models;

namespace InternshipProject_2.Middleware;

public class TokenValidationMiddleware
{
    readonly RequestDelegate _Next;
    readonly Project2Context _DbContext; 
    public TokenValidationMiddleware(RequestDelegate next)
    {
        _Next = next;
        _DbContext = new Project2Context();
        
    }

    public async Task Invoke(HttpContext context, TokenHelper _tokenHelper)
    {
        var token = _tokenHelper.GetToken(context);

        if (!string.IsNullOrEmpty(token))
        {
            var tokenExists = await _DbContext.InactiveTokens.AnyAsync(t => t.Token == token);

            if (tokenExists)
            {
                context.Response.StatusCode = 401; 
                return;
            }
        }

        await _Next(context);
    }

}

public static class TokenValidationMiddlewareExtension
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenValidationMiddleware>();
    }
}
