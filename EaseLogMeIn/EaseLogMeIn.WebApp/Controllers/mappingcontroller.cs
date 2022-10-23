using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EaseLogMeIn.WebApp.Controllers
{
    //[Authorize]
    public class MappingController : BaseController
    {
        readonly IWebsiteManager websiteManager;
        readonly IEmployeeManager employeeManager;
        readonly IWebsiteUserMappingManager mappingManager;
        MappingModel model = null;
        private MappingModel GetModel()
        {
            return new MappingModel
            {
                Users = employeeManager.GetMapping(),
                Websites = websiteManager.GetMapping()
            };
        }
        public MappingController(IWebsiteUserMappingManager _Manager, IEmployeeManager _employee, IWebsiteManager _website, IExceptionLoger loger) : base(loger)
        {
            employeeManager = _employee;
            websiteManager = _website;
            mappingManager = _Manager;
        }
        public ActionResult Index()
        {
            model = GetModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MappingModel mapping)
        {
            List<WebsiteUserMapping> websiteUserMappings = new List<WebsiteUserMapping>();
            List<MappingData> s = new List<MappingData>();
            List<WebsiteUserMapping> data = new List<WebsiteUserMapping>();
            if (mapping.MappingType == MappingType.User)
            {
                s = mapping.Websites.FindAll(q => q.IsSelected);
                for (int i = 0; i < s.Count; i++)
                {
                    data.Add(new WebsiteUserMapping
                    {
                        UserId = mapping.UserId,
                        WebsiteId = s[i].UniqueId
                    });
                }
            }
            if (mapping.MappingType == MappingType.Website)
            {
                s = mapping.Users.FindAll(q => q.IsSelected);
                for (int i = 0; i < s.Count; i++)
                {
                    data.Add(new WebsiteUserMapping
                    {
                        UserId = s[i].UniqueId,
                        WebsiteId = mapping.WebsiteId
                    });
                }
            }
            if (s.Count > 0)
            {
                int result = mappingManager.Add(data, mapping.MappingType);
                ViewBag.Result = result > 0 ? "Website mapped successfully" : "Unable to map!!";
            }
            model = GetModel();
            return View(model);
        }
        public JsonResult GetByUser(Guid Id)
        {
            List<WebsiteUserMapping> users = mappingManager.GetByUser(Id);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByWebsite(Guid Id)
        {
            List<WebsiteUserMapping> users = mappingManager.GetByWebsite(Id);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}