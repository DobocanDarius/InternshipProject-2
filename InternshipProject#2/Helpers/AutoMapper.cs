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
using RequestResponseModels.Watcher.Request;
using History = InternshipProject_2.Models.History;
using RequestResponseModels.History.Response;

namespace AutoMapper;

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
            cfg.CreateMap<AddHistoryRecordRequest, History>();
            cfg.CreateMap<History,AddHistoryRecordResponse>();
            cfg.CreateMap<InternshipProject_2.Models.User, GetAssignedUserResponse>();
            cfg.CreateMap<AddHistoryRecordRequest, History>();
            cfg.CreateMap<InternshipProject_2.Models.User, RequestResponseModels.Ticket.Response.User>();
            cfg.CreateMap<InternshipProject_2.Models.Comment, RequestResponseModels.Ticket.Response.Comment>().ForMember(x => x.Username, o => o.MapFrom(e => e.User.Username));
            cfg.CreateMap<History, RequestResponseModels.Ticket.Response.History>();
            cfg.CreateMap<InternshipProject_2.Models.Watcher, RequestResponseModels.Ticket.Response.Watcher>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
            cfg.CreateMap<Ticket, TicketGetResponse>();
            cfg.CreateMap<WatchRequest, InternshipProject_2.Models.Watcher>();
            cfg.CreateMap<TicketStatusRequest, Ticket>();
            cfg.CreateMap<LogoutRequest, InactiveToken>();
            cfg.CreateMap<InternshipProject_2.Models.Attachement, RequestResponseModels.Ticket.Response.Attachement>().ForMember(x => x.Link, o => o.MapFrom(e => e.Link));
        });

        var mapper = new Mapper(config);
        return mapper;
    }
}
