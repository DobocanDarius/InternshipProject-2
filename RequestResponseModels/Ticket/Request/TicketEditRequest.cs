using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Ticket.Request
{
    public class TicketEditRequest
    {
        public string Title { get; set; } = null!;

        public string Body { get; set; } = null!;

        public string Priority { get; set; } = null!;
    }
}