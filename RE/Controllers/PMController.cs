using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RE.Models;
using System.Net;
using System.Data.Entity;
namespace RE.Controllers
{
    public class PMController : Controller
    {
        private DBContext db = new DBContext();
        // GET: PM
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                List<object> MyModel = new List<object>();
                MyModel.Add(db.Users.ToList());
                MyModel.Add(db.Projects.ToList());
                return View(MyModel);
            }
            return RedirectToAction("Login", "HomePage");
        }
        public ActionResult PieCart()
        {
            return View(db.Projects.ToList());
        }
        public ActionResult HomePage()
        {
            return View(db.Projects.ToList());
        }
        //get accept or reject project
        public ActionResult AcceptOrReject(long? id)
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
        //post action method accept or reject project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptOrReject([Bind(Include = "ID,CustomerName,PMName,TLName,JEName,Name,Description,Price,Date,Code,Comment,Approval,PMAccept,TLAccept,JEAccept,Assign,Delivered,Feedback")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
        public ActionResult CreateTeam(long? id)
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
        // search for JE & TL
        public ActionResult search(string searchstring, long? id)
        {
            var co = from c in db.Users select c;
            if (!String.IsNullOrEmpty(searchstring))
            {
                if (searchstring == "TL" || searchstring == "JE")
                {
                    co = co.Where(s => s.JobRole.Contains(searchstring));
                }
            }
            return View(co);
        }
        //post form action create team 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeam([Bind(Include = "ID,CustomerName,PMName,TLName,JEName,Name,Description,Price,Date,Code,Comment,Approval,PMAccept,TLAccept,JEAccept,Assign,Delivered,Feedback")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
        // return submit view 
        public ActionResult Submit(long? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "ID,CustomerName,PMName,TLName,JEName,Name,Description,Price,Date,Code,Comment,Approval,PMAccept,TLAccept,JEAccept,Assign,Delivered,Submit,Report,Feedback,Comments")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
    }
}