using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers.User
{
    public class UserController : AuthController
    {
        RealEstateEntities db = new RealEstateEntities();

        public ActionResult Index()
        {
            if (!IsUserLogin())
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login", "User");
        }

        // GET: User
        public ActionResult Login()
        {
            if (IsUserLogin())
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "EMAIL,PASSWORD")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("User")).Single().ID;
                var usr = db.USERS.Where(m => m.EMAIL.Equals(uSER.EMAIL) && m.PASSWORD.Equals(uSER.PASSWORD) && m.ROLE_ID == uSER.ROLE_ID).SingleOrDefault();

                if (usr != null)
                {
                    Session["userId"] = usr.ID.ToString();
                    Session["Email"] = usr.EMAIL.ToString();
                    Session["Name"] = usr.FULLNAME.ToString();
                    Session["Type"] = db.USER_ROLE.Where(m => m.NAME.Equals("User")).Single().NAME.ToString();

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
            if (!IsUserLogin())
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

        public ActionResult Profile()
        {
            if (!IsUserLogin())
            {
                return RedirectToAction("Login");
            }

            int id = int.Parse(Session["userId"].ToString());

            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }

            return View(uSER);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "ID,EMAIL,PASSWORD,FULLNAME,MOBILE,ABOUT_ME,REGISTRATION_DATE")] USER uSER)
        {
            if (!IsUserLogin())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("User")).Single().ID;
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(uSER);
        }

        [HttpPost]
        public ActionResult SendQuery(string QUESTION, int ID)
        {
            if (!IsUserLogin())
            {
                return RedirectToAction("Login");
            }

            PROPERTY_QUERY pROPERTY_QUERY = new PROPERTY_QUERY();

            pROPERTY_QUERY.QUESTION = QUESTION;
            pROPERTY_QUERY.USER_ID = getUserID();
            pROPERTY_QUERY.VENDOR_ID = db.PROPERTies.Find(ID).ADDED_BY_ID;
            pROPERTY_QUERY.PROPERTY_ID = ID;
            pROPERTY_QUERY.Q_DATE = DateTime.Now;

            db.PROPERTY_QUERY.Add(pROPERTY_QUERY);
            db.SaveChanges();

            TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                                <strong>Success! </strong>Message Sent!
                                <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                            </div>";

            return RedirectToAction("PropertyDetails", "Home", new { ID });
        }

        public ActionResult AskedQuery()
        {
            if (!IsUserLogin())
            {
                return RedirectToAction("Login");
            }

            int userId = getUserID();
            var pROPERTY_QUERY = db.PROPERTY_QUERY.Include(p => p.PROPERTY)
                                                  .Include(p => p.USER)
                                                  .Include(p => p.USER1)
                                                  .Where(p => p.A_DATE == null && p.ANSWER == null && p.USER_ID == userId)
                                                  .OrderByDescending(p => p.Q_DATE); ;
            return View(pROPERTY_QUERY.ToList());
        }

        public ActionResult AnsweredQuery()
        {
            if (!IsUserLogin())
            {
                return RedirectToAction("Login");
            }

            int userId = getUserID();
            var pROPERTY_QUERY = db.PROPERTY_QUERY.Include(p => p.PROPERTY)
                                                  .Include(p => p.USER)
                                                  .Include(p => p.USER1)
                                                  .Where(p => p.A_DATE != null && p.ANSWER != null && p.USER_ID == userId)
                                                  .OrderByDescending(p => p.A_DATE);
            return View(pROPERTY_QUERY.ToList());
        }


    }
}