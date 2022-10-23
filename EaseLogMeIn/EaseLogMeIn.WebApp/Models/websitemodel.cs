using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EaseLogMeIn.Models
{
    public class WebsiteModel
    {
        public List<Website> Websites { get; set; }
        public List<MappingData> Users { get; set; }
        public Website Website { get; set; }
        public Paging Paging { get; set; }
    }
}