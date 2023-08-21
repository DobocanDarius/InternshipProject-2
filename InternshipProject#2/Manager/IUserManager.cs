using RequestResponseModels.User.Request;

namespace InternshipProject_2.Manager
{
    public interface IUserManager
    {
        Task Create(CreateUserRequest newUser);
    }
}