using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Birthday.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathinfo}");

            routes.MapRoute(
                name: "ImageRoute",
                url: "{controller}/Image/{imageid}",
                defaults: new
                {
                    controller = "home",
                    action = "file",
                    imageid = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "R0",
                url: "{controller}/{action}/{lang}",
                defaults: new
                {
                    controller = "home",
                    action = "index",
                    lang = UrlParameter.Optional
                }
            );
        }
    }
}