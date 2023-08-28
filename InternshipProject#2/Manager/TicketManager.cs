using AutoMapper;
using InternshipProject_2.Helpers;
using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
namespace InternshipProject_2.Manager
{
    public class TicketManager : ITicketManager
    {
        private readonly Project2Context _context;

        public TicketManager(Project2Context context)
        {
            _context = context;
        }
        public async Task<TicketCreateResponse> CreateTicketAsync(TicketCreateRequest newTicket, int reporterId)
        {
            var map = MapperConfig.InitializeAutomapper();
            var ticket = map.Map<Ticket>(newTicket);
            ticket.ReporterId = reporterId;
            ticket.CreatedAt = DateTime.Now;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            var response = new TicketCreateResponse { Message = "You succsessfully posted a new ticket!" };
            return response;
        }

        public async Task<TicketEditResponse> EditTicketAsync(TicketEditRequest editTicket, int id, int reporterId)
        {
            var dbTicket = await _context.Tickets.FindAsync(id);
            if (dbTicket != null)
            {
                var map = MapperConfig.InitializeAutomapper();
                var ticket = map.Map(editTicket, dbTicket);
                dbTicket.Id = id;
                dbTicket.UpdatedAt = DateTime.Now;
                if (dbTicket.ReporterId == reporterId)
                {
                    _context.Tickets.Update(ticket);
                    await _context.SaveChangesAsync();
                    var succesResponse = new TicketEditResponse { Message = "You succesfully edited this ticket!" };
                    return succesResponse;
                }
                var failResponse = new TicketEditResponse { Message = "You did not edit this ticket! You are not the owner of this ticket!" };
                return failResponse;
            }
            var response = new TicketEditResponse { Message = "You did not edit this ticket! This ticket doesnt exist!" };
            return response;
        }
        public async Task<TicketStatusResponse> ChangeTicketsStatus(TicketStatusRequest ticketStatus, int reporterId, int id)
        {
            var dbTicket = await _context.Tickets.FindAsync(id);
            var dbUser = await _context.Users.FindAsync(id);
            if (dbTicket != null && dbUser != null && dbTicket.ReporterId == reporterId)
            {
                if (Enum.IsDefined(typeof(TicketStatus), ticketStatus.Status))
                {
                    var map = MapperConfig.InitializeAutomapper();
                    var ticket = map.Map(ticketStatus, dbTicket);
                    switch ((TicketStatus)ticketStatus.Status)
                    {
                        case TicketStatus.ToDO:
                            if (dbUser.Role == "Developer")
                            {
                                dbTicket.Status = TicketStatus.ApprovedByDev;
                            }
                            break;
                        case TicketStatus.ApprovedByDev:
                            if (dbUser.Role == "Developer")
                            {
                                dbTicket.Status = TicketStatus.Construction;
                            }
                            else if (dbUser.Role == "Manager")
                            {
                                dbTicket.Status = TicketStatus.ToDO;
                            }
                            break;
                        case TicketStatus.Construction:
                            if (dbUser.Role == "Developer")
                            {
                                dbTicket.Status = TicketStatus.TestingByDev;
                            }
                            else if (dbUser.Role == "Manager")
                            {
                                dbTicket.Status = TicketStatus.ApprovedByDev;
                            }
                            break;
                        case TicketStatus.TestingByDev:
                            if (dbUser.Role == "Tester")
                            {
                                dbTicket.Status = TicketStatus.TestingByTester;
                            }
                            else if (dbUser.Role == "Manager")
                            {
                                dbTicket.Status = TicketStatus.Construction;
                            }
                            break;
                        case TicketStatus.TestingByTester:
                            if (dbUser.Role == "Tester")
                            {
                                dbTicket.Status = TicketStatus.Closed;
                            }
                            else if (dbUser.Role == "Manager")
                            {
                                dbTicket.Status = TicketStatus.Construction;
                            }
                            break;
                        case TicketStatus.Closed:
                            if (dbUser.Role == "Manager")
                            {
                                dbTicket.Status = TicketStatus.TestingByTester;
                            }
                            break;
                        default:
                            
                            break;
                    }
                    _context.Tickets.Update(ticket);
                    await _context.SaveChangesAsync();
                    var succesResponse = new TicketStatusResponse { Message = $"The ticket's status is : {dbTicket.Status}" };
                    return succesResponse;
                }
                  
            }
            var failResponse = new TicketStatusResponse { Message = "FAIL" };
            return failResponse;
        }
    }
}