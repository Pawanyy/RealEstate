﻿using Microsoft.Ajax.Utilities;
using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers
{
    public class VendorController : AuthController
    {
        // GET: Vendor
        RealEstateEntities db = new RealEstateEntities();

        public ActionResult Index()
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Dashboard", "Vendor");
        }

        #region VendorAuth
        public ActionResult Login()
        {
            if (IsVendorLogin())
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
                uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("Vendor")).Single().ID;
                var usr = db.USERS.Where(m => m.EMAIL.Equals(uSER.EMAIL) && m.PASSWORD.Equals(uSER.PASSWORD) && m.ROLE_ID == uSER.ROLE_ID).SingleOrDefault();

                if (usr != null)
                {
                    Session["userId"] = usr.ID.ToString();
                    Session["Email"] = usr.EMAIL.ToString();
                    Session["Name"] = usr.FULLNAME.ToString();
                    Session["Type"] = db.USER_ROLE.Where(m => m.NAME.Equals("Vendor")).Single().NAME.ToString();

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
                var usr = db.USERS.Where(m => m.EMAIL.Equals(uSER.EMAIL)).SingleOrDefault();
                if (usr == null)
                {
                    uSER.REGISTRATION_DATE = DateTime.Now;
                    uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("Vendor")).Single().ID;
                    db.USERS.Add(uSER);
                    db.SaveChanges();

                    ViewBag.msg = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                                      <strong>Success! </strong>Account Created
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                } else
                {
                    ViewBag.msg = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                                      <strong>Error! </strong>Account with Email Exist
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                }
                return View();
            }

            return View(uSER);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

        #endregion VendorAuth
        public ActionResult Dashboard()
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Profile()
        {
            if (!IsVendorLogin())
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
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("Vendor")).Single().ID;
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(uSER);
        }

        #region Property
        public ActionResult MyProperties()
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            int id = getUserID();
            var pROPERTies = db.PROPERTies.Include(p => p.CITY).Include(p => p.COUNTRY).Include(p => p.USER).Include(p => p.PROPERTY_TYPE).Include(p => p.STATE).Include(p => p.PROPERTY_STATUS).Where(p => p.ADDED_BY_ID == id);
            return View(pROPERTies.ToList());
        }

        public ActionResult AddProperty()
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            ViewBag.CITY_ID = new SelectList("");//new SelectList(db.CITies, "ID", "NAME");
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME");
            ViewBag.ADDED_BY_ID = new SelectList(db.USERS, "ID", "EMAIL");
            ViewBag.PROPERTY_TYPE_ID = new SelectList(db.PROPERTY_TYPE, "ID", "NAME");
            ViewBag.STATE_ID = new SelectList(""); //new SelectList(db.STATEs, "ID", "NAME");
            ViewBag.STATUS_ID = new SelectList(db.PROPERTY_STATUS, "ID", "STATUS");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProperty([Bind(Include = "NAME,DESCR,PROPERTY_TYPE_ID,STATUS_ID,LOCATION,BEDROOMS,BATHROOMS,FLOORS,GARAGES,AREA,PRICE,BEFORE_PRICE_LABEL,AFTER_PRICE_LABEL,FEATURES,IMAGE_1,IMAGE_2,IMAGE_3,IMAGE_4,ADDRESS,COUNTRY_ID,STATE_ID,CITY_ID,POSTAL_CODE,NEIGHBORHOOD")] PROPERTY pROPERTY)
        {

            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                // Image 1
                string filename_1 = Path.GetFileName(pROPERTY.IMAGE_1.FileName);
                string _filename_1 = DateTime.Now.ToString("hhmmssfff") + filename_1;
                string path_1 = Path.Combine(Server.MapPath("~/Image/"), _filename_1);

                pROPERTY.IMG_1 = "~/Image/" + _filename_1;

                // Image 2
                string filename_2 = Path.GetFileName(pROPERTY.IMAGE_2.FileName);
                string _filename_2 = DateTime.Now.ToString("hhmmssfff") + filename_2;
                string path_2 = Path.Combine(Server.MapPath("~/Image/"), _filename_2);

                pROPERTY.IMG_2 = "~/Image/" + _filename_2;

                // Image 3
                string filename_3 = Path.GetFileName(pROPERTY.IMAGE_3.FileName);
                string _filename_3 = DateTime.Now.ToString("hhmmssfff") + filename_3;
                string path_3 = Path.Combine(Server.MapPath("~/Image/"), _filename_3);

                pROPERTY.IMG_3 = "~/Image/" + _filename_3;

                // Image 4
                string filename_4 = Path.GetFileName(pROPERTY.IMAGE_4.FileName);
                string _filename_4 = DateTime.Now.ToString("hhmmssfff") + filename_4;
                string path_4 = Path.Combine(Server.MapPath("~/Image/"), _filename_4);

                pROPERTY.IMG_4 = "~/Image/" + _filename_4;

                pROPERTY.ADDED_DATE = DateTime.Now;
                pROPERTY.ADDED_BY_ID = getUserID();
                db.PROPERTies.Add(pROPERTY);

                string error = "";
                if (pROPERTY.IMAGE_1.ContentLength > 1000000)
                {
                    error += "Image 1 must be less than or equal to 1 MB<br>";
                }

                if (pROPERTY.IMAGE_2.ContentLength > 1000000)
                {
                    error += "Image 2 must be less than or equal to 1 MB<br>";
                }

                if (pROPERTY.IMAGE_3.ContentLength > 1000000)
                {
                    error += "Image 3 must be less than or equal to 1 MB<br>";
                }

                if (pROPERTY.IMAGE_4.ContentLength > 1000000)
                {
                    error += "Image 4 must be less than or equal to 1 MB<br>";
                }

                if (String.IsNullOrEmpty(error))
                {
                    if (db.SaveChanges() > 0)
                    {
                        pROPERTY.IMAGE_1.SaveAs(path_1);
                        pROPERTY.IMAGE_2.SaveAs(path_2);
                        pROPERTY.IMAGE_3.SaveAs(path_3);
                        pROPERTY.IMAGE_4.SaveAs(path_4);
                    }
                    return RedirectToAction("MyProperties");
                }
                else
                {
                    ViewBag.msg = error;
                }
            }

            ViewBag.CITY_ID = new SelectList(db.CITies.Where(m => m.STATE_ID == pROPERTY.STATE_ID), "ID", "NAME", pROPERTY.CITY_ID);
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME", pROPERTY.COUNTRY_ID);
            ViewBag.ADDED_BY_ID = new SelectList(db.USERS, "ID", "EMAIL", pROPERTY.ADDED_BY_ID);
            ViewBag.PROPERTY_TYPE_ID = new SelectList(db.PROPERTY_TYPE, "ID", "NAME", pROPERTY.PROPERTY_TYPE_ID);
            ViewBag.STATE_ID = new SelectList(db.STATEs.Where(m => m.COUNTRY_ID == pROPERTY.COUNTRY_ID), "ID", "NAME", pROPERTY.STATE_ID);
            ViewBag.STATUS_ID = new SelectList(db.PROPERTY_STATUS, "ID", "STATUS", pROPERTY.STATUS_ID);
            return View(pROPERTY);
        }

        public ActionResult EditProperty(int? id)
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY pROPERTY = db.PROPERTies.Find(id);
            if (pROPERTY == null)
            {
                return HttpNotFound();
            }
            
            pROPERTY.POSTAL_CODE = pROPERTY.POSTAL_CODE.Trim();

            ViewBag.CITY_ID = new SelectList(db.CITies.Where(m => m.STATE_ID == pROPERTY.STATE_ID), "ID", "NAME", pROPERTY.CITY_ID);
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME", pROPERTY.COUNTRY_ID);
            ViewBag.ADDED_BY_ID = new SelectList(db.USERS, "ID", "EMAIL", pROPERTY.ADDED_BY_ID);
            ViewBag.PROPERTY_TYPE_ID = new SelectList(db.PROPERTY_TYPE, "ID", "NAME", pROPERTY.PROPERTY_TYPE_ID);
            ViewBag.STATE_ID = new SelectList(db.STATEs.Where(m => m.COUNTRY_ID == pROPERTY.COUNTRY_ID), "ID", "NAME", pROPERTY.STATE_ID);
            ViewBag.STATUS_ID = new SelectList(db.PROPERTY_STATUS, "ID", "STATUS", pROPERTY.STATUS_ID);
            return View(pROPERTY);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProperty([Bind(Include = "ID,NAME,DESCR,PROPERTY_TYPE_ID,STATUS_ID,LOCATION,BEDROOMS,BATHROOMS,FLOORS,GARAGES,AREA,PRICE,BEFORE_PRICE_LABEL,AFTER_PRICE_LABEL,FEATURES,IMG_1,IMG_2,IMG_3,IMG_4,IMAGE_1,IMAGE_2,IMAGE_3,IMAGE_4,ADDRESS,COUNTRY_ID,STATE_ID,CITY_ID,POSTAL_CODE,NEIGHBORHOOD,ADDED_BY_ID,ADDED_DATE")] PROPERTY pROPERTY)
        {

            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                string path_1 = "", path_2 = "", path_3 = "", path_4 = "";
                string error = "";

                if(pROPERTY.IMAGE_1 != null && pROPERTY.IMAGE_1.ContentLength > 0)
                {
                    // Image 1
                    string filename_1 = Path.GetFileName(pROPERTY.IMAGE_1.FileName);
                    string _filename_1 = DateTime.Now.ToString("hhmmssfff") + filename_1;
                    path_1 = Path.Combine(Server.MapPath("~/Image/"), _filename_1);

                    pROPERTY.IMG_1 = "~/Image/" + _filename_1;
                    
                    if (pROPERTY.IMAGE_1.ContentLength > 1000000)
                    {
                        error += "Image 1 must be less than or equal to 1 MB<br>";
                    }
                }

                if (pROPERTY.IMAGE_2 != null && pROPERTY.IMAGE_2.ContentLength > 0)
                {
                    // Image 2
                    string filename_2 = Path.GetFileName(pROPERTY.IMAGE_2.FileName);
                    string _filename_2 = DateTime.Now.ToString("hhmmssfff") + filename_2;
                    path_2 = Path.Combine(Server.MapPath("~/Image/"), _filename_2);

                    pROPERTY.IMG_2 = "~/Image/" + _filename_2;

                    if (pROPERTY.IMAGE_2.ContentLength > 1000000)
                    {
                        error += "Image 2 must be less than or equal to 1 MB<br>";
                    }
                }


                if (pROPERTY.IMAGE_3 != null && pROPERTY.IMAGE_3.ContentLength > 0)
                {
                    // Image 3
                    string filename_3 = Path.GetFileName(pROPERTY.IMAGE_3.FileName);
                    string _filename_3 = DateTime.Now.ToString("hhmmssfff") + filename_3;
                    path_3 = Path.Combine(Server.MapPath("~/Image/"), _filename_3);

                    pROPERTY.IMG_3 = "~/Image/" + _filename_3;

                    if (pROPERTY.IMAGE_3.ContentLength > 1000000)
                    {
                        error += "Image 3 must be less than or equal to 1 MB<br>";
                    }
                }

                if (pROPERTY.IMAGE_4 != null && pROPERTY.IMAGE_4.ContentLength > 0)
                {
                    // Image 4
                    string filename_4 = Path.GetFileName(pROPERTY.IMAGE_4.FileName);
                    string _filename_4 = DateTime.Now.ToString("hhmmssfff") + filename_4;
                    path_4 = Path.Combine(Server.MapPath("~/Image/"), _filename_4);

                    pROPERTY.IMG_4 = "~/Image/" + _filename_4;

                    if (pROPERTY.IMAGE_4.ContentLength > 1000000)
                    {
                        error += "Image 4 must be less than or equal to 1 MB<br>";
                    }
                }

                if (String.IsNullOrEmpty(error))
                {
                    db.Entry(pROPERTY).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        if (!String.IsNullOrEmpty(path_1)) pROPERTY.IMAGE_1.SaveAs(path_1);
                        if (!String.IsNullOrEmpty(path_2)) pROPERTY.IMAGE_2.SaveAs(path_2);
                        if (!String.IsNullOrEmpty(path_3)) pROPERTY.IMAGE_3.SaveAs(path_3);
                        if (!String.IsNullOrEmpty(path_4)) pROPERTY.IMAGE_4.SaveAs(path_4);
                    }
                    return RedirectToAction("MyProperties");
                }
                else
                {
                    ViewBag.msg = error;
                }
            }
            ViewBag.CITY_ID = new SelectList(db.CITies.Where(m => m.STATE_ID == pROPERTY.STATE_ID), "ID", "NAME", pROPERTY.CITY_ID);
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME", pROPERTY.COUNTRY_ID);
            ViewBag.ADDED_BY_ID = new SelectList(db.USERS, "ID", "EMAIL", pROPERTY.ADDED_BY_ID);
            ViewBag.PROPERTY_TYPE_ID = new SelectList(db.PROPERTY_TYPE, "ID", "NAME", pROPERTY.PROPERTY_TYPE_ID);
            ViewBag.STATE_ID = new SelectList(db.STATEs.Where(m => m.COUNTRY_ID == pROPERTY.COUNTRY_ID), "ID", "NAME", pROPERTY.STATE_ID);
            ViewBag.STATUS_ID = new SelectList(db.PROPERTY_STATUS, "ID", "STATUS", pROPERTY.STATUS_ID);
            return View(pROPERTY);
        }

        public ActionResult DetailsProperty(int? id)
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

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

        public ActionResult DeleteProperty(int? id)
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

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

        [HttpPost, ActionName("DeleteProperty")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePropertyConfirmed(int id)
        {
            PROPERTY pROPERTY = db.PROPERTies.Find(id);
            db.PROPERTies.Remove(pROPERTY);
            db.SaveChanges();
            return RedirectToAction("MyProperties");
        }

        #endregion Property

        #region Query
        public ActionResult ReceivedQuery()
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            int userId = getUserID();
            var pROPERTY_QUERY = db.PROPERTY_QUERY.Include(p => p.PROPERTY)
                                                  .Include(p => p.USER)
                                                  .Include(p => p.USER1)
                                                  .Where(p => p.A_DATE == null && p.ANSWER == null && p.VENDOR_ID == userId)
                                                  .OrderByDescending(p => p.Q_DATE); ;
            return View(pROPERTY_QUERY.ToList());
        }

        public ActionResult AnsweredQuery()
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            int userId = getUserID();
            var pROPERTY_QUERY = db.PROPERTY_QUERY.Include(p => p.PROPERTY)
                                                  .Include(p => p.USER)
                                                  .Include(p => p.USER1)
                                                  .Where(p => p.A_DATE != null && p.ANSWER != null && p.VENDOR_ID == userId)
                                                  .OrderByDescending(p => p.A_DATE);
            return View(pROPERTY_QUERY.ToList());
        }

        public ActionResult AnswerQuery(int? id)
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROPERTY_QUERY pROPERTY_QUERY = db.PROPERTY_QUERY.Find(id);
            if (pROPERTY_QUERY == null)
            {
                return HttpNotFound();
            }
            return View(pROPERTY_QUERY);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerQuery([Bind(Include = "ID,QUESTION,ANSWER,PROPERTY_ID,USER_ID,VENDOR_ID,Q_DATE")] PROPERTY_QUERY pROPERTY_QUERY)
        {
            if (!IsVendorLogin())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                pROPERTY_QUERY.A_DATE = DateTime.Now;
                db.Entry(pROPERTY_QUERY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ReceivedQuery");
            }
            return View(pROPERTY_QUERY);
        }

        #endregion Query

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