using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Web.Mvc;

namespace EaseLogMeIn.WebApp.Controllers
{
   // [Authorize]
    public class WebAccessHistoryController : BaseController
    {
        readonly IWebsiteManager websiteManager;
        readonly IEmployeeManager employeeManager;
        readonly IWebsiteAccessManager accessManager;
        private WebHistoryModel model = null;
        private WebHistoryModel GetModel()
        {
            return new WebHistoryModel
            {
                Users = employeeManager.GetMapping(),
                Websites = websiteManager.GetMapping(),
                Paging = new SearchAccessLog { FromDate = DateTime.Now.IST().AddDays(-DateTime.Now.Day + 1).Date, ToDate = DateTime.Now.IST().Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 1) }
            };
        }
        public WebAccessHistoryController(IWebsiteAccessManager wa, IEmployeeManager _employee, IWebsiteManager _website, IExceptionLoger loger) : base(loger)
        {
            employeeManager = _employee;
            websiteManager = _website;
            accessManager = wa;
        }
        [HttpGet]
        public ActionResult Index(DateTime? FromDate, DateTime? ToDate, Guid? UserId)
        {
            model = GetModel();
            if (FromDate != null)
            {
                model.Paging.FromDate = FromDate.Value;
            }
            if (ToDate != null)
            {
                model.Paging.ToDate = ToDate.Value;
            }
            if (UserId != null)
            {
                model.Paging.UserId = UserId.Value;
            }
            model.Logs = accessManager.Search(model.Paging, out int Count);
            model.Paging.Count = Count;
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(WebHistoryModel search)
        {
            if (search != null)
            {
                model = GetModel();
                search.Paging.ToDate = search.Paging.ToDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 1);
                model.Logs = accessManager.Search(search.Paging, out int Count);
                model.Paging = search.Paging;
                model.Paging.Count = Count;
            }
            return View(model);
        }
    }
}