using System.Web;
using System.Web.Mvc;

namespace Homework_3_11_2019
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
