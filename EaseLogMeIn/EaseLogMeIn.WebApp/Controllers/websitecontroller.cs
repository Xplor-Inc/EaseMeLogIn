using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EaseLogMeIn.WebApp.Controllers
{
   // [Authorize]
    public class WebsiteController : BaseController
    {
        readonly IWebsiteManager websiteManager;
        readonly IEmployeeManager employeeManager;
        readonly IWebsiteUserMappingManager mappingManager;
        private WebsiteModel model = new WebsiteModel { Paging = new Paging() };
        private WebsiteModel GetModel(Paging paging, string Search)
        {
            model.Users = employeeManager.GetMapping();
            model.Websites = websiteManager.Get(Search, paging.PageSize, paging.PageIndex, out int Count);
            model.Paging = paging;
            model.Paging.Count = Count;
            return model;
        }
        public WebsiteController(IWebsiteManager _websiteManager, IEmployeeManager _employee, IWebsiteUserMappingManager wum, IExceptionLoger loger) : base(loger)
        {
            mappingManager = wum;
               employeeManager = _employee;
            websiteManager = _websiteManager;
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Result = TempData["Result"];
            model = GetModel(new Paging(), "");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(Website web)
        {
            if (!ModelState.IsValid)
            {
                model = GetModel(new Paging(), "");
                model.Website = web;
                return View(model);
            }
            else
            {
                int result = websiteManager.Add(web, out Guid WebsiteId);
                if (result > 0)
                {
                    TryUpdateModel(model);
                    if (model.Users != null && model.Users.Count > 0)
                    {
                        List<WebsiteUserMapping> data = new List<WebsiteUserMapping>();
                        List<MappingData> s = model.Users.FindAll(q => q.IsSelected);
                        for (int i = 0; i < s.Count; i++)
                        {
                            data.Add(new WebsiteUserMapping
                            {
                                UserId = s[i].UniqueId,
                                WebsiteId = WebsiteId
                            });
                        }
                        if (data.Count > 0)
                        {
                            mappingManager.Add(data, MappingType.Website);
                        }
                    }
                    ViewBag.Result = "Website added successfully";
                }
                else
                {
                    ViewBag.Result = "Website already exist with same User Id";
                    model.Website = web;
                }
            }
            model = GetModel(new Paging(), "");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(Website web)
        {
            if (!ModelState.IsValid)
            {
                model = GetModel(new Paging(), "");
                model.Website = web;
                return View(model);
            }
            else
            {
                int result = websiteManager.Update(web);
                if (result > 0)
                {
                    TempData["Result"] = "Website updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Result = "Website already exist with same User Id";
                    ViewBag.State = true;
                    model = GetModel(new Paging(), "");
                    model.Website = web;
                }
            }
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Search(WebsiteModel web, string WebsiteName)
        {
            ViewBag.WebsiteName = WebsiteName;
            if (web.Paging == null) { web.Paging = new Paging(); }
            model = GetModel(web.Paging, WebsiteName);
            model.Paging = web.Paging;
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public JsonResult Delete(Guid UniqueId)
        {
            int result = websiteManager.Delete(UniqueId);
            return Json(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public JsonResult Block(Guid UniqueId)
        {
            int result = websiteManager.Block(UniqueId);
            return Json(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public JsonResult UnBlock(Guid UniqueId)
        {
            int result = websiteManager.UnBlock(UniqueId);
            return Json(result);
        }

    }
}