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
        private BLL.UsersBLL UserBll { get; set; }

        [Filter.StudentFilter]
        public ActionResult Index()
        {
            MODAL.UsersModel imagename;
            UserBll = new BLL.UsersBLL();
            imagename = UserBll.LoadImageAndName((int)Session["Id"]);
            ViewData.Model = imagename;
            return View();
        }

        [Filter.StudentFilter]
        public ActionResult PersonCenter()
        {
            UserBll = new BLL.UsersBLL();
            ViewData.Model = UserBll.GetEntity((int)Session["Id"]);
            return View();
        }

        [Filter.StudentFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            return Json(new { state = "succeed" }, JsonRequestBehavior.AllowGet);
        }

        [Filter.StudentFilter]
        public ActionResult EditEntity(MODAL.UsersModel w, string oldpwd)
        {
            UserBll = new BLL.UsersBLL();
            MODAL.UsersModel model = UserBll.GetEntity((int)Session["Id"]);
            if (UserBll.IsExist(w))
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

            bool res = UserBll.EditEntity(model);
            var result = new { state = res ? "succeed" : "failed", msg = "修改成功！" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserImg(int Id)
        {
            UserBll = new BLL.UsersBLL();
            MODAL.UsersModel m = UserBll.LoadImageAndName(Id);
            return File(Server.MapPath("~/Users/UserImg/") + (m.UserImagePath == null ? "default-personal.png" : m.UserImagePath), "image/jpeg");
        }

        public ActionResult UserLogin(string username, string pwd)
        {
            UserBll = new BLL.UsersBLL();
            int id;
            string role = "";
            if (System.Text.RegularExpressions.Regex.IsMatch(username, "(\\w)+(\\.\\w+)*@(\\w)+((\\.\\w+)+)"))
            {
                id = UserBll.Login(1, username, pwd, out role);
            }
            else
            {
                id = UserBll.Login(2, username, pwd, out role);
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
            if (Session["Id"] != null && Session["role"] != null)
            {
                return RedirectToAction("Index", "Home");
            } 
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(MODAL.UsersModel user)
        {

            return Json(user);
        }
    }
}
