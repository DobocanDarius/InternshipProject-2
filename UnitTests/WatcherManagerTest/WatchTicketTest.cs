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
            var request = new WatchRequest(user.Id, ticket.Id, false);
            WatchResponse result = await _watchManager.WatchTicket(request, user.Id);

            _dbContext.Users.Remove(user);
            _dbContext.Tickets.Remove(ticket);
            _dbContext.SaveChanges();
            //Assert
            Assert.AreEqual("Watching ticket", result.Message);
        }

        [TestMethod]
        public async Task WatchTicket_AlreadyWatching()
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
            var request = new WatchRequest(user.Id, ticket.Id, false);
            await _watchManager.WatchTicket(request, user.Id);

            WatchResponse result = await _watchManager.WatchTicket(request, user.Id);

            _dbContext.Users.Remove(user);
            _dbContext.Tickets.Remove(ticket);
            _dbContext.SaveChanges();
            //Assert
            Assert.AreEqual("Already watching ticket", result.Message);
        }
        [TestMethod]
        public async Task StopWatchingTicket_Successful()
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
            var request = new WatchRequest(user.Id, ticket.Id, true);
            var result = await _watchManager.WatchTicket(request, user.Id);

            _dbContext.Users.Remove(user);
            _dbContext.Tickets.Remove(ticket);
            _dbContext.SaveChanges();
            //Assert
            Assert.AreEqual("Not watching anymore", result.Message);
        }
        [TestMethod]
        public async Task WatchAgain_Successful()
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
            var request = new WatchRequest(user.Id, ticket.Id, true);
            await _watchManager.WatchTicket(request, user.Id);

            request.isWatching = false;
            var result = await _watchManager.WatchTicket(request, user.Id);

            _dbContext.Users.Remove(user);
            _dbContext.Tickets.Remove(ticket);
            _dbContext.SaveChanges();
            //Assert
            Assert.AreEqual("Watching ticket again", result.Message);
        }
    }
}
