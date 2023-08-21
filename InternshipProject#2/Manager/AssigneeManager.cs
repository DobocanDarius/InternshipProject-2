using AutoMapper;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;

namespace InternshipProject_2.Manager
{
    public class AssigneeManager : IAssigneeManager
    {
        private Project2Context _dbContext;
        private readonly Mapper map;
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

        public async Task<GetAssignedUserResponse> GetAssignedUser(GetAssignedUserRequest request)
        {
            try
            {
                var assignment = await _dbContext.Assignees
                    .FirstOrDefaultAsync(a => a.TicketId == request.TicketId);

                if (assignment != null)
                {
                    int userId = assignment.UserId;
                    var user = await _dbContext.Users.FindAsync(userId);
                    if (user != null)
                    {
                        var userResponse = map.Map<GetAssignedUserResponse>(user);
                        return userResponse;
                    }
                    else
                    {
                        var response = new GetAssignedUserResponse { Username = "N/A", Role = "N/A", CreatedAt = DateTime.MinValue };
                        return response;
                    }

                }
                else
                {

                    var response = new GetAssignedUserResponse { Username = "N/A", Role = "N/A", CreatedAt = DateTime.MinValue };
                    return response;
                }
            }
            catch (Exception ex)
            {

                var response = new GetAssignedUserResponse { Username = "Error", Role = "Error", CreatedAt = DateTime.MinValue };
                return response;
            }
        }
        public async Task<RemoveAssignedUserResponse> RemoveAssignedUser(RemoveAssignedUserRequest request)
        {
            try
            {
                var assigment = await _dbContext.Assignees.FirstOrDefaultAsync(a => a.TicketId == request.TicketId);
                if (assigment !=null)
                {
                    _dbContext.Assignees.Remove(assigment);
                    await _dbContext.SaveChangesAsync();
                    var response = new RemoveAssignedUserResponse { Message = "Assigned user removed successfully" };
                    return response;
                }
                else
                {
                    var response = new RemoveAssignedUserResponse { Message = "No assigned user found" };
                    return response;
                }
            }
            catch (Exception ex)
            {
               var response = new RemoveAssignedUserResponse { Message = "Error removing assignment" };
                return response;
            }
        }
    }
}
