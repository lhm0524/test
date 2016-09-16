using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private BLL.UsersBLL userbll;

        [Filter.StudentFilter]
        public ActionResult Index()
        {
            MODAL.UsersModel imagename;
            userbll = new BLL.UsersBLL();
            imagename = userbll.LoadImageAndName((int)Session["Id"]);
            ViewData.Model = imagename;
            return View();
        }

        public ActionResult UserLogin(string username, string pwd)
        {
            userbll = new BLL.UsersBLL();
            int id;
            string role = "";
            if (System.Text.RegularExpressions.Regex.IsMatch(username, "(\\w)+(\\.\\w+)*@(\\w)+((\\.\\w+)+)"))
            {
                id = userbll.Login(1, username, pwd, out role);
            }
            else
            {
                id = userbll.Login(2, username, pwd, out role);
            }
            if (role != "null")
            {
                Session["Id"] = id;
                Session["Role"] = role;
                return Json(new { result = "sucessed" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "failed" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [Filter.StudentFilter]
        public ActionResult PersonCenter()
        {
            userbll = new BLL.UsersBLL();
            ViewData.Model = userbll.GetEntity((int)Session["Id"]);
            return View();
        }

        [Filter.StudentFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            return Json(new { state = "succeed" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditEntity(MODAL.UsersModel w, string oldpwd)
        {
            userbll = new BLL.UsersBLL();
            MODAL.UsersModel model = userbll.GetEntity((int)Session["Id"]);
            if (userbll.IsExist(w))
            {
                return Json(new { state = "failed", msg = "已存在相同的用户，请重试！" }, JsonRequestBehavior.AllowGet);
            }
            if (w.UserPwd != null)
            {
                if (oldpwd != model.UserPwd)
                {
                    return Json(new { state = "failed", msg = "原始密码不正确！" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.UserPwd = w.UserPwd;
                }
            }
            model.UserName = w.UserName;
            model.Birthday = w.Birthday;
            model.Email = w.Email;
            model.Sex = w.Sex.Equals("1") ? "男" : "女";
            model.Answer = w.Answer;

            bool res = userbll.EditEntity(model);
            var result = new { state = res ? "succeed" : "failed", msg = "修改成功！" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserImg(int Id)
        {
            userbll = new BLL.UsersBLL();
            MODAL.UsersModel m = userbll.LoadImageAndName(Id);
            return File(Server.MapPath("~/Users/UserImg/") + (m.UserImagePath == null ? "default-personal.png" : m.UserImagePath), "image/jpeg");
        }
    }
}
