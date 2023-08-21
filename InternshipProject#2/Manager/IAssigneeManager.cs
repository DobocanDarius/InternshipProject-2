namespace InternshipProject_2.Manager
{
    public interface IAssignmentManager
    {
        void AssignUserToTicket(int userId, int ticketId);
        int? GetAssignedUserIdForTicket(int ticketId);
    }
}
