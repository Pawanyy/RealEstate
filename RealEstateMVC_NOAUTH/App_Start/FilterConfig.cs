using System.Web;
using System.Web.Mvc;

namespace RealEstateMVC_NOAUTH
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
