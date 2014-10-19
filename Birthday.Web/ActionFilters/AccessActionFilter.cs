using Birthday.Domain.Services;
using Birthday.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Birthday.Web.ActionFilters
{
    public class AccessActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string email = null;
                string password = null;

                var cookie = filterContext.HttpContext.Request.Cookies["_Vis"];
                if (cookie != null)
                {
                    var val = cookie.Value.Split(new char[] { '|' });

                    email = val[0];
                    password = val[1];
                }

                if (email != null && password != null)
                {
                    if (VisualizationLoginHelper.ValidateUser(email, password, filterContext.HttpContext.Session))
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }
                }
            }
            catch { }
            
            filterContext.Result = new RedirectResult((filterContext.Controller as Controller).Url.Action("VisualizationLogin", "Visualization"));
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage2 = "Custom Action Filter: Message from OnActionExecuted method.";
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage3 = "Custom Action Filter: Message from OnResultExecuting method.";
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage4 = "Custom Action Filter: Message from OnResultExecuted method.";
        }
    }
}