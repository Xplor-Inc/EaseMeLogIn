using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EaseLogMeIn.WebApp.Controllers
{
   // [Authorize]
    public class DesktopAppLoginHistoryController : BaseController
    {
        readonly IEmployeeManager employeeManager;
        readonly IDesktopAppLoginHistoryManager loginHistoryManager;
        private DesktopAppLoginHistoryModel model = null;
        private DesktopAppLoginHistoryModel GetModel()
        {
            return new DesktopAppLoginHistoryModel
            {
                Users = employeeManager.GetMapping(),
                Paging = new SearchAccessLog { FromDate = DateTime.Now.IST().AddDays(-DateTime.Now.Day + 1), ToDate = DateTime.Now.IST() }
            };
        }
        public DesktopAppLoginHistoryController(IDesktopAppLoginHistoryManager lhm, IEmployeeManager _employee,  IExceptionLoger loger) : base(loger)
        {
            employeeManager = _employee;
            loginHistoryManager = lhm;
        }
        [HttpGet]
        public ActionResult Index()
        {
            model = GetModel();
            model.Logs = loginHistoryManager.Search(model.Paging, out int Count);
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
                model.Logs = loginHistoryManager.Search(search.Paging, out int Count);
                model.Paging = search.Paging;
                model.Paging.Count = Count;
            }
            return View(model);
        }
    }
}