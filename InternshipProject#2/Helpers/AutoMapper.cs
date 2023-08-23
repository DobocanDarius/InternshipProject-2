using AutoMapper;
using InternshipProject_2.Models;
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
                cfg.CreateMap<CommentRequest, Comment>();
                cfg.CreateMap<Comment, CommentResponse>();

            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}