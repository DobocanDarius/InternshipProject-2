using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;

namespace InternshipProject_2.Manager
{

    public class AssigneeManager : IAssigneeManager
    {
        private Project2Context _dbContext;
        private readonly Mapper map;
        private readonly HistoryBodyGenerator historyBodyGenerator;
        private HistoryWritter historyWritter;
        public AssigneeManager()
        {
            _dbContext = new Project2Context();
            map = MapperConfig.InitializeAutomapper();
            historyBodyGenerator = new HistoryBodyGenerator();
            historyWritter = new HistoryWritter(_dbContext, historyBodyGenerator);
        }
        public async Task<AssignUserResponse> AssignUserToTicket(AssignUserRequest request)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(request.UserId);
                if (user == null)
                {
                    var response = new AssignUserResponse { Message = "User not found" };
                    return response;
                }
                if (!string.Equals(user.Role, "developer", StringComparison.OrdinalIgnoreCase))
                {
                    var response = new AssignUserResponse { Message = "User is not a developer" };
                    return response;
                }
                var existingAssignment = await _dbContext.Assignees.FirstOrDefaultAsync(a => a.TicketId == request.TicketId);
                if (existingAssignment != null)
                {
                    var response = new AssignUserResponse { Message = "An assignment already exists for this ticket" };
                    return response;
                }

                var assignment = map.Map<Assignee>(request);

                _dbContext.Assignees.Add(assignment);
                await _dbContext.SaveChangesAsync();
                var historyRequest = new AddHistoryRecordRequest { UserId = request.UserId, TicketId = request.TicketId, EventType = HistoryEventType.Assign };
                await historyWritter.AddHistoryRecord(historyRequest);

                var succesResponse = new AssignUserResponse { Message = "User assigned successfully" };
                return succesResponse;
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
                    var user = await _dbContext.Users.FindAsync(assignment.UserId);

                    if (user != null)
                    {
                        var userResponse = map.Map<GetAssignedUserResponse>(user);
                        return userResponse;
                    }
                }
                return new GetAssignedUserResponse { Username = "N/A", Role = "N/A", CreatedAt = DateTime.MinValue };
            }
            catch (Exception ex)
            {
                return new GetAssignedUserResponse { Username = "Error", Role = "Error", CreatedAt = DateTime.MinValue };
            }
        }

        public async Task<RemoveAssignedUserResponse> RemoveAssignedUser(RemoveAssignedUserRequest request)
        {
            try
            {
                var assignment = await _dbContext.Assignees.FirstOrDefaultAsync(a => a.TicketId == request.TicketId);
                if (assignment != null)
                {
                    _dbContext.Assignees.Remove(assignment);
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

