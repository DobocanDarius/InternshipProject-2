using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequestResponseModels.History.Enum;

namespace RequestResponseModels.History.Request
{
    public class AddHistoryRecordRequest
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public HistoryEventType EventType { get; set; }
    }
}
