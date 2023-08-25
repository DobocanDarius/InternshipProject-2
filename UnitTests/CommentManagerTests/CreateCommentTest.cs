using AutoMapper;
using InternshipProject_2.Manager;
using InternshipProject_2.Models;
using Moq;
using RequestResponseModels.Comment.Request;
namespace UnitTests.CommentManagerTests
{
    [TestClass]
    public class CreateCommetTest
    {
        private CommentManager _commentManager;
        private Project2Context _project2Context;
        private IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _project2Context = new Project2Context();
            _mapper = new Mock<IMapper>().Object;
            _commentManager = new CommentManager(_project2Context, _mapper);
        }
        [TestMethod]
        public async Task CreateComment_Success()
        {
            // Arrange
            var comment = new Comment
            {
                Body = "Test body",
                UserId = 1,
                TicketId = 1,
                CreatedAt = DateTime.UtcNow
            };
            _project2Context.Comments.Add(comment);
            _project2Context.SaveChanges();
            var request = new CommentRequest
            {
                Body = comment.Body,
                UserId = comment.UserId,
                TicketId = comment.TicketId,
                CreatedAt = comment.CreatedAt
            };
            // Act
            await _commentManager.CreateComment(request);
        }

        [TestMethod]
        public async Task CreateComment_Failure()
        {
            var commentRequest = new CommentRequest
            {
                Body = "Test body",
                UserId = 1,
                TicketId = 123,
                CreatedAt = DateTime.UtcNow
            };

            var commentManagerMock = new Mock<ICommentManager>();
            commentManagerMock.Setup(cm => cm.CreateComment(commentRequest)).ThrowsAsync(new Exception("Simulated exception"));
            var commentManager = commentManagerMock.Object;

            // Act
            async Task CreateCommentAction() => await commentManager.CreateComment(commentRequest);

            // Assert
            await Assert.ThrowsExceptionAsync<Exception>(CreateCommentAction);
        }

    }
}

