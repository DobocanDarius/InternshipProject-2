using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Assignee.Request;
using RequestResponseModels.Assignee.Response;
using RequestResponseModels.Watcher.Request;

namespace AutoMapper
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AssignUserRequest, Assignee>();
                cfg.CreateMap<Assignee, AssignUserResponse>();
                cfg.CreateMap<User, GetAssignedUserResponse>();
                cfg.CreateMap<WatchRequest, Watcher>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}