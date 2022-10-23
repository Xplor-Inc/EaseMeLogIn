using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.Models
{
    public static class ReadOnlyKeys
    {
        static readonly NameValueCollection keys = ConfigurationManager.AppSettings;
        public static int PageSize
        {
            get
            {
                string size = keys["PageSize"];
                if (int.TryParse(size, out int PageSize))
                { return PageSize; }
                else { return 10; }
            }
        }
        public static string LogFolder => keys["LogFolder"];
        public static string DefaultPassword => keys["DefaultPassword"];
        public static string LogSource => keys["LogSource"];
        public static string APIBaseUrl => keys["APIBaseURL"];
        public static string IsValidEmployee => keys["IsValidEmployee"];
        public static string GetWebsiteList => keys["GetWebsiteList"];
        public static string GetIP => keys["GetIP"];
        public static string IsValidWebsite => keys["IsValidWebsite"];
        //public static string AddWebAccessHistory => keys["AddWebAccessHistory"];
        public static string AddUserActionData => keys["AddUserActionData"];
        public static string BlockWebsite => keys["BlockWebsite"];
        public static string AddLoginHistory => keys["AddLoginHistory"];
        public static string UpdateLoginHistory => keys["UpdateLoginHistory"];
        public static string APIUserId => keys["APIUserId"];
        public static string APIUserPassword => keys["APIUserPassword"];
    }
}
