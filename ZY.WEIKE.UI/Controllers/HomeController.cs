using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Controllers
{
    //[OutputCache(Duration = 3 * 60 * 60)]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private BLL.ColleageBLL colleageBll;
        private BLL.WeiKeBLL weikeBll;
        private BLL.ResourceBLL resbll;
        private IEnumerable<MODEL.ColleageModel> colleageModelList;
        private IEnumerable<MODEL.WeiKeModel> newlist;
        //[OutputCache(Duration = 3 * 60 * 60)]
        public ActionResult Index()
        {
            colleageBll = new BLL.ColleageBLL();
            weikeBll = new BLL.WeiKeBLL();
            //return Content(DateTime.Now.ToString("hh:mm:ss"));
            colleageModelList = colleageBll.GetTopList(7);
            newlist = weikeBll.LoadEntities("", "CreateTime", null, false, 1);
            ViewData.Add("list_colleagemodel", colleageModelList);
            ViewData.Add("list_new", newlist);
            
            return View();
        }

        public ActionResult ImgPath(int id)
        {
            resbll = new BLL.ResourceBLL();
            return File(Server.MapPath("/Users/" + resbll.GetImgPath(id)), "image/jpeg");
            //return Content(id.ToString());
        }
    }
}
