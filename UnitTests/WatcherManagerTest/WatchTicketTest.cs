using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace UnitTests.WatcherManagerTest;

[TestClass]
public class WatchTicketTest
{
    /*private Project2Context _project2Context;
    private WatcherManager _watcherManager;
    private IConfiguration _configuration;

    [TestInitialize]
    public void Setup()
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }

    [TestMethod]
    public async Task WatchTicket()
    {
        using (var dbContext = new Project2Context())
        {
            var request = new WatchRequest(2, 1);

            var watcherManager = new WatcherManager(dbContext, _configuration);

            // Act
            var result = await watcherManager.WatchTicket(request);

            // Assert
            Assert.AreEqual("Watching ticket", result.Message);
        }
    }*/
}
