using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Web.Mvc;

namespace EaseLogMeIn.WebApp.Controllers
{
  //  [Authorize]
    public class UserActionDataController : BaseController
    {
        readonly IEmployeeManager employeeManager;
        readonly IUserActionDataManager userActionData;
        private UserActionDataModel model = null;
        private UserActionDataModel GetModel()
        {
            return new UserActionDataModel
            {
                Users = employeeManager.GetMapping(),
                Paging = new UserActionSearch { FromDate = DateTime.Now.IST().AddDays(-DateTime.Now.Day + 1), ToDate = DateTime.Now.IST() }
            };
        }
        public UserActionDataController(IUserActionDataManager uad, IEmployeeManager _employee, IExceptionLoger loger) : base(loger)
        {
            employeeManager = _employee;
            userActionData = uad;
        }
        [HttpGet]
        public ActionResult Index()
        {
            model = GetModel();
            model.Logs = userActionData.Search(model.Paging, out int Count);
            model.Paging.Count = Count;
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(UserActionDataModel search)
        {
            if (search != null)
            {
                model = GetModel();
                search.Paging.ToDate = search.Paging.ToDate.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 1);
                model.Logs = userActionData.Search(search.Paging, out int Count);
                model.Paging = search.Paging;
                model.Paging.Count = Count;
            }
            return View(model);
        }
    }
}