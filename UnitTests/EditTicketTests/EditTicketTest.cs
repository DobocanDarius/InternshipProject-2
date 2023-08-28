using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.EditTicketTests
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
        public async Task EditTicketValidRequest()
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

            var request = new TicketEditRequest
            {
                Title = "111",
                Body = "111",
                Priority = "111",
            };

            var response = await _ticketManager.EditTicketAsync(request, ticket.Id, user.Id);
            Assert.AreEqual("You succesfully edited this ticket!", response.Message);
        }
    }
}
