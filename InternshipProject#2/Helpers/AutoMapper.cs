﻿using InternshipProject_2.Models;
using RequestResponseModels.User.Response;

using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;


namespace AutoMapper
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<User, LoginResponse>();
                cfg.CreateMap<AssignUserRequest, Assignee>();
                cfg.CreateMap<Assignee, AssignUserResponse>();
                cfg.CreateMap<User, GetAssignedUserResponse>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}