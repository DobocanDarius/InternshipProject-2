using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;

namespace UnitTests.TicketManagerTests
{
    public class NewTicketTest
    {
        [TestClass]
        public class EditTicketTest
        {
            private TicketManager _ticketManager;
            private Project2Context _project2Context;

            [TestInitialize]
            public void Setup()
            {
                _project2Context = new Project2Context();
                _ticketManager = new TicketManager(_project2Context);
            }

            [TestMethod]
            public async Task NewTicketValidRequest()
            {
                var user = new User
                {
                    Username = "Test",
                    Password = "password",
                    Email = "email",
                    Role = "developer",
                    CreatedAt = DateTime.Now
                };
                _project2Context.Users.Add(user);
                _project2Context.SaveChanges();
                var ticket = new TicketCreateRequest
                {
                    Title = "Test",
                    Body = "Test",
                    Type = "Test",
                    Priority = "Test",
                    Component = "Test",
                };
                var response = await _ticketManager.CreateTicketAsync(ticket, user.Id);
                Assert.AreEqual("You succsessfully posted a new ticket!", response.Message);
            }
        }
    }
}
