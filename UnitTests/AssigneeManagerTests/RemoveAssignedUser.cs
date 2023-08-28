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
    public class RemoveAssignedUser
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
        public async Task RemoveAssignedUserSucces()
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

            //Act
            AssignUserResponse result = await _assigneeManager.AssignUserToTicket(request);
            var requestRemove = new RemoveAssignedUserRequest
            {
                TicketId = ticket.Id
            };
            RemoveAssignedUserResponse resultFinal = await _assigneeManager.RemoveAssignedUser(requestRemove);
            //Assert
            Assert.AreEqual("Assigned user removed successfully", resultFinal.Message);
        }
        [TestMethod]
        public async Task RemoveAssignedUser_NoAssignedUserFound()
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

            var requestRemove = new RemoveAssignedUserRequest
            {
                TicketId = ticket.Id
            };

            // Act
            RemoveAssignedUserResponse result = await _assigneeManager.RemoveAssignedUser(requestRemove);

            // Assert
            Assert.AreEqual("No assigned user found", result.Message);
        }
    }

}
