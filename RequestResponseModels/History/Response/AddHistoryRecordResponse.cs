using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.History.Response
{
    public class AddHistoryRecordResponse
    {
       public string Body { get; set; }
       public DateTime CreatedAt { get; set; }
    }
}
