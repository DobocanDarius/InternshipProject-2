using InternshipProject_2.Controllers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class Delete
    {
        [TestMethod]
        public async Task DeleteComment_ValidDelete_ReturnsOk()
        {
            // Arrange
            var commentId = 1002;
            var mockContext = new Mock<Project2Context>();
            mockContext.Setup(context => context.Comments.FindAsync(It.IsAny<int>())).ReturnsAsync(new Comment { Id = commentId }); 
            var commentManager = new CommentManager(mockContext.Object);
            var controller = new CommentController(commentManager);

            // Act
            var result = await controller.DeleteComment(commentId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("The comment was deleted succesfully!", result.Value);
        }

        [TestMethod]
        public async Task DeleteComment_InvalidComment_ThrowsException()
        {
            // Arrange
            var commentId = 1002; 
            var mockContext = new Mock<Project2Context>();
            mockContext.Setup(context => context.Comments.FindAsync(It.IsAny<int>())).ReturnsAsync((Comment)null);
            var commentManager = new CommentManager(mockContext.Object);
            var controller = new CommentController(commentManager);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await controller.DeleteComment(commentId));
        }
    }
}
