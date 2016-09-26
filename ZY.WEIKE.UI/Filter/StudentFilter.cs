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
                //浏览器请求控制器的方法是必须要拿到一个actionresult的 因此 如果使用response.write()调整 则没有返回一个actionresult 因此会继续往下执行 直到拿到actionreult
                filterContext.Result = new RedirectResult("/User/Login");//new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary(new { controller = "User", action = "Login" }));
                //确认了actionresult
            }
        }
    }
}