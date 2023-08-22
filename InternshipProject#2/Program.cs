using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InternshipProject_2.Models;
using Microsoft.Extensions.Configuration;

using AutoMapper;

using Microsoft.AspNetCore.Hosting;
using System.Text;
using InternshipProject_2.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Project2Context>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<ICommentManager, CommentManager>();
builder.Services.AddScoped<PasswordHash>();
builder.Services.AddScoped<Token>();
builder.Services.AddDbContext<Project2Context>();
builder.Services.AddScoped<IAssigneeManager,AssigneeManager>();
builder.Services.AddScoped<ITicketManager, TicketManager>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes("aB5G7HjL3kR8xY0qP9eF2wZI6mN1cV4XoE5bD9A");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<LoggedUserData>();
app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
