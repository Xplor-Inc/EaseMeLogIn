using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
    public class UserActionData
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        // [NotMapped]
        public Guid WebsiteId { get; set; }
        public string Data { get; set; }
        public DateTime CreateDate { get; set; }
        public ActionType ActionType { get; set; }
        //  [NotMapped]
        public bool IsPasted { get; set; }
    }
    public enum ActionType
    {
        Both = 0,
        Clipboard = 1,
        Keystroke = 2
    }
    public class UserActionSearch : SearchAccessLog
    {
        public ActionType ActionType { get; set; }
        public string KeyWord { get; set; }
    }
    public class UserData
    {
        public string EmployeeName { get; set; }
        public string WebName { get; set; }
        public string WebUser { get; set; }
        public string Data { get; set; }
        public DateTime AccessTime { get; set; }
        public ActionType Action { get; set; }
        public bool IsPasted { get; set; }
    }
}
