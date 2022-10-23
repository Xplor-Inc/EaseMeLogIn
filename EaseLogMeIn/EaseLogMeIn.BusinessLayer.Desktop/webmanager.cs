using EaseLogMeIn.Models;
using EaseMeLogIn.BusinessLayer.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer.Desktop
{
    public static class WebManager
    {
        public static void GetWebsites()
        {
            APIRequestHandler.GetWebsites();
        }
        public static bool IsValidWebsite(Guid WebsiteId, Guid UserId)
        {
            return APIRequestHandler.IsValidWebsite(WebsiteId, UserId);
        }
        public static void UpdateClipboardData(string clipData, ActionType actionType)
        {
            UserActionData data = new UserActionData
            {
                ActionType = actionType,
                CreateDate = DateTime.Now.IST(),
                Data = clipData,
                UserId = UserStaticData.Employee.UniqueId,
                WebsiteId = UserStaticData.CurrentWeb == null ? Guid.Empty : UserStaticData.CurrentWeb.UniqueId
            };
            APIRequestHandler.UpdateClipboardData(data);
        }

        public static void UpdateLogOut()
        {
            APIRequestHandler.UpdateLogOut();
        }

    }
}
