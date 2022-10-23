using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EaseLogMeIn.Models
{
    public class WebHistoryModel
    {
        public SearchAccessLog Paging { get; set; }
        public List<WebHistory> Logs { get; set; }
        public List<MappingData> Users { get; set; }
        public List<MappingData> Websites { get; set; }
    }
}