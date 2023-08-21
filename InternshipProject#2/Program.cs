using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InternshipProject_2.Models;
using Microsoft.Extensions.Configuration;

using AutoMapper;

using Microsoft.AspNetCore.Hosting;
using System.Text;

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
builder.Services.AddScoped<AssigneeManager>();
builder.Services.AddScoped<ITicketManager, TicketManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
