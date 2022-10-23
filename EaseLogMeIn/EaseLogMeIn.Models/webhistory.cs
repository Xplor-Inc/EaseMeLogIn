using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
    public class WebHistory
    {
        public string Name { get; set; }
        public string WebName { get; set; }
        public string WebUser { get; set; }
        public string IPAddress { get; set; }
        public DateTime AccessTime { get; set; }
    }
    public  class SearchAccessLog : Paging
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Guid UserId { get; set; }
        public Guid WebsiteId { get; set; }
    }
}
