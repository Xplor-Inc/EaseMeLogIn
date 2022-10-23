using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseMeLogIn.BusinessLayer.Desktop
{
    public static class UserStaticData
    {
        public static string IPAddress { get; set; }
        public static Employee Employee { get; set; }
        public static List<Website> Websites { get; set; }
        public static Website CurrentWeb { get; set; }
        public static int LoginId { get; set; }
    }
}
