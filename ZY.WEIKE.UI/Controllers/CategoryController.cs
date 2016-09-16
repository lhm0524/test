using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        private BLL.ColleageBLL colleagebll;
        private BLL.DepartmentBLL departmentbll;
        public ActionResult Index()
        {
            //return Content(id.ToString() + "\r\n" + name);
            colleagebll = new BLL.ColleageBLL();
            ViewData.Add("initdata", colleagebll.GetTopList(4));
            return View();
        }

        [OutputCache(Duration = 1 * 1000 * 60 * 60)]
        public ActionResult LoadDepartmentByParentID(int id)
        {
            departmentbll = new BLL.DepartmentBLL();
            return Json(departmentbll.LoadByParentID(id), JsonRequestBehavior.AllowGet);
        }
    }
}
