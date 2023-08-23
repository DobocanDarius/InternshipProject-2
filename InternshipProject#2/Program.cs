using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Project2Context>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITicketManager, TicketManager>();
builder.Services.AddDbContext<Project2Context>();
builder.Services.AddScoped<IAssigneeManager, AssigneeManager>();

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
