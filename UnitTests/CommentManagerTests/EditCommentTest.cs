using Microsoft.EntityFrameworkCore;
using Moq;

using AutoMapper;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Comment.Request;

namespace UnitTests.CommentManagerTests;

[TestClass]
public class EditCommentTest
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
    public async Task EditComment_Success()
    {
        // Arrange
        Comment comment = new Comment
        {
            Body = "Test body",
            UserId = 1,
            TicketId = 1,
            CreatedAt = DateTime.UtcNow
        };
        CommentEditRequest editCommentRequest = new CommentEditRequest
        {
            Id = 4002,
            Body = "Updated body",
        };
        _project2Context.Comments.Update(comment);
        _project2Context.SaveChanges();
        // Act
        await _commentManager.EditComment(editCommentRequest,1);
    }

    [TestMethod]
    public async Task EditComment_Failure()
    {
        // Arrange
        List<Comment> comments = new List<Comment>
        {
            new Comment { Id = 4002, Body = "Test body", UserId = 1, TicketId = 1, CreatedAt = DateTime.UtcNow }
        };
        CommentEditRequest editCommentRequest = new CommentEditRequest
        {
            Id = 4002,
            Body = "Updated body"
        };
        Mock<Project2Context> dbContextMock = new Mock<Project2Context>();
        Mock<DbSet<Comment>> dbSetMock = new Mock<DbSet<Comment>>();
        dbSetMock.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(comments.AsQueryable().Provider);
        dbSetMock.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(comments.AsQueryable().Expression);
        dbSetMock.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(comments.AsQueryable().ElementType);
        dbSetMock.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(comments.AsQueryable().GetEnumerator());
        dbContextMock.Setup(db => db.Comments).Returns(dbSetMock.Object);
        CommentManager commentManager = new CommentManager(dbContextMock.Object, null);

        // Act and Assert
        await Assert.ThrowsExceptionAsync<Exception>(async () =>
        {
            await commentManager.EditComment(editCommentRequest,1);
        });
    }
}
