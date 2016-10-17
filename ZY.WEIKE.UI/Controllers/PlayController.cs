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
            MODEL.ResourceModel res = resbll.GetModelByPrimaryKey(id);
            ViewData.Add("res", res);
            Random next = new Random();
            List<int> Rating = new List<int>();

            MODEL.VotesModel m = GetStar(id);
            Rating.Add(m.Z_Star_1.Value);
            Rating.Add(m.Z_Star_2.Value);
            Rating.Add(m.Z_Star_3.Value);
            Rating.Add(m.Z_Star_4.Value);
            Rating.Add(m.Z_Star_5.Value);
            ViewBag.Total = m.Z_Star_1 + m.Z_Star_2 + m.Z_Star_3 + m.Z_Star_4 + m.Z_Star_5;
            ViewBag.Rating = Rating;
            ViewBag.Id = id;
            return View();
        }
        public ActionResult LoadRes(int id)
        {
            resbll = new BLL.ResourceBLL();
            MODEL.ResourceModel res = resbll.GetModelByPrimaryKey(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateWords(MODEL.WordsModel w)
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
            IEnumerable<MODEL.WordsModel> list = worbll.LoadPageEntities(pageindex, pagesize, out totalcount, "", dic, order, isasc);
            List<object> listresult = new List<object>();
            foreach (MODEL.WordsModel item in list)
            {
                MODEL.UsersModel m = new MODEL.UsersModel();
                m = userbll.LoadImageAndName(item.UserId);
                string path = "/Users/UserImg/" + (m.UserImagePath == null ? "default-personal.png" : m.UserImagePath);
                listresult.Add(new { WordsBody = item.WordsBody, WordsTime = item.WordsTime, WordsTitle = item.WordsTitle, UserName = m.UserName, Img = path });
            }
            return Json(new { count = totalcount, list = listresult }, JsonRequestBehavior.AllowGet);
        }

        private MODEL.VotesModel GetStar(int id)
        {
            votesbll = new BLL.VotesBLL();
            string where = "Z_weikeId=@wid";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("wid", id);
            return votesbll.GetEntity(where, dic);
        }

        [HttpPost]
        public ActionResult Votes(int score, int id)
        {
            votesbll = new BLL.VotesBLL();
            int res = votesbll.Vote((int)Session["Id"], ll(score, id));
            return Json(new { state =  res > 0 });
        }
        private MODEL.VotesModel ll(int score, int id)
        {
            MODEL.VotesModel m = new MODEL.VotesModel();
            m.Z_WeiKeId = id;
            switch (score)
            {
                case 1:
                    m.Z_Star_1 = 1;
                    break;
                case 2:
                    m.Z_Star_2 = 1;
                    break;
                case 3:
                    m.Z_Star_3 = 1;
                    break;
                case 4:
                    m.Z_Star_4 = 1;
                    break;
                default:
                    m.Z_Star_5 = 1;
                    break;
            }
            return m;
        }

    }
}
