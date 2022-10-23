using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EaseLogMeIn.Models
{
    public  class DesktopAppLoginHistory
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public string MACAddress { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string TimeSpend { get; set; }
        public int VisitedWebCounts { get; set; }
        public string OSName { get; set; }
        public string OSVersion { get; set; }
        public string OSArchitecture { get; set; }
        public string SystemArchitecture { get; set; }
        public string MachineName { get; set; } 
        public bool IsSuccessed { get; set; }
    }
}
