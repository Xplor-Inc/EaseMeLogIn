using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EaseLogMeIn.WebApp.Areas.APIController.Controllers
{
    [Authorize]
    public class WebController : ApiController
    {
        readonly IWebsiteManager webAPI;
        readonly IUserActionDataManager actionManager;
        readonly IWebsiteAccessManager WebAccessManager;
        readonly IDesktopAppLoginHistoryManager desktopAppLogin;
        public WebController(IWebsiteManager _Web, IWebsiteAccessManager WebAccess, IUserActionDataManager action, IDesktopAppLoginHistoryManager desktopApp)
        {
            WebAccessManager = WebAccess;
            webAPI = _Web;
            actionManager = action;
            desktopAppLogin = desktopApp;
        }

        [HttpPost]
        [Route("api/web/IsValidWebsite")]
        public HttpResponseMessage IsValidWebsite(WebsiteAccessLog website)
        {
            int id = website.Id;
            Website web = webAPI.IsValidWebsite(website.WebsiteId, website.UserId);
            website.Status = web != null;
            website.AccessTime =  DateTime.Now.IST();
            WebAccessManager.Add(website);
            desktopAppLogin.UpdateVisitCount(id);
            return Request.CreateResponse(HttpStatusCode.OK, web);
        }

        [HttpGet]
        public HttpResponseMessage GetWebsites(string UserId)
        {
            List<Website> websites = webAPI.Get(UserId);
            return Request.CreateResponse(HttpStatusCode.OK, websites);
        }

        public string GetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        [HttpPost]
        [Route("api/web/UpdateClipboardData")]
        public HttpResponseMessage UpdateClipboardData(UserActionData data)
        {
            actionManager.Add(data);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

    }
}
