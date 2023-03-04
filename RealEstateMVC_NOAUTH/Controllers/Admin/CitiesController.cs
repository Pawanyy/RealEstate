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
    public class CitiesController : AuthController
    {
        private RealEstateEntities db = new RealEstateEntities();

        // GET: Cities
        public ActionResult Index()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            var cITies = db.CITies.Include(c => c.STATE);
            return View(cITies.ToList());
        }

        // GET: Cities/Details/5
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
            CITY cITY = db.CITies.Find(id);
            if (cITY == null)
            {
                return HttpNotFound();
            }
            return View(cITY);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.STATE_ID = new SelectList(db.STATEs, "ID", "NAME");
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,STATE_ID")] CITY cITY)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                db.CITies.Add(cITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.STATE_ID = new SelectList(db.STATEs, "ID", "NAME", cITY.STATE_ID);
            return View(cITY);
        }

        // GET: Cities/Edit/5
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
            CITY cITY = db.CITies.Find(id);
            if (cITY == null)
            {
                return HttpNotFound();
            }
            ViewBag.STATE_ID = new SelectList(db.STATEs, "ID", "NAME", cITY.STATE_ID);
            return View(cITY);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,STATE_ID")] CITY cITY)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                db.Entry(cITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.STATE_ID = new SelectList(db.STATEs, "ID", "NAME", cITY.STATE_ID);
            return View(cITY);
        }

        // GET: Cities/Delete/5
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
            CITY cITY = db.CITies.Find(id);
            if (cITY == null)
            {
                return HttpNotFound();
            }
            return View(cITY);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            CITY cITY = db.CITies.Find(id);
            db.CITies.Remove(cITY);
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
