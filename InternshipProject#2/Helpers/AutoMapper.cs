using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;


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

            cfg.CreateMap<AssignUserRequest, Assignee>();
            cfg.CreateMap<Assignee, AssignUserResponse>();

            cfg.CreateMap<Comment, CommentResponse>();
            cfg.CreateMap<CommentRequest, Comment>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}