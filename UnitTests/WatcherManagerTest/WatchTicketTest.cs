using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Watcher.Request;
using RequestResponseModels.Watcher.Response;

namespace UnitTests
{
    [TestClass]
    public class WatcherManagerTests
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
        public async Task WatchTicket_Successful()
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
                Status = 1,
                CreatedAt = DateTime.Now
            };
            _dbContext.Users.Add(user);
            _dbContext.Tickets.Add(ticket);
            _dbContext.SaveChanges();
            var request = new WatchRequest(user.Id, ticket.Id);
            request.isWatching = false;
            WatchResponse result = await _watchManager.WatchTicket(request, user.Id);

            _dbContext.Users.Remove(user);
            _dbContext.Tickets.Remove(ticket);
            //Assert
            Assert.AreEqual("Watching ticket", result.Message);
        }

        [TestMethod]
        public async Task WatchTicket_AlreadyWatching()
        {
            //Arrange

            var request = new WatchRequest(2002, 1);

            WatchResponse result = await _watchManager.WatchTicket(request, 1);

            //Assert
            Assert.AreEqual("Already watching ticket", result.Message);
        }
    }
}
