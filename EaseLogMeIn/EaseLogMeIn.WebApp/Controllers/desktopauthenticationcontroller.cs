using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EaseLogMeIn.WebApp.Areas.APIController.Controllers
{
  //  [Authorize]
    public class DesktopAuthenticationController : ApiController
    {
        readonly IEmployeeManager emp;
        IDesktopAppLoginHistoryManager loginHistoryManager;
        public DesktopAuthenticationController(IEmployeeManager _emp, IDesktopAppLoginHistoryManager lhm)
        {
            emp = _emp;
            loginHistoryManager = lhm;
        }

        [HttpGet]
        public HttpResponseMessage IsValidEmployee(string UserId)
        {
            Employee employee = emp.IsValidEmployee(UserId);
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }
        [HttpPost]
        [Route("api/AddLoginHistory")]
        public HttpResponseMessage AddLogin(DesktopAppLoginHistory history)
        {
            history.LoginTime = DateTime.Now.IST();
            int Id = loginHistoryManager.AddLogin(history);
            return Request.CreateResponse(HttpStatusCode.OK, Id);
        }
        [HttpPost]
        [Route("api/UpdateLoginHistory")]
        public HttpResponseMessage UpdateLogOut(DesktopAppLoginHistory history)
        {
            loginHistoryManager.UpdateLogOut(history);
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
        public HttpResponseMessage Put(string Name)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}
