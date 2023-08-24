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
    public class DeleteAllFromTicket
    {
        [TestMethod]
        public async Task DeleteCommentsByTicketId_ValidTicketId_ReturnsOk()
        {
            // Arrange
            var ticketId = 1; 
            var commentsToDelete = new List<Comment>
            {
                new Comment { Id = 1002, TicketId = ticketId },
                new Comment { Id = 2002, TicketId = ticketId }
            };
            var mockContext = new Mock<Project2Context>();
            mockContext.Setup(context => context.Comments.RemoveRange(It.IsAny<IEnumerable<Comment>>())).Callback<IEnumerable<Comment>>(comments => commentsToDelete.RemoveAll(c => comments.Contains(c)));
            var commentManager = new CommentManager(mockContext.Object);
            var controller = new CommentController(commentManager);

            // Act
            var result = await controller.DeleteCommentsFromTicket(ticketId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("The comments are deleted!", result.Value);
        }

        [TestMethod]
        public async Task DeleteCommentsByTicketId_NoCommentsToDelete_ReturnsOk()
        {
            // Arrange
            var ticketId = 1002;
            var mockContext = new Mock<Project2Context>();
            var commentManager = new CommentManager(mockContext.Object);
            var controller = new CommentController(commentManager);

            // Act
            var result = await controller.DeleteCommentsFromTicket(ticketId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("The comments are deleted!", result.Value);
        }
    }
}
