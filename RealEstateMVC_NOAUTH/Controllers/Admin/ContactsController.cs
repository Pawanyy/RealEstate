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
    public class ContactsController : Controller
    {
        private RealEstateEntities db = new RealEstateEntities();
        private bool IsLogin()
        {
            return (Session.Count > 0 && Session["Type"].Equals("Admin"));
        }

        // GET: Contacts
        public ActionResult Index()
        {
            if (!IsLogin())
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(db.CONTACTs.ToList());
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!IsLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACT cONTACT = db.CONTACTs.Find(id);
            if (cONTACT == null)
            {
                return HttpNotFound();
            }
            return View(cONTACT);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsLogin())
            {
                return RedirectToAction("Index", "Admin");
            }

            CONTACT cONTACT = db.CONTACTs.Find(id);
            db.CONTACTs.Remove(cONTACT);
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
