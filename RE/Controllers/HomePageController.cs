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
    public class HomePageController : Controller
    {
        private DBContext db = new DBContext();
        // GET: HomePage
        [HttpGet]
        public ActionResult Home()
        {

            return View();
        }

       
        [HttpGet]

       public ActionResult About()

        {

            return View();

        }
        public ActionResult Contact()
        {

            return View();

        }
       public ActionResult Login()
        {

            return View();

        }
        public ActionResult Register()
        {

            return View();

        }
/* [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (DBContext db = new DBContext())
                {
                   
                        db.Users.Add(user);
                        db.SaveChanges();

                }
                ModelState.Clear();
                ViewBag.Message = user.UserName + " " + "Success.";

                if (user.JobRole == "Admin")
                    return RedirectToAction("HomePage", "Admin");
                else if (user.JobRole == "PM")
                    return RedirectToAction("HomePage", "PM");
                else if (user.JobRole == "Customer") 
                    return RedirectToAction("CustomerHome", "Customer");
                else if (user.JobRole == "TL")
                    return RedirectToAction("HomePage","TL");
                else if (user.JobRole == "JE")
                    return RedirectToAction("HomePage", "JE");
                //return RedirectToAction("Home","HomePage");//1-ActionName 2-controllerName
            }
            return View();
        }*/

        [HttpPost]
        public ActionResult LogIn(User userl)
        {
            using (DBContext db = new DBContext())
            {
                try
                {
                    var usr = db.Users.Single(u => u.UserName == userl.UserName && u.Password == userl.Password);
                    if (usr != null)
                    {
                        Session["ID"] = usr.ID.ToString();
                        Session["UserName"] = usr.UserName.ToString();
                        Session["FirstName"] = usr.FirstName.ToString();
                        Session["LastName"] = usr.LastName.ToString();
                        Session["Mobile"] = usr.Mobile.ToString();
                        Session["Email"] = usr.Email.ToString();
                        Session["JobDescription"] = usr.JobDescription.ToString();
                        Session["JobRole"] = usr.JobRole.ToString();
                      //  return RedirectToAction("Loggedin");
                       if (usr.JobRole == "Admin")
                            return RedirectToAction("HomePage", "Admin");
                        else if (usr.JobRole == "PM")
                            return RedirectToAction("HomePage", "PM");
                        else if (usr.JobRole == "Customer")
                            return RedirectToAction("CustomerHome", "Customer");
                       else if (usr.JobRole == "TL")
                           return RedirectToAction("HomePage", "TL");
                       else if (usr.JobRole == "JE")
                           return RedirectToAction("HomePage", "JE");
                    }
                    else
                        ModelState.AddModelError("", "Info Is Wrong.");
                }
                catch (Exception ex)
                {

                    this.Session["userName OR password is incorrect"] = ex.Message;
                    Response.Redirect("Register.cshtml");
                    Response.Write(ex.Message + ex.StackTrace);


                }
            }
            return View();
        }
        public ActionResult Loggedin()
        {
            if (Session["ID"] != null)
            {
                var Role = Session["JobRole"].ToString();
                if (Role == "Customer")
                    return RedirectToAction("Index", "Customer");
                else if (Role == "Admin")
                    return RedirectToAction("Index", "Admin");
                else if (Role== "PM")
                 return RedirectToAction("Index", "PM");
                else if (Role == "TL")
                    return RedirectToAction("Index", "TL");
                else if (Role == "JE")
                    return RedirectToAction("Index", "JE");
                 else  
                    return View();   
            }
            else
            {
                  return RedirectToAction("Login");
            }
         
           
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                
                    using (DBContext db = new DBContext())
                    {
                        var usr = db.Users.Single(u => u.UserName == user.UserName && u.Email == user.Email);
                        if (usr == null)
                        {
                            db.Users.Add(user);
                            db.SaveChanges();
                            ModelState.Clear();
                            ViewBag.Message = user.UserName + " " + "Success.";

                            if (user.JobRole == "Admin")
                                return RedirectToAction("HomePage", "Admin");
                            else if (user.JobRole == "PM")
                                return RedirectToAction("HomePage", "PM");
                            else if (user.JobRole == "Customer")
                                return RedirectToAction("CustomerHome", "Customer");
                            else if (user.JobRole == "TL")
                                return RedirectToAction("HomePage", "TL");
                            else if (user.JobRole == "JE")
                                return RedirectToAction("HomePage", "JE");
                        }
                        else
                        {
                            return RedirectToAction("LogIn", "HomePage");
                        }
                        
                    }
                
            }
            return View();
        }
       
    }
}