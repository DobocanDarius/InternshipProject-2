﻿using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using RequestResponseModels.User.Request;
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
                cfg.CreateMap<User, CreateUserResponse>();
                cfg.CreateMap<CreateUserRequest, User>();
                cfg.CreateMap<CommentRequest, Comment>();
                cfg.CreateMap<Comment, CommentResponse>();
                cfg.CreateMap<Ticket, TicketCreateResponse>();
                cfg.CreateMap<CommentEditRequest, Comment>();
                cfg.CreateMap<Comment, CommentResponse>();
              //  cfg.CreateMap<Ticket, TicketResponse>();
                cfg.CreateMap<TicketCreateRequest, Ticket>();
                cfg.CreateMap<TicketEditRequest, Ticket>();
                cfg.CreateMap<AssignUserRequest, Assignee>();
                cfg.CreateMap<Assignee, AssignUserResponse>();
                cfg.CreateMap<User, GetAssignedUserResponse>();

                cfg.CreateMap<TicketStatusRequest, Ticket>();
                cfg.CreateMap<Ticket, TicketStatusResponse>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
