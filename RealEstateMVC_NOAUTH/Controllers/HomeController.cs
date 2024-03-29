﻿using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers
{
    public class HomeController : Controller
    {

        RealEstateEntities db = new RealEstateEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View(db.FAQS.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "NAME,EMAIL,SUBJECT,PHONE,MESSAGE")] CONTACT cONTACT)
        {
            if (ModelState.IsValid)
            {
                cONTACT.DATE = DateTime.Now;
                db.CONTACTs.Add(cONTACT);
                db.SaveChanges();
                ViewBag.msg = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                                      <strong>Success! </strong>Message Sent Successfully
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                return View("Contact");
            }

            return View(cONTACT);
        }

        public ActionResult Properties()
        {
            var pROPERTies = db.PROPERTies.Include(p => p.CITY)
                .Include(p => p.COUNTRY)
                .Include(p => p.PROPERTY_TYPE)
                .Include(p => p.STATE)
                .Include(p => p.PROPERTY_STATUS)
                .Include(p => p.USER);
            return View(pROPERTies.ToList());
        }

        public ActionResult PropertyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY pROPERTY = db.PROPERTies.Find(id);
            if (pROPERTY == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY);
        }



        public ActionResult Login()
        {
            return View();
        }
    }
}