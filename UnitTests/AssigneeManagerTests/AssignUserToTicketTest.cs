using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.AssigneeManagerTests
{
    [TestClass]
    public class AssignUserToTicketTest
    {
        private AssigneeManager _assigneeManager;
        private Project2Context _project2Context;

        [TestInitialize]
        public void Setup()
        {
            _assigneeManager = new AssigneeManager();
            _project2Context = new Project2Context();
        }

        [TestMethod]
        public async Task AssignUserToTicket_ValidRequest_ReturnsSuccessResponse()
        {
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
            var request = new AssignUserRequest
            {
                UserId = user.Id,
                TicketId = ticket.Id
            };

            AssignUserResponse result = await _assigneeManager.AssignUserToTicket(request);

            Assert.AreEqual("User assigned successfully", result.Message);
        }
    }
}
