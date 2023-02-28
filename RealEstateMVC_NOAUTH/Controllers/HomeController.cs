using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}