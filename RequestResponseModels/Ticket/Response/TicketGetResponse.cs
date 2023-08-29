using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Ticket.Response
{
    public class TicketGetResponse
    {
        public string Title { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Priority { get; set; } = null!;

        public string Component { get; set; } = null!;

        public int ReporterId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public byte[]? Attachements { get; set; }

        public User Reporter { get; set; } = null!;

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<History>? Histories { get; set; }

        public ICollection<Watcher>? Watchers { get; set; } 
    }
}
