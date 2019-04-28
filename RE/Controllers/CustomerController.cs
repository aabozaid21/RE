using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using RE.Models;
using System.Data.Entity;
namespace RE.Controllers
{
    public class CustomerController : Controller
    {
        
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                return View(db.Projects.ToList());
            }
            return RedirectToAction("Login", "HomePage");
            return View(db.Projects.ToList());
        }
        // customer home view
        public ActionResult CustomerHome()
        {
            return View(db.Projects.ToList());

        }
        //get delete project view
        public ActionResult DeleteProject(long? id)
        {
          /*  if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        // from action method delete project
        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProjectConfirmed(long id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // get assign project view
        public ActionResult AssignProject(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        // form action method assign project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignProject([Bind(Include = "ID,CustomerName,PMName,TLName,JEName,Name,Description,Price,Date,Code,Comment,Approval,PMAccept,TLAccept,JEAccept,Assign,Delivered,Feedback")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
        // search form action method 
        public ActionResult search(string searchstring, long? id)
        {
            var co = from c in db.Users select c;
            if (!String.IsNullOrEmpty(searchstring))
            {
                if (searchstring == "PM")
                {
                    co = co.Where(s => s.JobRole.Contains(searchstring));
                }
            }
            return View(co);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddP([Bind(Include = "ID,CustomerName,PMName,TLName,JEName,Name,Description,Price,Date,Code,Comment,Approval,TLAccept,JEAccept,Assign,Delivered,Feedback")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("CustomerHome");
            }

            return View(project);
        }
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProject([Bind(Include = "ID,CustomerName,PMName,TLName,JEName,Name,Description,Price,Date,Code,Comment,Approval,TLAccept,JEAccept,Assign,Delivered,Feedback")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }
     
    }
}