using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.Attachement.Response
{
    public class AttachementResponse
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
