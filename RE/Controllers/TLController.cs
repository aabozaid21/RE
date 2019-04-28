using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using RE.Models;
using System.Data.Entity;

namespace RE.Controllers
{
    public class TLController : Controller
    {
        private DBContext db = new DBContext();
        // GET: TL
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
        //get the home page view
        public ActionResult HomePage()
        {
            return View(db.Projects.ToList());
        }

        // get the accept or reject project view
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
        // get the search view 
        public ActionResult search(string searchstring, long? id)
        {
            var co = from c in db.Users select c;
            if (!String.IsNullOrEmpty(searchstring))
            {
                if (searchstring == "JE")
                {
                    co = co.Where(s => s.JobRole.Contains(searchstring));
                }
            }
            return View(co);
        }
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
    }
}