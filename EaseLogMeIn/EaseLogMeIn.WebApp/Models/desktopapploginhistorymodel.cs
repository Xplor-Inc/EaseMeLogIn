using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class DesktopAppLoginHistoryModel
    {
        public SearchAccessLog Paging { get; set; }
        public List<DesktopAppLoginHistory> Logs { get; set; }
        public List<MappingData> Users { get; set; }
    }
}
