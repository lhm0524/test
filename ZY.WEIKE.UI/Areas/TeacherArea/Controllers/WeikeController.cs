using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Areas.TeacherArea.Controllers
{
    public class WeikeController : Controller
    {
        //
        // GET: /TeacherArea/Weike/
        public BLL.ColleageBLL colleageBLL { get; set; }
        public BLL.DepartmentBLL departmentBLL { get; set; }
        public BLL.WeiKeBLL weikeBLL { get; set; }
        public ActionResult Index()
        {
            colleageBLL = new BLL.ColleageBLL();
            departmentBLL = new BLL.DepartmentBLL();
            weikeBLL = new BLL.WeiKeBLL();
            IEnumerable<MODAL.ColleageModel> colleagemodel = colleageBLL.LoadEntites("", null, "", false);
            Dictionary<string, object> dic = new Dictionary<string,object>();
            dic.Add("@id", 7);
            int count = weikeBLL.GetAllPageCountForWhere("teacherId=@id", dic);
            ViewBag.Count = count;
            ViewData.Add("list_colleagemodel", colleagemodel);
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult LoadDepartment(int parentId)
        {
            departmentBLL = new BLL.DepartmentBLL();
            return Json(departmentBLL.LoadByParentID(parentId), JsonRequestBehavior.DenyGet);
        }

        public ActionResult LoadPageEntity(int pageIndex, int pageSize)
        {
            weikeBLL = new BLL.WeiKeBLL();
            IEnumerable<MODAL.WeiKeModel> weikemodel = Load(1, 10, "");
            return Json(weikemodel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(int pageIndex, int pageSize, string keyword)
        {
            weikeBLL = new BLL.WeiKeBLL();
            departmentBLL = new BLL.DepartmentBLL();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", 7);
            int totalCount = 0;
            IEnumerable<MODAL.WeiKeModel> weikemodel = weikeBLL.Manager_LoadPageEntities(pageIndex, pageSize, "teacherid=@id", dic, "", false, out totalCount);
            List<object> res = new List<object>();
            foreach (MODAL.WeiKeModel item in weikemodel)
            {
                res.Add(new { Id = item.Id, Name = item.Name, TypeId =  departmentBLL.LoadByParentID(item.TypeId), CreateTime = item.CreateTime});
            }
            var jsonresult = new { totalCount = totalCount, Data = res };
            return Json(jsonresult, JsonRequestBehavior.DenyGet);
        }
        private IEnumerable<MODAL.WeiKeModel> Load(int pageIndex, int pageSize, string keyword)
        {
            weikeBLL = new BLL.WeiKeBLL();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", 7);
            dic.Add("@keyword", '%' + keyword + '%');
            int totalCount = 0;
            IEnumerable<MODAL.WeiKeModel> weikemodel = weikeBLL.Manager_LoadPageEntities(pageIndex, 10, "teacherid=@id and name like @keyword", dic, "", false, out totalCount);
            return weikemodel;
        }

        public ActionResult DeleteWeike(int id)
        {
            weikeBLL = new BLL.WeiKeBLL();
            if (weikeBLL.DeleteModelByPrimaryKey(id))
            {
                return Json(new { state = true, msg = "删除成功!" }, JsonRequestBehavior.DenyGet);
            }
            else
            {
                return Json(new { state = false, msg = "删除失败!" }, JsonRequestBehavior.DenyGet);
            }
        }
    }
}
