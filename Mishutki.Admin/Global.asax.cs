using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mishutki.Admin.Infrastructure;
using Mishutki.Admin.Models;
using System.Data.Entity;

namespace Mishutki.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            ControllerBuilder.Current.DefaultNamespaces.Add("Mishutki.Admin.Controllers");
        }
    }
}
