using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        RealEstateEntities db = new RealEstateEntities();

        private bool IsLogin()
        {
            return (Session.Count > 0 && Session["Type"].Equals("Vendor"));
        }

        public ActionResult Index()
        {
            if (!IsLogin())
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login", "Vendor");
        }

        // GET: User
        public ActionResult Login()
        {
            if (IsLogin())
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
                uSER.REGISTRATION_DATE = DateTime.Now;
                uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("Vendor")).Single().ID;
                db.USERS.Add(uSER);
                db.SaveChanges();

                ViewBag.msg = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                                      <strong>Success! </strong>Account Created
                                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                                    </div>";
                return View();
            }

            return View(uSER);
        }

        public ActionResult Dashboard()
        {
            //if (!IsLogin())
            //{
            //    return RedirectToAction("Login");
            //}

            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

        public ActionResult Profile()
        {
            if (!IsLogin())
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
            if (ModelState.IsValid)
            {
                uSER.ROLE_ID = db.USER_ROLE.Where(m => m.NAME.Equals("Vendor")).Single().ID;
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(uSER);
        }

        public ActionResult AddProperty()
        {
            ViewBag.CITY_ID = new SelectList(db.CITies, "ID", "NAME");
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME");
            ViewBag.ADDED_BY_ID = new SelectList(db.USERS, "ID", "EMAIL");
            ViewBag.PROPERTY_TYPE_ID = new SelectList(db.PROPERTY_TYPE, "ID", "NAME");
            ViewBag.STATE_ID = new SelectList(db.STATEs, "ID", "NAME");
            ViewBag.STATUS_ID = new SelectList(db.PROPERTY_STATUS, "ID", "STATUS");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProperty([Bind(Include = "NAME,DESCR,PROPERTY_TYPE_ID,STATUS_ID,LOCATION,BEDROOMS,BATHROOMS,FLOORS,GARAGES,AREA,PRICE,BEFORE_PRICE_LABEL,AFTER_PRICE_LABEL,FEATURES,IMAGE_1,IMAGE_2,IMAGE_3,IMAGE_4,ADDRESS,COUNTRY_ID,STATE_ID,CITY_ID,POSTAL_CODE,NEIGHBORHOOD")] PROPERTY pROPERTY)
        {
            if (ModelState.IsValid)
            {
                // Image 1
                string filename_1 = Path.GetFileName(pROPERTY.IMAGE_1.FileName);
                string _filename_1 = DateTime.Now.ToString("hhmmssfff") + filename_1;
                string path_1 = Path.Combine(Server.MapPath("~/Image/"), _filename_1);

                pROPERTY.IMG_1 = "~/Image" + _filename_1;

                // Image 2
                string filename_2 = Path.GetFileName(pROPERTY.IMAGE_2.FileName);
                string _filename_2 = DateTime.Now.ToString("hhmmssfff") + filename_2;
                string path_2 = Path.Combine(Server.MapPath("~/Image/"), _filename_2);

                pROPERTY.IMG_2 = "~/Image" + _filename_2;

                // Image 3
                string filename_3 = Path.GetFileName(pROPERTY.IMAGE_3.FileName);
                string _filename_3 = DateTime.Now.ToString("hhmmssfff") + filename_3;
                string path_3 = Path.Combine(Server.MapPath("~/Image/"), _filename_3);

                pROPERTY.IMG_3 = "~/Image" + _filename_3;

                // Image 4
                string filename_4 = Path.GetFileName(pROPERTY.IMAGE_4.FileName);
                string _filename_4 = DateTime.Now.ToString("hhmmssfff") + filename_4;
                string path_4 = Path.Combine(Server.MapPath("~/Image/"), _filename_4);

                pROPERTY.IMG_4 = "~/Image" + _filename_4;

                pROPERTY.ADDED_DATE = DateTime.Now;
                pROPERTY.ADDED_BY_ID = int.Parse(Session["userId"].ToString());
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
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = error;
                }
            }

            ViewBag.CITY_ID = new SelectList(db.CITies, "ID", "NAME", pROPERTY.CITY_ID);
            ViewBag.COUNTRY_ID = new SelectList(db.COUNTRies, "ID", "NAME", pROPERTY.COUNTRY_ID);
            ViewBag.ADDED_BY_ID = new SelectList(db.USERS, "ID", "EMAIL", pROPERTY.ADDED_BY_ID);
            ViewBag.PROPERTY_TYPE_ID = new SelectList(db.PROPERTY_TYPE, "ID", "NAME", pROPERTY.PROPERTY_TYPE_ID);
            ViewBag.STATE_ID = new SelectList(db.STATEs, "ID", "NAME", pROPERTY.STATE_ID);
            ViewBag.STATUS_ID = new SelectList(db.PROPERTY_STATUS, "ID", "STATUS", pROPERTY.STATUS_ID);
            return View(pROPERTY);
        }

        public ActionResult MyProperties() {
            var pROPERTies = db.PROPERTies.Include(p => p.CITY).Include(p => p.COUNTRY).Include(p => p.USER).Include(p => p.PROPERTY_TYPE).Include(p => p.STATE).Include(p => p.PROPERTY_STATUS);
            return View(pROPERTies.ToList());
        }
    }

}