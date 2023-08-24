using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class CreateCommetTest
    {
        [TestMethod]
        public async Task CreateComment_ValidData()
        {
            //Arrange
            var mockCommentManager = new Mock<ICommentManager>();
            var controller = new CommentController(mockCommentManager.Object);

            var newComment = new CommentRequest
            {
                Body = "This is the comment body.",
                UserId = 1,
                TicketId = 1,
                CreatedAt = DateTime.UtcNow
            };

            // Act
            var result = await controller.CreateComment(newComment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            mockCommentManager.Verify(manager => manager.CreateComment(newComment), Times.Once);
        }
    }
}
