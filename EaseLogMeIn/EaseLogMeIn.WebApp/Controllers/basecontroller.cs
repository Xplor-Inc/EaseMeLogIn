using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.Models;
using System;
using System.Web.Mvc;

namespace EaseLogMeIn.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly IExceptionLoger exceptionLoger;
        public BaseController(IExceptionLoger loger)
        {
            exceptionLoger = loger;
        }
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                RequestLoger loger = new RequestLoger
                {
                    MethodName = filterContext.Controller?.ControllerContext?.RouteData?.Values["action"].ToString(),
                    Controller = filterContext.Controller?.ControllerContext?.RouteData?.Values["controller"].ToString(),
                    Date = DateTime.Now.IST(),
                    Parameter = filterContext.Controller?.ControllerContext?.RouteData?.Values["id"]?.ToString(),
                    UserId = AuthorizedUser.UserId
                };
                exceptionLoger.AddRequest(loger);
            }
            catch (Exception ) { }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            CustomException custom = new CustomException
            {
                MethodName = filterContext.Controller?.ControllerContext?.RouteData?.Values["action"].ToString(),
                Controller = filterContext.Controller?.ControllerContext?.RouteData?.Values["controller"].ToString(),
                Parameter = filterContext.Controller?.ControllerContext?.RouteData?.Values["id"]?.ToString()
            };
            exceptionLoger.AddException(filterContext.Exception, custom);
            ViewBag.Error = filterContext.Exception.Message;
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
        }
        [HttpGet]
        public ActionResult NotFound()
        {
            var statusCode = (int)System.Net.HttpStatusCode.NotFound;
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            HttpContext.Response.StatusCode = statusCode;
            HttpContext.Response.TrySkipIisCustomErrors = true;
            return View("Error");
        }
    }
}