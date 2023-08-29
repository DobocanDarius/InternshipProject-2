using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Ticket.Response
{
    public class Comment
    {
        public string Body { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
