using InternshipProject_2.Manager;
using InternshipProject_2.Models;

using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;

namespace UnitTests.AssigneeManagerTests;

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

        User user = new User
        {
            Username = "Test",
            Password = "password",
            Email = "email",
            Role = "developer",
            CreatedAt = DateTime.Now

        };
        Ticket ticket = new Ticket
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
        AssignUserRequest request = new AssignUserRequest
        {
            UserId = user.Id,
            TicketId = ticket.Id
        };

        //Act
        AssignUserResponse result = await _assigneeManager.AssignUserToTicket(request);
        RemoveAssignedUserRequest requestRemove = new RemoveAssignedUserRequest
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
        User user = new User
        {
            Username = "Test",
            Password = "password",
            Email = "email",
            Role = "developer",
            CreatedAt = DateTime.Now
        };

        Ticket ticket = new Ticket
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

        RemoveAssignedUserRequest requestRemove = new RemoveAssignedUserRequest
        {
            TicketId = ticket.Id
        };

        // Act
        RemoveAssignedUserResponse result = await _assigneeManager.RemoveAssignedUser(requestRemove);

        // Assert
        Assert.AreEqual("No assigned user found", result.Message);
    }
}
