using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers
{
    public class AdminController : AuthController
    {
        // GET: Admin

        private RealEstateEntities db = new RealEstateEntities();

        public ActionResult Index()
        {
            if (IsAdminLogin())
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(ADMIN admin)
        {
            if(ModelState.IsValid)
            {
                var usr = db.ADMINs.Where( m => m.USERNAME.Equals(admin.USERNAME) && m.PASSWORD.Equals(admin.PASSWORD)).SingleOrDefault();

                if(usr != null)
                {
                    Session["userId"] = usr.ID.ToString();
                    Session["Email"] = usr.USERNAME.ToString();
                    Session["Type"] = "Admin";

                    return RedirectToAction("Dashboard");

                } else
                {
                    ViewBag.msg = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                                      <strong>Error! </strong>Invalid Email or Password
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                }

            }
            return View();
        }

        public ActionResult Dashboard()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Profile()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index");
            }

            ADMIN aDMIN = db.ADMINs.Find(int.Parse(Session["userId"].ToString()));
            if (aDMIN != null)
            {
                return View(aDMIN);
            }

            return View("Index");

        }

        [HttpPost]
        public ActionResult Profile([Bind(Include = "ID,USERNAME,PASSWORD")] ADMIN aDMIN)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Entry(aDMIN).State = EntityState.Modified;
                db.SaveChanges();

                Session.RemoveAll();
                return RedirectToAction("Index");
            }
            return View(aDMIN);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

    }
}