using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EaseLogMeIn.WebApp.Controllers
{
  //  [Authorize]
    public class EmployeeController : BaseController
    {
        readonly IEmployeeManager employeeManager;
        readonly IWebsiteManager websiteManager;
        readonly IWebsiteUserMappingManager mappingManager;
        private EmployeeModel model = new EmployeeModel { Paging = new Paging() };
        private EmployeeModel GetModel(Paging paging, string Search)
        {
            model.Websites = websiteManager.GetMapping();
            model.Employees = employeeManager.Get(Search, paging.PageSize, paging.PageIndex, out int Count);
            model.Paging = paging;
            model.Paging.Count = Count;
            return model;
        }
        public EmployeeController(IEmployeeManager em, IWebsiteManager wm, IWebsiteUserMappingManager wum, IExceptionLoger loger):base(loger)
        {
            mappingManager = wum;
            websiteManager = wm;
            employeeManager = em;
        }
        public ActionResult Index()
        {
            ViewBag.Result = TempData["Result"];
            model = GetModel(new Paging(), "");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                model = GetModel(new Paging(), "");
                model.Employee = emp;
                return View(model);
            }
            else
            {
                int result = employeeManager.Add(emp,out Guid UserId);
                if (result > 0)
                {
                    TryUpdateModel(model);
                    if (model.Websites != null && model.Websites.Count > 0)
                    {
                        List<WebsiteUserMapping> data = new List<WebsiteUserMapping>();
                        List<MappingData> s = model.Websites.FindAll(q => q.IsSelected);
                        for (int i = 0; i < s.Count; i++)
                        {
                            data.Add(new WebsiteUserMapping
                            {
                                UserId = UserId,
                                WebsiteId = s[i].UniqueId
                            });
                        }
                        if (data.Count > 0)
                        {
                            mappingManager.Add(data, MappingType.User);
                        }
                    }
                    ViewBag.Result = "Employee added successfully";
                }
                else
                {
                    ViewBag.Result = "Employee already exist with same User Id";
                    model.Employee = emp;
                }
            }
            model = GetModel(new Paging(), "");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                model = GetModel(new Paging(), "");
                model.Employee = emp;
                return View(model);
            }
            else
            {
                int result = employeeManager.Update(emp);
                if (result > 0)
                {
                    TempData["Result"] = "Employee updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Result = "Employee already exist with same User Id";
                    ViewBag.State = true;
                    model = GetModel(new Paging(), "");
                    model.Employee = emp;
                    return View("Index", model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Search(EmployeeModel emp, string EmployeeName)
        {
            ViewBag.EmployeeName = EmployeeName;
            if (emp.Paging == null) { emp.Paging = new Paging(); }
            model = GetModel(emp.Paging, EmployeeName);
            model.Paging = emp.Paging;
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public JsonResult Delete(Guid UniqueId)
        {
            int result = employeeManager.Delete(UniqueId);
            return Json(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public JsonResult Block(Guid UniqueId)
        {
            int result = employeeManager.Block(UniqueId);
            return Json(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public JsonResult UnBlock(Guid UniqueId)
        {
            int result = employeeManager.UnBlock(UniqueId);
            return Json(result);
        }

    }

}