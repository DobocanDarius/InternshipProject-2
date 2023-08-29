using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Comment.Request;
using RequestResponseModels.Comment.Response;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using RequestResponseModels.User.Request;
using RequestResponseModels.User.Response;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using RequestResponseModels.History.Request;
using RequestResponseModels.History.Response;
using RequestResponseModels.Watcher.Request;

namespace AutoMapper
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InternshipProject_2.Models.User, CreateUserResponse>();
                cfg.CreateMap<CreateUserRequest, InternshipProject_2.Models.User>();
                cfg.CreateMap<CommentRequest, InternshipProject_2.Models.Comment>();
                cfg.CreateMap<InternshipProject_2.Models.Comment, CommentResponse>();
                cfg.CreateMap<Ticket, TicketCreateResponse>();
                cfg.CreateMap<CommentEditRequest, InternshipProject_2.Models.Comment>();
                cfg.CreateMap<InternshipProject_2.Models.Comment, CommentResponse>();
                cfg.CreateMap<TicketCreateRequest, Ticket>();
                cfg.CreateMap<TicketEditRequest, Ticket>();
                cfg.CreateMap<AssignUserRequest, Assignee>();
                cfg.CreateMap<Assignee, AssignUserResponse>();
                cfg.CreateMap<InternshipProject_2.Models.User, GetAssignedUserResponse>();
                cfg.CreateMap<AddHistoryRecordRequest, InternshipProject_2.Models.History>();
                cfg.CreateMap<InternshipProject_2.Models.User, RequestResponseModels.Ticket.Response.User>();
                cfg.CreateMap<InternshipProject_2.Models.Comment, RequestResponseModels.Ticket.Response.Comment>();
                cfg.CreateMap<InternshipProject_2.Models.History, RequestResponseModels.Ticket.Response.History>();
                cfg.CreateMap<Ticket, TicketGetResponse>();
                cfg.CreateMap<WatchRequest, Watcher>()
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
