using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
    public class Users:BaseProperties
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }
}
