using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;

namespace UnitTests.HistoryManagerTests
{
    [TestClass]
    public class GetHistoryTest
    {
        private Project2Context _project2Context;
        private HistoryManager historyManager;
        private HistoryWritter historyWritter;
        private HistoryBodyGenerator historyBodyGenerator;

        [TestInitialize]
        public void Setup()
        {
            _project2Context = new Project2Context();
            historyBodyGenerator = new HistoryBodyGenerator();
            historyManager = new HistoryManager(_project2Context);
            historyWritter = new HistoryWritter(_project2Context, historyBodyGenerator);
        }

        [TestMethod]
        public async Task GetHistoryValidResponse()
        {
            //Arrange
            var user = new User
            {
                Username = "Test",
                Password = "password",
                Email = "email",
                Role = "tester",
                CreatedAt = DateTime.Now
            };
            _project2Context.Users.Add(user);
            _project2Context.SaveChanges();
            var ticket = new Ticket
            {
                Title = "Test",
                Body = "Test",
                Type = "Test",
                Priority = "Test",
                Component = "Test",
                ReporterId = user.Id,
                CreatedAt = DateTime.Now
            };

            _project2Context.Tickets.Add(ticket);
            _project2Context.SaveChanges();


            var request = new GetHistoryRequest { TicketId = ticket.Id };
            var addHistoryRecordRequest = new AddHistoryRecordRequest
            {
                TicketId = ticket.Id,
                UserId = user.Id,
                EventType = HistoryEventType.Create
            };
            var addHistoryRecordRequest2 = new AddHistoryRecordRequest
            {
                TicketId = ticket.Id,
                UserId = user.Id,
                EventType = HistoryEventType.Close
            };
            
            //Act
            var addResponse = await historyWritter.AddHistoryRecord(addHistoryRecordRequest);
            var addResponse2 = await historyWritter.AddHistoryRecord(addHistoryRecordRequest2);

            var response = await historyManager.GetHistory(request);

            //Assert

            Assert.AreEqual(user.Username + " created a ticket", response.HistoryRecords[0].Body);
            Assert.AreEqual(user.Username + " closed the ticket", response.HistoryRecords[1].Body);
            
        }

        [TestMethod]
        public async Task GetHistoryNoRecords_ResponseContainsErrorMessage()
        {
            // Arrange
            var ticketId = 999;
            var request = new GetHistoryRequest { TicketId = ticketId };


            // Act
            var response = await historyManager.GetHistory(request);

            // Assert
           
            Assert.AreEqual("No history records found for this ticket.", response.HistoryRecords[0].Body);
        }
    }

}

