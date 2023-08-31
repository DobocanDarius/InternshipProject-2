using InternshipProject_2.Manager;
using InternshipProject_2.Models;

namespace UnitTests.TicketManagerTests
{
    [TestClass]
    public class DeleteTicket
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
        public async Task DeleteTicketValidRequest()
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
            var ticket = new Ticket
            {
                Title = "Test",
                Body = "Test",
                Type = "Test",
                Priority = "Test",
                Component = "Test",
                ReporterId = user.Id,
                CreatedAt = DateTime.Now
            };
            _project2Context.Tickets.Add(ticket);
            _project2Context.SaveChanges();

            var response = await _ticketManager.DeleteTicketAsync(ticket.Id, user.Id);
            Assert.AreEqual("You succesfully deleted this ticket!", response.Message);
            }
        }
}
