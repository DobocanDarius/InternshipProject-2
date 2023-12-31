﻿using AutoMapper;
using InternshipProject_2.Models;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;

namespace InternshipProject_2.Helpers
{
    public class TicketStatusHelper
    {
        private readonly Project2Context _context;
        private readonly IMapper _mapper;

        public TicketStatusHelper()
        {
        }

        public TicketStatusHelper(Project2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TicketStatusResponse> HandleStatusChange(Ticket dbTicket, Models.User dbUser, TicketStatusRequest ticketStatus)
        {
            if (Enum.IsDefined(typeof(TicketStatus), ticketStatus.Status))
            { 
                var CurrentStatus = dbTicket.Status;
                var ticket = _mapper.Map(ticketStatus, dbTicket);
                switch ((TicketStatus)CurrentStatus)
                {
                    case TicketStatus.ToDO:
                        if (dbUser.Role == "developer")
                        {
                            dbTicket.Status = (int)TicketStatus.ApprovedByDev;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    case TicketStatus.ApprovedByDev:
                        if (dbUser.Role == "developer")
                        {
                            dbTicket.Status = (int)TicketStatus.Construction;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        else if (dbUser.Role == "manager")
                        {
                            dbTicket.Status = (int)TicketStatus.ToDO;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    case TicketStatus.Construction:
                        if (dbUser.Role == "developer")
                        {
                            dbTicket.Status = (int)TicketStatus.TestingByDev;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        else if (dbUser.Role == "Manager" && dbUser.Role != "developer")
                        {
                            dbTicket.Status = (int)TicketStatus.ApprovedByDev;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    case TicketStatus.TestingByDev:
                        if (dbUser.Role == "Tester")
                        {
                            dbTicket.Status = (int)TicketStatus.TestingByTester;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        else if (dbUser.Role == "Manager")
                        {
                            dbTicket.Status = (int)TicketStatus.Construction;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    case TicketStatus.TestingByTester:
                        if (dbUser.Role == "Tester")
                        {
                            dbTicket.Status = (int)TicketStatus.Closed;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        else if (dbUser.Role == "Manager")
                        {
                            dbTicket.Status = (int)TicketStatus.Construction;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
                        }
                        break;
                    case TicketStatus.Closed:
                        if (dbUser.Role == "Manager")
                        {
                            dbTicket.Status = (int)TicketStatus.TestingByTester;
                            _context.Tickets.Update(ticket);
                            await _context.SaveChangesAsync();
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
}
