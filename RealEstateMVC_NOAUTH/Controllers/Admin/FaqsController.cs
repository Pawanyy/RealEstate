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
    public class FaqsController : AuthController
    {
        private RealEstateEntities db = new RealEstateEntities();

        // GET: Faqs
        public ActionResult Index()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            return View(db.FAQS.ToList());
        }

        // GET: Faqs/Details/5
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
            FAQ fAQ = db.FAQS.Find(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // GET: Faqs/Create
        public ActionResult Create()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        // POST: Faqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QUEST,ANSWER")] FAQ fAQ)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                db.FAQS.Add(fAQ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fAQ);
        }

        // GET: Faqs/Edit/5
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
            FAQ fAQ = db.FAQS.Find(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // POST: Faqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QUEST,ANSWER")] FAQ fAQ)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (ModelState.IsValid)
            {
                db.Entry(fAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fAQ);
        }

        // GET: Faqs/Delete/5
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
            FAQ fAQ = db.FAQS.Find(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // POST: Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            FAQ fAQ = db.FAQS.Find(id);
            db.FAQS.Remove(fAQ);
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
