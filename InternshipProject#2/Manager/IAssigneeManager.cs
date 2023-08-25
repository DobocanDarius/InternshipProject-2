
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;

namespace InternshipProject_2.Manager
{
    public interface IAssigneeManager
    {
        public Task<AssignUserResponse> AssignUserToTicket(AssignUserRequest request);
        public Task<GetAssignedUserResponse> GetAssignedUser(GetAssignedUserRequest request);

    }
}