using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class AuthResponse
    {
        public bool Status { get; set; }
        public string Token { get; set; }
        public string IPAddress { get; set; }
    }
}
