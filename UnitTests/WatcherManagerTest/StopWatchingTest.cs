using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace UnitTests
{
    [TestClass]
    public class StopWatchingTest
    {
        private WatcherManager _watchManager;
        private Project2Context _dbContext;

        [TestInitialize]
        public void Setup()
        {
            _dbContext = new Project2Context();
            _watchManager = new WatcherManager(_dbContext);
        }

        [TestMethod]
        public async Task StopWatching_Successful()
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
            _dbContext.Users.Add(user);
            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();
            var request = new WatchRequest(user.Id, ticket.Id);
            await _watchManager.WatchTicket(request, user.Id);
            WatchResponse result = await _watchManager.StopWatching(request, user.Id);

            //Assert
            Assert.AreEqual("Stopped watching", result.Message);
        }

        [TestMethod]
        public async Task WatchTicket_AlreadyWatching()
        {
            //Arrange

            var request = new WatchRequest(2002, 1);

            WatchResponse result = await _watchManager.StopWatching(request, 9999999);

            //Assert
            Assert.AreEqual("Not watching ticket", result.Message);
        }
    }
}
