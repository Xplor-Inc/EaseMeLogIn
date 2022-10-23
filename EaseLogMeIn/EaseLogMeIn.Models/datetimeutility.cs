using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace EaseLogMeIn.Models
{
    public static class DateTimeUtility
    {
        public static DateTime IST(this DateTime _)
        {
            return DateTime.Now.ToUniversalTime().AddMinutes(30).AddHours(5);
        }
    }

    public static class AuthorizedUser
    {
        public static Guid UserId
        {
            get
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket != null && !authTicket.Expired)
                    {
                        Guid.TryParse(authTicket.UserData, out Guid Userid);
                        return Userid;
                    }
                }
                return Guid.Empty;
            }
        }
    }
}
