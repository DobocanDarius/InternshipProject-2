using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestResponseModels.History.Response
{
    public class GetHistoryResponse
    {
        public List<AddHistoryRecordResponse> HistoryRecords { get; set; }
    }
}
