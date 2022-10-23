using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
  public  class Website: BaseProperties
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string URL { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserIdTextId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordTextId { get; set; }
        [Required]
        public ConfigurationType ConfigurationType { get; set; }

        public bool IsBlocked { get; set; }
        public string Salt { get; set; }
        public string ButtonId { get; set; }
        public string Script { get; set; }
    }
    public enum ConfigurationType
    {
        ById = 1,
        ByName
    }
}
