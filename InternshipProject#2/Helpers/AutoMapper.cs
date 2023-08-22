using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;

namespace AutoMapper
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Ticket, TicketResponse>();
                cfg.CreateMap<TicketRequest, Ticket>();
                cfg.CreateMap<User, CreateUserResponse>();
                cfg.CreateMap<CreateUserRequest, User>();
                cfg.CreateMap<LoginRequest, User>();
                cfg.CreateMap<Ticket, TicketResponse>();
                cfg.CreateMap<TicketRequest, Ticket>();

                cfg.CreateMap<AssignUserRequest, Assignee>();
                cfg.CreateMap<Assignee, AssignUserResponse>();
                cfg.CreateMap<User, GetAssignedUserResponse>();

                cfg.CreateMap<CommentRequest, Comment>();
                cfg.CreateMap<Comment, CommentResponse>();

                cfg.CreateMap<CommentEditRequest, Comment>();
                cfg.CreateMap<Comment, CommentResponse>();

            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}