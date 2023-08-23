using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Ticket.Response
{
    public class TicketResponse
    {
        public TicketResponse(int id, string title, string body, string type, string priority, string component, int reporterId, DateTime createdAt, DateTime? updatedAt, byte attachements)
        {
            Id = id;
            Title = title;
            Body = body;
            Type = type;
            Priority = priority;
            Component = component;
            ReporterId = reporterId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Attachements = attachements;
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public string Component { get; set; } = null!;

        public int ReporterId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public byte? Attachements { get; set; }

    }
}

