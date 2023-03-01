using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers.User
{
    public class UserController : Controller
    {
        RealEstateEntities db = new RealEstateEntities();

        private bool IsLogin()
        {
            return (Session.Count > 0 && Session["Type"].Equals("User"));
        }

        public ActionResult Index()
        {
            if (!IsLogin())
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login", "User");
        }

        // GET: User
        public ActionResult Login()
        {
            if (IsLogin())
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "EMAIL,PASSWORD")] USER uSER) {
            ViewBag.msg = ModelState.IsValid.ToString();
            if (ModelState.IsValid)
            {
                var usr = db.USERS.Where(m => m.EMAIL.Equals(uSER.EMAIL) && m.PASSWORD.Equals(uSER.PASSWORD)).SingleOrDefault();

                if (usr != null)
                {
                    Session["userId"] = usr.ID.ToString();
                    Session["Email"] = usr.EMAIL.ToString();
                    Session["Name"] = usr.FULLNAME.ToString();
                    Session["Type"] = usr.USER_ROLE.ToString();

                    return RedirectToAction("Dashboard");

                }
                else
                {
                    ViewBag.msg = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                                      <strong>Error! </strong>Invalid Email or Password
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                }

            }
            return View();
        }

        public ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "EMAIL,PASSWORD,FULLNAME,MOBILE,ABOUT_ME")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                uSER.REGISTRATION_DATE = DateTime.Now;
                uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("User")).Single().ID;
                db.USERS.Add(uSER);
                db.SaveChanges();

                ViewBag.msg = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                                      <strong>Success! </strong>Account Created
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                return View();
            }

            return View(uSER);
        }

        public ActionResult Dashboard()
        {
            if (!IsLogin())
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}