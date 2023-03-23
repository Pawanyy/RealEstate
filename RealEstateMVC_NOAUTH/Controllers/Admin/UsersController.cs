using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstateMVC_NOAUTH.Models;

namespace RealEstateMVC_NOAUTH.Controllers.Admin
{
    public class UsersController : AuthController
    {
        private RealEstateEntities db = new RealEstateEntities();

        // GET: Users
        public ActionResult Index()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            var uSERS = db.USERS.Include(u => u.USER_ROLE);
            return View(uSERS.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.ROLE_ID = new SelectList(db.USER_ROLE, "ID", "NAME");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EMAIL,PASSWORD,FULLNAME,MOBILE,ROLE_ID,ABOUT_ME")] USER uSER)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                var usr = db.USERS.Where(m => m.EMAIL.Equals(uSER.EMAIL)).SingleOrDefault();
                if (usr == null)
                {
                    uSER.REGISTRATION_DATE = DateTime.Now;
                    db.USERS.Add(uSER);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.msg = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                                      <strong>Error! </strong>Account with Email Exist
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                    return View(uSER);
                }

                return RedirectToAction("Index");
            }

            ViewBag.ROLE_ID = new SelectList(db.USER_ROLE, "ID", "NAME", uSER.ROLE_ID);
            return View(uSER);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.ROLE_ID = new SelectList(db.USER_ROLE, "ID", "NAME", uSER.ROLE_ID);
            return View(uSER);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EMAIL,PASSWORD,FULLNAME,MOBILE,ROLE_ID,ABOUT_ME,REGISTRATION_DATE")] USER uSER)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ROLE_ID = new SelectList(db.USER_ROLE, "ID", "NAME", uSER.ROLE_ID);
            return View(uSER);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            USER uSER = db.USERS.Find(id);

             var propertyQuries = db.PROPERTY_QUERY.Where(p => p.VENDOR_ID == id || p.USER_ID == id);

            foreach(var property in propertyQuries.ToList())
            {
                db.PROPERTY_QUERY.Remove(property);
            }

            db.USERS.Remove(uSER);
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
