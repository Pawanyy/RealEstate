using RealEstateMVC_NOAUTH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers
{
    public class RemoteController : Controller
    {
        RealEstateEntities db = new RealEstateEntities();
        // GET: Remote
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult IsUserEmailExists(string UserEmail)
        {
            //check if any of the UserEmail matches the UserEmail specified in the Parameter using the ANY extension method.  
            return Json(!db.USERS.Any(x => x.EMAIL== UserEmail), JsonRequestBehavior.AllowGet);
        }
    }
}