using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Assignee.Request
{
    public class AssignUserRequest
    {
       public int UserId { get; set; }
       public int TicketId { get; set; }
    }
}
