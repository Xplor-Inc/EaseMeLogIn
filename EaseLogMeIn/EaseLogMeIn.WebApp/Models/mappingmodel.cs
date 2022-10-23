using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EaseLogMeIn.Models
{
   
    public class MappingModel
    {
        public MappingType MappingType { get; set; }
        public Guid UserId { get; set; }
        public Guid WebsiteId { get; set; }
        public List<MappingData> Users { get; set; }
        public List<MappingData> Websites { get; set; }
    }
}