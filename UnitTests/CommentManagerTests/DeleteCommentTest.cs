using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class DeleteCommentTest
    {
        [TestMethod]
        public async Task DeleteComment_ValidDelete_ReturnsOk()
        {
            // Arrange
            var commentId = 1002;
            var comments = new[]
            {
                new Comment { Id = commentId }
            };

            var dbContextMock = new Mock<Project2Context>();
            var dbSetMock = new Mock<DbSet<Comment>>();
            dbSetMock.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(comments.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(comments.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(comments.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(comments.AsQueryable().GetEnumerator());

            dbContextMock.Setup(db => db.Comments).Returns(dbSetMock.Object);

            var commentManager = new CommentManager(dbContextMock.Object);

            // Act
            await commentManager.DeleteComment(commentId,1);

            // Assert
            dbSetMock.Verify(m => m.Remove(It.IsAny<Comment>()), Times.Once());
        }
    }
}
