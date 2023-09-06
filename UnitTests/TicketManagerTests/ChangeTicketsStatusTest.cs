using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Moq;
using RequestResponseModels.Ticket.Request;

namespace UnitTests.TicketManagerTests
{
    [TestClass]
    public class ChangeTicketsStatusTest
    {
        Mock<Project2Context> _project2Context;
        TicketManager _ticketManager;
        [TestInitialize]
        public void SetUp()
        {
            _project2Context = new Mock<Project2Context>();
            _ticketManager = new TicketManager(_project2Context.Object);
        }

        [TestMethod]
        public async Task ChangeTicketsStatus_ValidChange_Success()
        {
            // Arrange
            int reporterId = 16005;
            int ticketId = 16007;
            var ticketStatusRequest = new TicketStatusRequest { Status = (int)TicketStatus.ApprovedByDev };

            var dbUser = new User { Id = reporterId, Role = "developer" };
            var dbTicket = new Ticket { Id = ticketId, ReporterId = reporterId, Status = (int)TicketStatus.ToDO };

            _project2Context.Setup(c => c.Users.FindAsync(reporterId)).ReturnsAsync(dbUser);
            _project2Context.Setup(c => c.Tickets.FindAsync(ticketId)).ReturnsAsync(dbTicket);

            // Act
            RequestResponseModels.Ticket.Response.TicketStatusResponse result = await _ticketManager.ChangeTicketsStatus(ticketStatusRequest, reporterId, ticketId);

            // Assert
            _project2Context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.AreEqual("The ticket's status is : 2", result.Message);
        }

        [TestMethod]
        public async Task ChangeTicketsStatus_InvalidChange_Fail()
        {
            // Arrange
            int reporterId = 1;
            int ticketId = 1;
            var ticketStatusRequest = new TicketStatusRequest { Status = (int)(TicketStatus)100 };

            User dbUser = new User { Id = reporterId, Role = "developer" };
            Ticket dbTicket = new Ticket { Id = ticketId, ReporterId = reporterId, Status = (int)TicketStatus.ToDO };

            _project2Context.Setup(c => c.Users.FindAsync(reporterId)).ReturnsAsync(dbUser);
            _project2Context.Setup(c => c.Tickets.FindAsync(ticketId)).ReturnsAsync(dbTicket);

            // Act
            RequestResponseModels.Ticket.Response.TicketStatusResponse result = await _ticketManager.ChangeTicketsStatus(ticketStatusRequest, reporterId, ticketId);

            // Assert
            _project2Context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            Assert.AreEqual("FAIL", result.Message);
        }

    }
}
