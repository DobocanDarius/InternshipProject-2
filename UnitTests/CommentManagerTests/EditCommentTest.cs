using InternshipProject_2.Controllers;
using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Mvc;
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
    public class EditCommentTest
    {
        [TestMethod]
        public async Task EditComment_Success()
        {
            // Arrange
            var editCommentRequest = new CommentEditRequest
            {
                Id = 1002,
                Body = "Updated body",
            };
            var commentManagerMock = new Mock<ICommentManager>();
            commentManagerMock.Setup(cm => cm.EditComment(editCommentRequest)).Returns(Task.CompletedTask);

            var controller = new CommentController(commentManagerMock.Object);

            // Act
            var result = await controller.EditComment(editCommentRequest);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task EditComment_Failure()
        {
            // Arrange
            var editCommentRequest = new CommentEditRequest
            {
                Id = 1,
                Body = "Updated body"
            };

            var commentManagerMock = new Mock<ICommentManager>();
            commentManagerMock.Setup(cm => cm.EditComment(editCommentRequest)).ThrowsAsync(new Exception("Simulated exception"));

            var controller = new CommentController(commentManagerMock.Object);

            // Act
            var result = await controller.EditComment(editCommentRequest);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("An error occurred: Simulated exception", badRequestResult.Value);
        }
    }
}
