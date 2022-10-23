using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
    public class MappingData
    {
        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }

    public class WebsiteUserMapping 
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WebsiteId { get; set; }
    }
    public enum MappingType
    {
        User = 1,
        Website
    }
}
