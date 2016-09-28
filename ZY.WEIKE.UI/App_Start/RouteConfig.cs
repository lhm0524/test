using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZY.WEIKE.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default4",
                url: "{controller}/{action}/{username}-{pwd}",
                defaults: new { controller = "User", action = "Index"}
            );
            routes.MapRoute(
                name: "Default3",
                url: "{controller}/{action}/page/{type}/{pagesize}",
                defaults: new { controller = "CategoryList", action = "Index", pagesize = 100, type = 100 }
            );
            routes.MapRoute(
                name: "Default2",
                url: "{controller}/{action}/page/{type}-{pageindex}-{pagesize}",
                defaults: new { controller = "CategoryList", action = "Index", pageindex = 100, pagesize = 100, type = 100 }
            );
            //routes.MapRoute(
            //    name: "Default1",
            //    url: "{controller}/{action}/{cid}/{did}/{name}",
            //    defaults: new { controller = "CategoryList", action = "Index", cid = UrlParameter.Optional, did = UrlParameter.Optional, name = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "Default7",
                url: "{controller}/{action}/{id}/{name}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional },
                namespaces: new string[] { "ZY.WEIKE.UI.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{*id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ZY.WEIKE.UI.Controllers" }
            );
        }
    }
}