using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class RequestLoger
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public string Controller { get; set; }
        public string Parameter { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
