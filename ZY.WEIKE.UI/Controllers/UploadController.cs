using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Demo()
        {
            string path = Server.MapPath("~/TempFiles/");
            HttpPostedFileBase file = Request.Files["files"];
            
            file.SaveAs(path + file.FileName);
            return Content("/TempFiles/" + file.FileName);
        }

    }
}
