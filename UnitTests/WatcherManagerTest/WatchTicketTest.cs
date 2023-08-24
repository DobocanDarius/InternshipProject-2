using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace UnitTests.WatcherManagerTest;

[TestClass]
public class WatchTicketTest
{
    private Project2Context _project2Context;
    private WatcherManager _watcherManager;
    private IConfiguration _configuration;

    [TestInitialize]
    public void Setup(IConfiguration configuration)
    {
        _project2Context = new Project2Context();
        _configuration = configuration;
        _watcherManager = new WatcherManager(_project2Context, _configuration);
    }

    [TestMethod]
    public async Task WatchTicket()
    {
        //Arrange
        var user = new User
        {
            Username = "Test",
            Password = "password",
            Email = "email",
            Role = "developer",
            CreatedAt = DateTime.Now

        };
        var ticket = new Ticket
        {
            Title = "Test",
            Body = "Test",
            Type = "Test",
            Priority = "Test",
            Component = "Test",
            ReporterId = 1,
            CreatedAt = DateTime.Now
        };
        _project2Context.Users.Add(user);
        _project2Context.Tickets.Add(ticket);
        _project2Context.SaveChanges();

        var request = new WatchRequest(user.Id, ticket.Id);

        //Act
        WatchResponse result = await _watcherManager.WatchTicket(request);

        //Assert
        Assert.AreEqual("Watching ticket", result.Message);
    }
}
