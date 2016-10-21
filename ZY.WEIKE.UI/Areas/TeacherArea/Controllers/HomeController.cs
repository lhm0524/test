using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Areas.TeacherArea.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /TeacherArea/Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}
