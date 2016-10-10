using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Controllers
{
    public class PlayController : Controller
    {
        //
        // GET: /Play/
        public BLL.WeiKeBLL weikebll { get; set; }
        public BLL.ResourceBLL resbll { get; set; }
        public BLL.WordsBLL worbll { get; set; }
        public BLL.UsersBLL userbll { get; set; }
        public BLL.VotesBLL votesbll { get; set; }
        //[OutputCache(Duration = 1 * 60 * 60)]
        public ActionResult Index(int id)
        {
            weikebll = new BLL.WeiKeBLL();
            resbll = new BLL.ResourceBLL();
            ViewData.Model = weikebll.GetModelByPrimaryKey(id);
            MODAL.ResourceModel res = resbll.GetModelByPrimaryKey(id);
            ViewData.Add("res", res);
            Random next = new Random();
            List<int> Rating = new List<int>();

            MODAL.VotesModel m = GetStar(id);
            Rating.Add(m.Z_Star_1.Value);
            Rating.Add(m.Z_Star_2.Value);
            Rating.Add(m.Z_Star_3.Value);
            Rating.Add(m.Z_Star_4.Value);
            Rating.Add(m.Z_Star_5.Value);

            ViewBag.Rating = Rating;
            return View();
        }
        public ActionResult LoadRes(int id)
        {
            resbll = new BLL.ResourceBLL();
            MODAL.ResourceModel res = resbll.GetModelByPrimaryKey(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateWords(MODAL.WordsModel w)
        {

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadCommit(int pagesize, int pageindex, string order, bool isasc,int id)
        {
            worbll = new BLL.WordsBLL();
            userbll = new BLL.UsersBLL();
            int totalcount;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@id", id);
            IEnumerable<MODAL.WordsModel> list = worbll.LoadPageEntities(pageindex, pagesize, out totalcount, "", dic, order, isasc);
            List<object> listresult = new List<object>();
            foreach (MODAL.WordsModel item in list)
            {
                MODAL.UsersModel m = new MODAL.UsersModel();
                m = userbll.LoadImageAndName(item.UserId);
                string path = "/Users/UserImg/" + (m.UserImagePath == null ? "default-personal.png" : m.UserImagePath);
                listresult.Add(new { WordsBody = item.WordsBody, WordsTime = item.WordsTime, WordsTitle = item.WordsTitle, UserName = m.UserName, Img = path });
            }
            return Json(new { count = totalcount, list = listresult }, JsonRequestBehavior.AllowGet);
        }

        private MODAL.VotesModel GetStar(int id)
        {
            votesbll = new BLL.VotesBLL();
            string where = "Z_weikeId=@wid";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("wid", id);
            return votesbll.GetEntity(where, dic);
        }

    }
}
