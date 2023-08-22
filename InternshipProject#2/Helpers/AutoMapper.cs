using AutoMapper;
using InternshipProject_2.Models;

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
                
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}