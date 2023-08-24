﻿using RequestResponseModels.Assignee.Response;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace InternshipProject_2.Manager;

public interface IUserManager
{
    Task<CreateUserResponse> Create(CreateUserRequest newUser);
}