using System.Web;
using System.Web.Mvc;

namespace JCold_UVU_MVC_Inventory
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
