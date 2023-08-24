using Microsoft.VisualStudio.TestTools.UnitTesting;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.AssigneeManagerTests
{
    [TestClass]
    public class GetAssignedUserTest
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
        public async Task GetAssignedUserAssignmentExists()
        {
            // Arrange
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
                ReporterId = 1,
                CreatedAt = DateTime.Now
            };
            _project2Context.Tickets.Add(ticket);
            _project2Context.SaveChanges();
            var assignment = new Assignee
            {
                UserId = user.Id,
                TicketId = ticket.Id
            };

            
          
            _project2Context.Assignees.Add(assignment);
            _project2Context.SaveChanges();

            var request = new GetAssignedUserRequest
            {
                TicketId = ticket.Id
            };

            // Act
            GetAssignedUserResponse result = await _assigneeManager.GetAssignedUser(request);
            Console.WriteLine($"User CreatedAt: {user.CreatedAt}");
            Console.WriteLine($"Result CreatedAt: {result.CreatedAt}");

            // Assert

            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(user.Role, result.Role);
            
        }

        [TestMethod]
        public async Task GetAssignedUserNoAssignmentExists()
        {
            // Arrange
            var request = new GetAssignedUserRequest
            {
                TicketId = 123
            };

            // Act
            GetAssignedUserResponse result = await _assigneeManager.GetAssignedUser(request);

            // Assert
            Assert.AreEqual("N/A", result.Username);
            Assert.AreEqual("N/A", result.Role);
            Assert.AreEqual(DateTime.MinValue, result.CreatedAt);
        }

    }
}
