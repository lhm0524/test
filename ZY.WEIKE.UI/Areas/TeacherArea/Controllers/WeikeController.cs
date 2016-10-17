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
            IEnumerable<MODEL.ColleageModel> colleagemodel = colleageBLL.LoadEntites("", null, "", false);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", 7);
            int count = weikeBLL.GetAllPageCountForWhere("teacherId=@id", dic);
            ViewBag.Count = count;
            ViewData.Add("list_colleagemodel", colleagemodel);
            return View();
        }

        public ActionResult Add()
        {
            colleageBLL = new BLL.ColleageBLL();
            IEnumerable<MODEL.ColleageModel> colleagemodel = colleageBLL.LoadEntites("", null, "", false);
            ViewData.Add("list_colleagemodel", colleagemodel);
            return View();
        }

        [HttpPost]
        public ActionResult Add(MODEL.WeiKeModel entity)
        {
            string serverpath = Server.MapPath(COMMONHELPER.CacheHelper.Get("filespath").ToString());
            MODEL.ResourceModel m = new MODEL.ResourceModel();
            weikeBLL = new BLL.WeiKeBLL();
            App_Start.Upload Uploader;
            bool falg = false;
            entity.Detail = entity.Detail.Trim();
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description.Trim();
            entity.TeacherId = 7;//(int)Session["Id"];
            #region 上传图片
            if (Request.Files["pic"].ContentLength != 0)
            {
                falg = true;
                Uploader = new App_Start.ImageUploader();
                Uploader.File = Request.Files["pic"];
                Uploader.ServerPath = serverpath + "images\\";
                Uploader.UploadAction();
                switch (Uploader.State)
                {
                    case 0:
                        return Json(new { state = false, msg = "文件为空！" });
                        break;
                    case -1:
                        return Json(new { state = false, msg = "非法图片！" });
                        break;
                    case -2:
                        return Json(new { state = false, msg = "图片过大！" });
                        break;
                    case 1:
                        m.VideoImgPath = "\\images\\" + Uploader.NewName;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region 上传视频
            Uploader = new App_Start.VideoUploader();
            Uploader.File = Request.Files["video"];
            Uploader.ServerPath = (serverpath + "Videos\\");
            Uploader.UploadAction();
            switch (Uploader.State)
            {
                case 0:
                    return Json(new { state = false, msg = "视频文件为空！" });
                    break;
                case -1:
                    return Json(new { state = false, msg = "非法视频拓展名！" });
                    break;
                case -2:
                    return Json(new { state = false, msg = "视频文件过大！" });
                    break;
                case 1:
                    m.VideoPath = "\\Videos\\" + Uploader.NewName;
                    if (!falg)
                    {
                        m.VideoImgPath = "\\images\\" + COMMONHELPER.CommonHelper.CaptureVideo(System.Configuration.ConfigurationManager.AppSettings["ffmpegpath"], serverpath + m.VideoPath, serverpath + "Images\\", "video" + Guid.NewGuid().ToString());
                    }
                    break;
                default:
                    break;
            }
            #endregion

            #region 附件
            if (Request.Files["attach"].ContentLength != 0)
            {
                Uploader = new App_Start.AttachUploader();
                Uploader.File = Request.Files["attach"];
                Uploader.ServerPath = (serverpath + "Attachs\\");
                Uploader.UploadAction();
                switch (Uploader.State)
                {
                    case 0:
                        return Json(new { state = false, msg = "文件为空！" });
                        break;
                    case -1:
                        return Json(new { state = false, msg = "非法拓展名！" });
                        break;
                    case -2:
                        return Json(new { state = false, msg = "文件过大！" });
                        break;
                    case 1:
                        m.AttachmentPath = "\\Attachs\\" + Uploader.NewName;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            m.TotalProgress = COMMONHELPER.CommonHelper.GetVideoDuration(System.Configuration.ConfigurationManager.AppSettings["ffmpegpath"], serverpath + m.VideoPath);
            if (weikeBLL.CreateEntity(entity, m))
            {
                return new RedirectResult("/Teacher/Weike/Index");
            }
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
            IEnumerable<MODEL.WeiKeModel> weikemodel = Load(1, 10, "");
            return Json(weikemodel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(int pageIndex, int pageSize, string keyword)
        {
            weikeBLL = new BLL.WeiKeBLL();
            departmentBLL = new BLL.DepartmentBLL();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", 7);
            int totalCount = 0;
            IEnumerable<MODEL.WeiKeModel> weikemodel = weikeBLL.Manager_LoadPageEntities(pageIndex, pageSize, "teacherid=@id", dic, "CreateTime", false, out totalCount);
            List<object> res = new List<object>();
            foreach (MODEL.WeiKeModel item in weikemodel)
            {
                res.Add(new { Id = item.Id, Name = item.Name, TypeId = departmentBLL.GetEntityByPrimarykey(item.TypeId).Name, CreateTime = item.CreateTime });
            }
            var jsonresult = new { totalCount = totalCount, Data = res };
            return Json(jsonresult, JsonRequestBehavior.DenyGet);
        }
        private IEnumerable<MODEL.WeiKeModel> Load(int pageIndex, int pageSize, string keyword)
        {
            weikeBLL = new BLL.WeiKeBLL();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", 7);
            dic.Add("@keyword", '%' + keyword + '%');
            int totalCount = 0;
            IEnumerable<MODEL.WeiKeModel> weikemodel = weikeBLL.Manager_LoadPageEntities(pageIndex, 10, "teacherid=@id and name like @keyword", dic, "", false, out totalCount);
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
