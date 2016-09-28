using System.Web.Mvc;

namespace ZY.WEIKE.UI.Areas.TeacherArea
{
    public class TeacherAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TeacherArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TeacherArea_default",
                "Teacher/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
