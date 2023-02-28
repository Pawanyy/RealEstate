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
    public class PropertyTypesController : Controller
    {
        private RealEstateEntities db = new RealEstateEntities();

        // GET: PropertyTypes
        public ActionResult Index()
        {
            return View(db.PROPERTY_TYPE.ToList());
        }

        // GET: PropertyTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY_TYPE pROPERTY_TYPE = db.PROPERTY_TYPE.Find(id);
            if (pROPERTY_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY_TYPE);
        }

        // GET: PropertyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME")] PROPERTY_TYPE pROPERTY_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.PROPERTY_TYPE.Add(pROPERTY_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pROPERTY_TYPE);
        }

        // GET: PropertyTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY_TYPE pROPERTY_TYPE = db.PROPERTY_TYPE.Find(id);
            if (pROPERTY_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY_TYPE);
        }

        // POST: PropertyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME")] PROPERTY_TYPE pROPERTY_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROPERTY_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROPERTY_TYPE);
        }

        // GET: PropertyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY_TYPE pROPERTY_TYPE = db.PROPERTY_TYPE.Find(id);
            if (pROPERTY_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY_TYPE);
        }

        // POST: PropertyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROPERTY_TYPE pROPERTY_TYPE = db.PROPERTY_TYPE.Find(id);
            db.PROPERTY_TYPE.Remove(pROPERTY_TYPE);
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
