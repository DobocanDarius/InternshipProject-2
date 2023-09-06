using Microsoft.EntityFrameworkCore;

using AutoMapper;

using InternshipProject_2.Helpers;
using InternshipProject_2.Models;

using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using RequestResponseModels.History.Enum;
using RequestResponseModels.History.Request;

namespace InternshipProject_2.Manager;

public class AssigneeManager : IAssigneeManager
{
    Project2Context _DbContext;
    readonly Mapper _Map;
    readonly HistoryBodyGenerator _HistoryBodyGenerator;
    HistoryWritter _HistoryWritter;
    public AssigneeManager()
    {
        _DbContext = new Project2Context();
        _Map = MapperConfig.InitializeAutomapper();
        _HistoryBodyGenerator = new HistoryBodyGenerator();
        _HistoryWritter = new HistoryWritter(_DbContext, _HistoryBodyGenerator);
    }
    public async Task<AssignUserResponse> AssignUserToTicket(AssignUserRequest request)
    {
        try
        {
            User? user = await _DbContext.Users.FindAsync(request.UserId);
            if (user == null)
            {
                var response = new AssignUserResponse 
                { 
                    Message = "User not found" 
                };
                return response;
            }
            if (!string.Equals(user.Role, "developer", StringComparison.OrdinalIgnoreCase))
            {
                var response = new AssignUserResponse 
                { 
                    Message = "User is not a developer" 
                };
                return response;
            }
            Assignee? existingAssignment = await _DbContext.Assignees.FirstOrDefaultAsync(a => a.TicketId == request.TicketId);
            if (existingAssignment != null)
            {
                var response = new AssignUserResponse 
                { 
                    Message = "An assignment already exists for this ticket" 
                };
                return response;
            }

            Assignee assignment = _Map.Map<Assignee>(request);

            _DbContext.Assignees.Add(assignment);
            await _DbContext.SaveChangesAsync();
            AddHistoryRecordRequest historyRequest = new AddHistoryRecordRequest 
            { 
                UserId = request.UserId,
                TicketId = request.TicketId,
                EventType = HistoryEventType.Assign 
            };
            await _HistoryWritter.AddHistoryRecord(historyRequest);

            AssignUserResponse succesResponse = new AssignUserResponse 
            { 
                Message = "User assigned successfully" 
            };
            return succesResponse;
        }
        catch
        {
            AssignUserResponse response = new AssignUserResponse 
            { 
                Message = "Error assigning user" 
            };
            return response;
        }
    }
    public async Task<GetAssignedUserResponse> GetAssignedUser(GetAssignedUserRequest request)
    {
        try
        {
            Assignee? assignment = await _DbContext.Assignees
                .FirstOrDefaultAsync(a => a.TicketId == request.TicketId);

            if (assignment != null)
            {
                User? user = await _DbContext.Users.FindAsync(assignment.UserId);

                if (user != null)
                {
                    var userResponse = _Map.Map<GetAssignedUserResponse>(user);
                    return userResponse;
                }
            }
            return new GetAssignedUserResponse 
            { 
                Username = "N/A", 
                Role = "N/A", 
                CreatedAt = DateTime.MinValue 
            };
        }
        catch
        {
            return new GetAssignedUserResponse 
            { 
                Username = "Error", 
                Role = "Error",
                CreatedAt = DateTime.MinValue 
            };
        }
    }

    public async Task<RemoveAssignedUserResponse> RemoveAssignedUser(RemoveAssignedUserRequest request)
    {
        try
        {
            Assignee? assignment = await _DbContext.Assignees.FirstOrDefaultAsync(a => a.TicketId == request.TicketId);
            if (assignment != null)
            {
                _DbContext.Assignees.Remove(assignment);
                await _DbContext.SaveChangesAsync();
                RemoveAssignedUserResponse response = new RemoveAssignedUserResponse 
                { 
                    Message = "Assigned user removed successfully" 
                };
                return response;
            }
            else
            {
                RemoveAssignedUserResponse response = new RemoveAssignedUserResponse 
                { 
                    Message = "No assigned user found" 
                };
                return response;
            }
        }
        catch
        {
            RemoveAssignedUserResponse response = new RemoveAssignedUserResponse 
            { 
                Message = "Error removing assignment" 
            };
            return response;
        }
    }
}

