using Birthday.Properties.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Birthday.Web.Helpers
{
    public class JsonResultHelper
    {
        public static JsonResult JsonError(HttpResponseBase response, string message = null)
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult
            {
                Data = new { Message = message ?? GeneralResource.ErrorOccured },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}