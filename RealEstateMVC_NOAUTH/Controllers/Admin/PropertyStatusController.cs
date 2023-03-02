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
    public class PropertyStatusController : Controller
    {
        private RealEstateEntities db = new RealEstateEntities();

        // GET: PropertyStatus
        public ActionResult Index()
        {
            return View(db.PROPERTY_STATUS.ToList());
        }

        // GET: PropertyStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY_STATUS pROPERTY_STATUS = db.PROPERTY_STATUS.Find(id);
            if (pROPERTY_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY_STATUS);
        }

        // GET: PropertyStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,STATUS")] PROPERTY_STATUS pROPERTY_STATUS)
        {
            if (ModelState.IsValid)
            {
                db.PROPERTY_STATUS.Add(pROPERTY_STATUS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pROPERTY_STATUS);
        }

        // GET: PropertyStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY_STATUS pROPERTY_STATUS = db.PROPERTY_STATUS.Find(id);
            if (pROPERTY_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY_STATUS);
        }

        // POST: PropertyStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,STATUS")] PROPERTY_STATUS pROPERTY_STATUS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROPERTY_STATUS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROPERTY_STATUS);
        }

        // GET: PropertyStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY_STATUS pROPERTY_STATUS = db.PROPERTY_STATUS.Find(id);
            if (pROPERTY_STATUS == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY_STATUS);
        }

        // POST: PropertyStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROPERTY_STATUS pROPERTY_STATUS = db.PROPERTY_STATUS.Find(id);
            db.PROPERTY_STATUS.Remove(pROPERTY_STATUS);
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
