using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace ZY.WEIKE.UI.Filter
{
    public class StudentFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string role = filterContext.HttpContext.Session["role"] as string;
            if (role == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary(new { controller = "User", action = "Login" }));
            }
        }
    }
}