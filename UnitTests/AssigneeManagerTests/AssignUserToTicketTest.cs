using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;

namespace UnitTests.AssigneeManagerTests;

[TestClass]
public class AssignUserToTicketTest
{
    AssigneeManager _assigneeManager;
    Project2Context _project2Context;

    [TestInitialize]
    public void Setup()
    {
        _assigneeManager = new AssigneeManager();
        _project2Context = new Project2Context();
    }

    [TestMethod]
    public async Task AssignUserToTicketValidRequest()
    {
        //Arrange
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

        //Assert
        Assert.AreEqual("User assigned successfully", result.Message);
    }
    [TestMethod]
    public async Task AssignUserToTicketUserNotExist()
    {
        //Arrange
        AssignUserRequest request = new AssignUserRequest
        {
            UserId = 999,
            TicketId = 2
        };

        //Act
        AssignUserResponse result = await _assigneeManager.AssignUserToTicket(request);

        //Assert
        Assert.AreEqual("User not found", result.Message);
    }
    [TestMethod]
    public async Task AssignUserToTicketUserNotDeveloper()
    {
        //Arrange
        User user = new User
        {
            Username = "Test",
            Password = "password",
            Email = "email",
            Role = "reporter",
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

        //Assert
        Assert.AreEqual("User is not a developer", result.Message);
    }

    [TestMethod]
    public async Task AssignUserToTicketAssignmentAlreadyExists()
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
        Assignee existingAssignment = new Assignee
        {
            UserId = user.Id,
            TicketId = ticket.Id
        };
        _project2Context.Assignees.Add(existingAssignment);
        _project2Context.SaveChanges();

        AssignUserRequest request = new AssignUserRequest
        {
            UserId = user.Id,
            TicketId = ticket.Id
        };

        // Act
        AssignUserResponse result = await _assigneeManager.AssignUserToTicket(request);

        // Assert
        Assert.AreEqual("An assignment already exists for this ticket", result.Message);
    }
}


