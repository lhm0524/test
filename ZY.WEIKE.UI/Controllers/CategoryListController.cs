using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Controllers
{
    public class CategoryListController : Controller
    {
        //
        // GET: /CategoryList/
        private BLL.DepartmentBLL departmentbll;
        private BLL.WeiKeBLL weikebll;
        private const int PageSize = 2;
        public ActionResult Index(string name, int Id)
        {
            departmentbll = new BLL.DepartmentBLL();
            IEnumerable<MODAL.DepartmentModel> list = departmentbll.LoadByParentID(Id);
            ViewData.Add("cl_department", list);
            return View();
        }

        public ActionResult LoadWeiKeEntities(int Id)
        {
            weikebll = new BLL.WeiKeBLL();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", Id);
            IEnumerable<MODAL.WeiKeModel> list = weikebll.LoadEntities("typeid=@id", "createtime", dic, true, 2);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadPageEntities(int type, int pageindex, int pagesize)
        {
            weikebll = new BLL.WeiKeBLL();
            int totalCount;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", type);
            IEnumerable<MODAL.WeiKeModel> list = weikebll.LoadPageEntities(pageindex, pagesize, out totalCount, "typeid=@id", dic, "", true);
            return Json(new { count = totalCount, data = list}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPageCount(int type, int pagesize)
        {
            weikebll = new BLL.WeiKeBLL();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", type);
            int count = weikebll.GetAllPageCountForWhere("typeid=@id", dic) / pagesize + 1;
            return Content(count.ToString());
        }
        
    }
}
