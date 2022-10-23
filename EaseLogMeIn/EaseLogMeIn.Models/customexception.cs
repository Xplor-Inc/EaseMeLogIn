using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class CustomException
    {
        public int Id { get; set; }
        public string MethodName { get; set; }
        public string Controller { get; set; }
        public string Parameter { get; set; }
        public string Error { get; set; }
        public string StackTrace { get; set; }
        public Guid? UserId { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }
    }
}
