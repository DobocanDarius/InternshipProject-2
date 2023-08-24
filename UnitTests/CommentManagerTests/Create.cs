using InternshipProject_2.Controllers;
using InternshipProject_2.Manager;
using Moq;
using RequestResponseModels.Comment.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class Create
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
            mockCommentManager.Verify(manager => manager.CreateComment(newComment), Times.Once);
        }
    }
}
