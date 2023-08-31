using RequestResponseModels.Assignee.Response;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace InternshipProject_2.Manager;

public interface IUserManager
{
    Task<LoginResponse> Login(LoginRequest user);
    Task<CreateUserResponse> Create(CreateUserRequest newUser);
    Task<LogoutResponse> Logout(LogoutRequest request);
}