using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrainWeb.Models;
using System.Data.SqlClient;
using System.Data.Entity;

namespace BrainWeb.Areas.Admins.Controllers
{
    public class HomeController : Controller
    {
        labEntities db = new labEntities();
        private object entitystate;

        // GET: Admins/Home
        public ActionResult Index()
        {
            ViewBag.employe = db.Employer.OrderBy(a => a.id).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert([Bind(Include ="id,name,surname,age,gender")]Employer emp)
        {
            if (ModelState.IsValid)
            {
                db.Employer.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            return View(emp);
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "id,name,surname,age,gender")]Employer emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public ActionResult Delete(int? id)
        {
            Employer emp = db.Employer.SingleOrDefault(x => x.id == id);
            db.Employer.Remove(emp);
            db.SaveChanges();

            return RedirectToAction("index");
        }
    }
}