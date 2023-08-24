﻿using InternshipProject_2.Controllers;
using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RequestResponseModels.Comment.Request;
using System;
using System.Threading.Tasks;
namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class CreateCommetTest
    {
        [TestMethod]
        public async Task CreateComment_Success()
        {
            // Arrange
            var commentRequest = new CommentRequest
            {
                Body = "Test body",
                UserId = 1,
                TicketId = 1,
                CreatedAt = DateTime.UtcNow
            };

            var commentManagerMock = new Mock<ICommentManager>();
            commentManagerMock.Setup(cm => cm.CreateComment(commentRequest)).Returns(Task.CompletedTask);

            var controller = new CommentController(commentManagerMock.Object);

            // Act
            var result = await controller.CreateComment(commentRequest);

            // Assert
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task CreateComment_Failure()
        {
            // Arrange
            var commentRequest = new CommentRequest
            {
                Body = "Test body",
                UserId = 1,
                TicketId = 123,
                CreatedAt = DateTime.UtcNow 
            };

            var commentManagerMock = new Mock<ICommentManager>();
            commentManagerMock.Setup(cm => cm.CreateComment(commentRequest)).ThrowsAsync(new Exception("Simulated exception"));

            var controller = new CommentController(commentManagerMock.Object);

            // Act
            var result = await controller.CreateComment(commentRequest);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("An error occurred: Simulated exception", badRequestResult.Value);
        }
    }
}

