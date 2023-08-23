using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var ticketId = 123;
            var commentsData = new List<Comment>
            {
                new Comment { TicketId = ticketId, Text = "Comment 1" },
                new Comment { TicketId = ticketId, Text = "Comment 2" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Comment>>();
            mockDbSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(commentsData.Provider);
            mockDbSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(commentsData.Expression);
            mockDbSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(commentsData.ElementType);
            mockDbSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(commentsData.GetEnumerator());

            var mockDbContext = new Mock<YourDbContext>(); 
            mockDbContext.Setup(c => c.Comments).Returns(mockDbSet.Object);

            var commentManager = new CommentManager(mockDbContext.Object);
            var controller = new YourController(commentManager);

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
