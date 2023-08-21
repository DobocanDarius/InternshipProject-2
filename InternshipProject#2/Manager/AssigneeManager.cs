using InternshipProject_2.Models;

namespace InternshipProject_2.Manager
{
    public class AssigneeManager : IAssignmentManager
    {
        private Project2Context _dbContext;
        public AssigneeManager()
        {
            _dbContext = new Project2Context();
        }
        public void AssignUserToTicket(int userId, int ticketId)
        {
            var assigment = new Assignee
            {
                UserId = userId,
                TicketId = ticketId
            };
            _dbContext.Assignees.Add(assigment);
            _dbContext.SaveChanges();
        }

        public int? GetAssignedUserIdForTicket(int ticketId)
        {
            var assignment = _dbContext.Assignees.Where(a=> a.TicketId == ticketId).Select(a => a.UserId).FirstOrDefault();
            return assignment;
        }
    }
}
