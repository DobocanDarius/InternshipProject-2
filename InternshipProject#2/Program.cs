using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InternshipProject_2.Models;
using System.Text;
using InternshipProject_2;
using Microsoft.Extensions.Options;
using InternshipProject_2.BackgroundServices;
using InternshipProject_2.Middleware;
using FileSystem.Registration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Project2Context>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<TokenHelper>();
builder.Services.AddScoped<TicketStatusHelper>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddFileSystemServices();
builder.Services.AddSession(options =>
{
    // Configure session options here
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;
        var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddScoped<ICommentManager, CommentManager>();
builder.Services.AddScoped<ITicketManager, TicketManager>();
builder.Services.AddHostedService<TicketChangeNotifier>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAssigneeManager, AssigneeManager>();
builder.Services.AddScoped<IHistoryManager, HistoryManager>();
builder.Services.AddScoped<IAttachementManager, AttachementManager>();
builder.Services.AddScoped<HistoryBodyGenerator>();
builder.Services.AddScoped<HistoryWritter>();
builder.Services.AddScoped<TokenValidationParameters>();
builder.Services.AddScoped<IWatcherManager, WatcherManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMyMiddleware();

app.MapControllers();


app.Run();