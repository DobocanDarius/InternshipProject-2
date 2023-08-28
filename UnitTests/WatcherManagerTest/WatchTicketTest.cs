using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;
using System.Net.Http;

namespace UnitTests.WatcherManagerTest;

[TestClass]
public class WatchTicketTest
{
    private Project2Context _project2Context;
    private WatcherManager _watcherManager;
    private IConfiguration _configuration;
    public HttpContext _httpContext;
    private TokenGenerator _tokenGenerator;

    [TestInitialize]
    public void Setup()
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _tokenGenerator = new TokenGenerator(_configuration);
    }

    [TestMethod]
    public async Task WatchTicket()
    {
        using (var dbContext = new Project2Context())
        {
            var user = new User
            {
                Id = 123,
                Username = "Test",
                Password = "password",
                Email = "email",
                Role = "developer",
                CreatedAt = DateTime.Now

            };
            var request = new WatchRequest(user.Id, 2);

            var watcherManager = new WatcherManager(dbContext, _configuration);

            // Act
            var result = await watcherManager.WatchTicket(request);

            // Assert
            Assert.AreEqual("Watching ticket", result.Message);
        }
    }
}
