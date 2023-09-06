﻿using AutoMapper;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.CommentManagerTests;

[TestClass]
public class GetCommentsTest
{
    CommentManager _commentManager;
    Project2Context _project2Context;
    IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        _project2Context = new Project2Context();
        _mapper = new Mock<IMapper>().Object;
        _commentManager = new CommentManager(_project2Context, _mapper);
    }
    [TestMethod]
    public async Task GetCommentsForTicket_ReturnsComments()
    {
        // Arrange
        int ticketId = 1;
        List<Comment> comments = new List<Comment>
        {
            new Comment { Id = 3, Body = "Updated Content", UserId = 1, TicketId = 1, CreatedAt = DateTime.Now },
            new Comment { Id = 1002, Body = "string", UserId = 1, TicketId = 1, CreatedAt = DateTime.Now },
            new Comment { Id = 1003, Body = "string", UserId = 1, TicketId = 1, CreatedAt = DateTime.Now },
        };
        IQueryable<Comment> commentsQueryable = comments.AsQueryable();
        Mock<DbSet<Comment>> commentsDbSetMock = new Mock<DbSet<Comment>>();
        commentsDbSetMock.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(commentsQueryable.Provider);
        commentsDbSetMock.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(commentsQueryable.Expression);
        commentsDbSetMock.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(commentsQueryable.ElementType);
        commentsDbSetMock.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(commentsQueryable.GetEnumerator());
        Mock<Project2Context> dbContextMock = new Mock<Project2Context>();
        dbContextMock.Setup(db => db.Comments).Returns(commentsDbSetMock.Object);
        _project2Context = dbContextMock.Object;

        // Act
        var result = await _commentManager.GetComments(ticketId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(comments.Count, result.Count());
    }
}
