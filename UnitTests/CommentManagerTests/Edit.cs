using InternshipProject_2.Controllers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RequestResponseModels.Comment.Request;

namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class Edit
    {
        [TestMethod]
        public async Task EditComment_ValidEdit_ReturnsOk()
        {
            // Arrange
            var editComment = new CommentEditRequest
            {
                Id = 1002,
                Body = "Edited comment body",
                CreatedAt = DateTime.Now 
            };

            var mockCommentManager = new Mock<ICommentManager>();
            mockCommentManager.Setup(manager => manager.EditComment(It.IsAny<CommentEditRequest>())).Returns(Task.CompletedTask);
            var controller = new CommentController(mockCommentManager.Object);

            // Act
            var result = await controller.EditComment(editComment) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(editComment, result.Value);
        }

        [TestMethod]
        public async Task EditComment_InvalidComment_ThrowsException()
        {
            // Arrange
            var editComment = new CommentEditRequest
            {
                Id = 1002,
                Body = "Edited comment body",
                CreatedAt = DateTime.Now
            };
            var mockContext = new Mock<Project2Context>();
            mockContext.Setup(context => context.Comments.FindAsync(It.IsAny<int>()))
                .ReturnsAsync((Comment)null);

            var commentManager = new CommentManager(mockContext.Object);
            var controller = new CommentController(commentManager);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await controller.EditComment(editComment));
        }
    }
}
