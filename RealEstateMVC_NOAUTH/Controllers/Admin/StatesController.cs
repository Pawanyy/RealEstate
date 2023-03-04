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
    public class StatesController : AuthController
    {
        private RealEstateEntities db = new RealEstateEntities();

        // GET: States
        public ActionResult Index()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            var sTATEs = db.STATEs.Include(s => s.COUNTRY);
            return View(sTATEs.ToList());
        }

        // GET: States/Details/5
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
            STATE sTATE = db.STATEs.Find(id);
            if (sTATE == null)
            {
                return HttpNotFound();
            }
            return View(sTATE);
        }

        // GET: States/Create
        public ActionResult Create()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME");
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,COUNTRY_ID")] STATE sTATE)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                db.STATEs.Add(sTATE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME", sTATE.COUNTRY_ID);
            return View(sTATE);
        }

        // GET: States/Edit/5
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
            STATE sTATE = db.STATEs.Find(id);
            if (sTATE == null)
            {
                return HttpNotFound();
            }
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME", sTATE.COUNTRY_ID);
            return View(sTATE);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,COUNTRY_ID")] STATE sTATE)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                db.Entry(sTATE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME", sTATE.COUNTRY_ID);
            return View(sTATE);
        }

        // GET: States/Delete/5
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
            STATE sTATE = db.STATEs.Find(id);
            if (sTATE == null)
            {
                return HttpNotFound();
            }
            return View(sTATE);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            STATE sTATE = db.STATEs.Find(id);
            db.STATEs.Remove(sTATE);
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
