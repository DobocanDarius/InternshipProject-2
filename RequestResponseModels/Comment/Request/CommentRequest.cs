using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Comment.Request
{
    public class CommentRequest
    {
        public string Body { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}