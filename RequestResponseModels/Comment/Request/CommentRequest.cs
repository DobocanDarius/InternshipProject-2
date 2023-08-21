using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Comment.Request
{
    public class CommentRequest
    {
        public string Body;
        public int UserId;
        public int TicketId;
        public DateTime CreatedAt;
    }
}
