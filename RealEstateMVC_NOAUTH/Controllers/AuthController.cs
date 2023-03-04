using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH.Controllers
{
    public class AuthController : Controller
    {
        public bool IsUserLogin()
        {
            return (Session.Count > 0 && Session["Type"].Equals("User"));
        }

        public bool IsVendorLogin()
        {
            return (Session.Count > 0 && Session["Type"].Equals("Vendor"));
        }

        public bool IsAdminLogin()
        {
            return (Session.Count > 0 && Session["Type"].Equals("Admin"));
        }

        public int getUserID()
        {
            return int.Parse(Session["userId"].ToString());
        }
    }
}