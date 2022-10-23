using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class WebsiteAccessLog
    {
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid WebsiteId { get; set; }
        [Required]
        public DateTime AccessTime { get; set; }
        public bool Status { get; set; }
        public string IPAddress { get; set; }
    }
}
