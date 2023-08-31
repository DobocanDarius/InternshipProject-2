using InternshipProject_2.Models;

namespace InternshipProject_2.Manager
{
    public interface IAttachementManager
    {
        Task AddAsync(Attachement entity);
    }
}