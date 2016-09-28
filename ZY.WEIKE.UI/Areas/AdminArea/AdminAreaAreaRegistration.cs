using System.Web.Mvc;

namespace ZY.WEIKE.UI.Areas.AdminArea
{
    public class AdminAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminArea_default",
                "Admin/{controller}/{action}/{*id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
