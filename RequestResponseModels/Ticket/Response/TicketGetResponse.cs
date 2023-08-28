using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Ticket.Response
{
    public class TicketGetResponse
    {

        public string Title { get; } = null!;

        public string Body { get; } = null!;

        public string Type { get; } = null!;

        public string Priority { get; } = null!;

        public string Component { get; } = null!;

        public int ReporterId { get; }

        public DateTime CreatedAt { get; }

        public DateTime? UpdatedAt { get; }

        public byte[]? Attachements { get;}

        public User Reporter { get; } = null!;

        public ICollection<Comment> Comments { get; } = new List<Comment>();

        public ICollection<History> Histories { get; } = new List<History>();

        public Watcher? Watchers { get; } 
    }
}
