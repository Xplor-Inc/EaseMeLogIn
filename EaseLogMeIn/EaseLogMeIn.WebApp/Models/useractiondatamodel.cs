using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EaseLogMeIn.Models
{
    public class UserActionDataModel
    {
        public UserActionSearch Paging { get; set; }
        public List<UserData> Logs { get; set; }
        public List<MappingData> Users { get; set; }
    }
 
    
}