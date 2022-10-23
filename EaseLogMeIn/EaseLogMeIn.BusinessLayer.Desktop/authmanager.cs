using EaseMeLogIn.BusinessLayer.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer.Desktop
{
    public static class AuthManager
    {
        public static bool IsValidEmployee(string UserId)
        {
            return APIRequestHandler.IsValidEmployee(UserId);
        }
        public static bool IssueToken()
        {
            return APIRequestHandler.IssueToken();
        }
    }
}
