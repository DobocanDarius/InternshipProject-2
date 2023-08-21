using AutoMapper;
using InternshipProject_2.Models;
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
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}