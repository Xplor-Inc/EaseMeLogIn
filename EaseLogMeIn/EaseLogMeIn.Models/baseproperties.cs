using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
   public class BaseProperties
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now.IST();
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public Guid UniqueId { get; set; }
    }
}
