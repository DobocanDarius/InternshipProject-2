using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CommentManagerTests
{
    using InternshipProject_2.Controllers;
    using InternshipProject_2.Manager;
    using InternshipProject_2.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;

    namespace UnitTests.CommentManagerTests
    {
        [TestClass]
        public class GetAllFromTicket
        {
            [TestMethod]
            public async Task GetCommentsForTicket_ReturnsComments()
            {
                // Arrange
                var ticketId = 123;
                var commentsData = new List<Comment>
            {
                new Comment { TicketId = ticketId, Body = "Comment 1" },
                new Comment { TicketId = ticketId, Body = "Comment 2" }
            }.AsQueryable();

                var mockDbSet = new Mock<DbSet<Comment>>();
                mockDbSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(commentsData.Provider);
                mockDbSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(commentsData.Expression);
                mockDbSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(commentsData.ElementType);
                mockDbSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(commentsData.GetEnumerator());

                var mockDbContext = new Mock<Project2Context>();
                mockDbContext.Setup(c => c.Comments).Returns(mockDbSet.Object);

                var commentManager = new CommentManager(mockDbContext.Object);
                var controller = new CommentController(commentManager);

                // Act
                var result = await controller.GetCommentsForTicket(ticketId);

                // Assert
                Assert.IsNotNull(result);
                var commentsResult = result.Value as List<Comment>;
                Assert.IsNotNull(commentsResult);
                Assert.AreEqual(2, commentsResult.Count);
            }
        }
    }
}
