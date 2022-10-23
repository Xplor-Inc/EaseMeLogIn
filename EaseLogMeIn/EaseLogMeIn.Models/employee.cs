using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class Employee: BaseProperties
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsNonADUser { get; set; }
        public string MACId { get; set; }
        //[NotMapped]
        //public string MobileNo { get; set; }
    }
}
