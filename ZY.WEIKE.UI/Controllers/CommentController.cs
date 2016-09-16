using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZY.WEIKE.UI.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/
        private BLL.WordsBLL commentbll;
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Comment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Comment/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Comment/Create

        [HttpPost][Filter.StudentFilter]
        public ActionResult Create(MODAL.WordsModel wm)
        {
            commentbll = new BLL.WordsBLL();
            wm.UserId = (int)Session["Id"];
            wm.WordsTime = DateTime.Now;
            bool result = commentbll.CreateEntity(wm);
            return Json(new { state = result }, JsonRequestBehavior.DenyGet);
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Comment/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
