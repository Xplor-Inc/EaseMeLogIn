using EaseLogMeIn.BusinessLayer;
using EaseLogMeIn.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace EaseLogMeIn.WebApp
{
    public class Resolver
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IEmployeeContext, EmployeeContext>();
            container.RegisterType<IEmployeeManager, EmployeeManager>();

            container.RegisterType<IWebUsersContext, WebUsersContext>();
            container.RegisterType<IWebUsersManager, WebUsersManager>();

            container.RegisterType<IWebsiteContext, WebsiteContext>();
            container.RegisterType<IWebsiteManager, WebsiteManager>();

            container.RegisterType<IWebsiteUserMappingContext, WebsiteUserMappingContext>();
            container.RegisterType<IWebsiteUserMappingManager, WebsiteUserMappingManager>();

            container.RegisterType<IWebsiteAccessManager, WebsiteAccessManager>();
            container.RegisterType<IWebsiteAccessContext, WebsiteAccessContext>();

            container.RegisterType<IExceptionContext, ExceptionContext>();
            container.RegisterType<IExceptionLoger, ExceptionLoger>();

            container.RegisterType<IUserActionDataContext, UserActionDataContext>();
            container.RegisterType<IUserActionDataManager,UserActionDataManage>();

            container.RegisterType<IWebsiteAccessContext, WebsiteAccessContext>();
            container.RegisterType<IWebsiteAccessManager, WebsiteAccessManager>();

            container.RegisterType<IDesktopAppLoginHistoryContext, DesktopAppLoginHistoryContext>();
            container.RegisterType<IDesktopAppLoginHistoryManager, DesktopAppLoginHistoryManager>();
        }

    }
}