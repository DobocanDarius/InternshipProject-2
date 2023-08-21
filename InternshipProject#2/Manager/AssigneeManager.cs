using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;

namespace InternshipProject_2.Manager
{
    public class AssigneeManager
    {
        private Project2Context _dbContext;
        private Mapper map;
        public AssigneeManager()
        {
            _dbContext = new Project2Context();
            map = MapperConfig.InitializeAutomapper();
        }
        public async Task<AssignUserResponse> AssignUserToTicket(AssignUserRequest request)
        {
            try
            {
                var assignment = map.Map<Assignee>(request);
                _dbContext.Assignees.Add(assignment);
                await _dbContext.SaveChangesAsync();

                var response = new AssignUserResponse { Message = "User assigned successfully" };
                return response;
            }
            catch (Exception ex)
            {
                
                var response = new AssignUserResponse { Message = "Error assigning user" };
                return response;
            }
        }

        public int? GetAssignedUserIdForTicket(int ticketId)
        {
            var assignment = _dbContext.Assignees.Where(a=> a.TicketId == ticketId).Select(a => a.UserId).FirstOrDefault();
            return assignment;
        }
    }
}
