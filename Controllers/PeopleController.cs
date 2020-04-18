using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhoIsWho.Models;using System.Diagnostics;

namespace WhoIsWho.Controllers
{
    public class PeopleController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        public ActionResult Index(int? idGame = 4)
        {
            this.Session["idGame"] = idGame;
            return View(db.People.ToList());
        }

        [HttpPost]
        public JsonResult Details(int? id)
        {
            Person person = db.People.Find(id);
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(String name, String picture)
        {
            return View(new Person { Name = name, Picture = picture });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Picture")] Person todo)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person todo = db.People.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Picture")] Person todo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person todo = db.People.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person todo = db.People.Find(id);
            db.People.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
