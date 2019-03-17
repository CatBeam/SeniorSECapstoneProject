using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SaveNScore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // custom routes go before the default route
            routes.MapMvcAttributeRoutes();

            //alternate routing method (instead of attribute route)
            //routes.MapRoute(
            //    "TransactionsByDate",
            //    "Transactions/ByTransDate/{year}/{month}/{day}",
            //    new { controller = "Transactions", action = "ByTransDate"}
            //    new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
