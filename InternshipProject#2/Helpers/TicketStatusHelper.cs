using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;

namespace InternshipProject_2.Helpers;

public class TicketStatusHelper
{
    readonly Project2Context _DbContext;
    readonly IMapper _Mapper;

    public TicketStatusHelper(Project2Context context)
    {
        _DbContext = context;
        _Mapper = MapperConfig.InitializeAutomapper();
    }

    public async Task<TicketStatusResponse> HandleStatusChange(Ticket dbTicket, Models.User dbUser, TicketStatusRequest ticketStatus)
    {
        if (Enum.IsDefined(typeof(TicketStatus), ticketStatus.Status))
        {
            int CurrentStatus = dbTicket.Status;
            Ticket ticket = _Mapper.Map(ticketStatus, dbTicket);
            switch ((TicketStatus)CurrentStatus)
            {
                case TicketStatus.ToDO:
                    if (dbUser.Role == "developer")
                    {
                        dbTicket.Status = (int)TicketStatus.ApprovedByDev;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    break;
                case TicketStatus.ApprovedByDev:
                    if (dbUser.Role == "developer")
                    {
                        dbTicket.Status = (int)TicketStatus.Construction;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    else if (dbUser.Role == "manager")
                    {
                        dbTicket.Status = (int)TicketStatus.ToDO;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    break;
                case TicketStatus.Construction:
                    if (dbUser.Role == "developer")
                    {
                        dbTicket.Status = (int)TicketStatus.TestingByDev;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    else if (dbUser.Role == "Manager" && dbUser.Role != "developer")
                    {
                        dbTicket.Status = (int)TicketStatus.ApprovedByDev;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    break;
                case TicketStatus.TestingByDev:
                    if (dbUser.Role == "Tester")
                    {
                        dbTicket.Status = (int)TicketStatus.TestingByTester;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    else if (dbUser.Role == "Manager")
                    {
                        dbTicket.Status = (int)TicketStatus.Construction;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    break;
                case TicketStatus.TestingByTester:
                    if (dbUser.Role == "Tester")
                    {
                        dbTicket.Status = (int)TicketStatus.Closed;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    else if (dbUser.Role == "Manager")
                    {
                        dbTicket.Status = (int)TicketStatus.Construction;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    break;
                case TicketStatus.Closed:
                    if (dbUser.Role == "Manager")
                    {
                        dbTicket.Status = (int)TicketStatus.TestingByTester;
                        _DbContext.Tickets.Update(ticket);
                        await _DbContext.SaveChangesAsync();
                    }
                    break;

                default:
                    break;
            }
            return new TicketStatusResponse { Message = $"The ticket's status is : {dbTicket.Status}" };
        }
        else
        {
            return new TicketStatusResponse { Message = "FAIL" };
        }
    }
}
