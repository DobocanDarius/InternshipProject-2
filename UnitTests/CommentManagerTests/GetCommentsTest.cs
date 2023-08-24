using InternshipProject_2.Controllers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class GetCommentsTest
    {
        [TestMethod]
        public async Task GetCommentsForTicket_ReturnsComments()
        {
            // Arrange
            var ticketId = 1;
            var comments = new List<Comment>
            {
                new Comment { Id = 3, Body = "Updated Content", UserId = 1, TicketId = 1, CreatedAt = DateTime.Now },
                new Comment { Id = 1002, Body = "string", UserId = 1, TicketId = 1, CreatedAt = DateTime.Now },
                new Comment { Id = 1003, Body = "string", UserId = 1, TicketId = 1, CreatedAt = DateTime.Now }
            };

            var commentManagerMock = new Mock<ICommentManager>();
            commentManagerMock.Setup(cm => cm.GetComments(ticketId)).ReturnsAsync(comments);

            var controller = new CommentController(commentManagerMock.Object);

            // Act
            var result = await controller.GetCommentsForTicket(ticketId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Comment>>));
            if (result is ActionResult<List<Comment>> actionResult)
            {
                var okResult = actionResult.Result as OkObjectResult;
                Assert.IsNotNull(okResult);

                var returnedComments = okResult.Value as List<Comment>;
                Assert.IsNotNull(returnedComments);
                Assert.AreEqual(comments.Count, returnedComments.Count);
            }
            else
            {
                Assert.Fail("Unexpected result type");
            }
        }
    }
}
