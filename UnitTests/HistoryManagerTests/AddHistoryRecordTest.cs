using InternshipProject_2.Helpers;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;

namespace UnitTests.HistoryManagerTests
{
    [TestClass]
    public class AddHistoryRecordTest
    {
        private HistoryManager _historyManager;
        private Project2Context _project2Context;
        private HistoryBodyGenerator _historyBodyGenerator;
        private AssigneeManager _assigneeManager;
        private CommentManager _commentManager;

        [TestInitialize]
        public void SetUp()
        {
            _project2Context = new Project2Context();
            _historyBodyGenerator = new HistoryBodyGenerator();
            _assigneeManager = new AssigneeManager();
            _commentManager = new CommentManager(_project2Context);
            _historyManager = new HistoryManager(_project2Context,_historyBodyGenerator);
            
        }
        [TestMethod]
        public async Task AddHistoryRecordCreateValidResponse()
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
                ReporterId = 1,
                CreatedAt = DateTime.Now
            };
           
            _project2Context.Tickets.Add(ticket);
            _project2Context.SaveChanges();


            var request = new AddHistoryRecordRequest
            {
                TicketId = ticket.Id,
                UserId = user.Id,
                EventType = HistoryEventType.Create
            };
            //Act

            var response = await _historyManager.AddHistoryRecord(request);
            var expectedResponse = _historyBodyGenerator.GenerateHistoryBody(request.EventType,request.UserId);
            string actualResponse = $"User {user.Id} created a ticket";
            //Assert
            Assert.AreEqual(actualResponse, response.Body);
        }

        [TestMethod]
        public async Task AddHistoryRecordAssignValidResponse()
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
                ReporterId = 1,
                CreatedAt = DateTime.Now
            };
            _project2Context.Tickets.Add(ticket);
            _project2Context.SaveChanges();
            var assignRequest = new AssignUserRequest
            {
                TicketId = ticket.Id,
                UserId = user.Id,
            };
            await _assigneeManager.AssignUserToTicket(assignRequest);
            var request = new AddHistoryRecordRequest
            {
                UserId = user.Id,
                TicketId = ticket.Id,
                EventType = HistoryEventType.Assign
            };

            //Act
            var response  = await _historyManager.AddHistoryRecord(request);
            var expectedResponse  = $"User {user.Id} was assigned to this ticket";

            //Assert
            Assert.AreEqual(expectedResponse, response.Body);
        }
        [TestMethod]

        public async Task AddHistoryRecordCommentValidResponse()
        {
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
                ReporterId = 1,
                CreatedAt = DateTime.Now
            };
            _project2Context.Tickets.Add(ticket);
            _project2Context.SaveChanges();

            var commentRequest = new CommentRequest
            {
                Body = "Test",
                TicketId = ticket.Id,
                UserId = user.Id,
                CreatedAt = DateTime.Now
            };
            await _commentManager.CreateComment(commentRequest);

            var request = new AddHistoryRecordRequest
            {
                UserId = user.Id,
                TicketId = ticket.Id,
                EventType = HistoryEventType.Comment
            };

            //Act
            var response = await _historyManager.AddHistoryRecord(request);
            var expectedResponse = $"User {user.Id} added a comment";

            //Assert
            Assert.AreEqual(expectedResponse, response.Body);
        }

    }
}
