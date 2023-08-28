﻿using InternshipProject_2.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using RequestResponseModels.Ticket.Request;
using RequestResponseModels.Ticket.Response;
using System.Net;
using InternshipProject_2.Models;
using AutoMapper.Configuration.Conventions;

namespace InternshipProject_2.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager _ticketAcces;

        public TicketController(ITicketManager ticket)
        {
            _ticketAcces = ticket;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> NewTicket([FromBody] TicketCreateRequest ticket)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
            try
            {
                if (userId?.Value != null)
                {
                    int reporterId = int.Parse(userId.Value);
                    await _ticketAcces.CreateTicketAsync(ticket, reporterId);
                    return Ok();
                }
                else return BadRequest("You need to be logged in");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("edit/{ticketId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EditTicket([FromBody] TicketEditRequest ticket, int ticketId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
            try
            {
                if (userId?.Value != null)
                {
                    int reporterId = int.Parse(userId.Value);
                    if (reporterId != 0)
                    {
                        await _ticketAcces.EditTicketAsync(ticket, ticketId, reporterId);
                        return Ok();
                    }
                    else return BadRequest("You did not post this!");
                }
                else return BadRequest("You are not logged in!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("get")]
        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            try 
            {
                return await _ticketAcces.GetTicketsAsync();
            }
            catch(Exception ex)
            {
                return (IEnumerable<Ticket>)BadRequest(ex.Message);
            }

        }
    }
}
