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
                url: "{controller}/BirthdayImage/{imageIndex}",
                defaults: new
                {
                    controller = "Home",
                    action = "GetBirthdayImage",
                    imageid = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "R0",
                url: "{controller}/{action}/{lang}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    lang = UrlParameter.Optional
                }
            );
        }
    }
}