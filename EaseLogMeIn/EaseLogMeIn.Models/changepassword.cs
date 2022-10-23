using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class ChangePassword
    {
        [Required]
        public Guid UniqueId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Minimum 8 charactor required")]
        [MaxLength(16, ErrorMessage ="Maximum 16 charactor allowed")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
