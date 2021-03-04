using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mishutki.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "",
                url: "contacts",
                defaults: new { controller = "Home", action = "Contacts" }
            );

            routes.MapRoute(
                name: "",
                url: "about",
                defaults: new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                name: "",
                url: "tag/{tagname}",
                defaults: new { controller = "Tag", action = "Index", page = 1 }
            );

            routes.MapRoute(
                name: "",
                url: "tag/{tagname}/page-{page}",
                defaults: new { controller = "Tag", action = "Index" }
            );

            routes.MapRoute(
                name: "",
                url: "posts/{category}/sort-{sort}",
                defaults: new { controller = "Home", action = "Index", page = 1 }
            );

            routes.MapRoute(
               name: "",
               url: "posts/{category}/sort-{sort}/page-{page}",
               defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "",
                url: "posts/sort-{sort}/page-{page}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "",
                url: "posts/{category}/page-{page}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "",
                url: "posts/page-{page}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "",
                url: "posts/sort-{sort}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Category",
                url: "posts/{category}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
