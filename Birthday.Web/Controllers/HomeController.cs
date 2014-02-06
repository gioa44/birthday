using Birthday.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Birthday.Web.Controllers
{
    public class HomeController : BaseController
    {
        private static IEnumerable<Day> Days;
        private static object lockObject = new object();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reserve(FormCollection col)
        {
            lock (lockObject)
            {
                var day = Days.FirstOrDefault(x => x.Date.ToString("yyyyMMdd") == col["day"]);

                if (day != null && !day.Reserved)
                {
                    day.Reserved = true;
                    //Do reservation

                    return RedirectToAction("Reserve");
                }
                else
                {
                    ViewBag.ErrorMessage = day.Date.ToString("dd MMMM") + " already reserved!";
                }
            }

            return View();
        }

        public ActionResult Visualization()
        {
            return View();
        }

        public ActionResult AnniversaryHistory()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public PartialViewResult ReserveDays()
        {
            if (Days == null)
            {
                Days = GetDays();
            }

            return PartialView("_ReserveDays", Days);
        }

        public ImageResult File(int imageId)
        {
            return new ImageResult("/App_Data/image.jpg");
        }

        [HttpPost]
        public JsonResult ImageUpload()
        {
            var fileContent = Request.Files[0].InputStream;

            using (var file = System.IO.File.Open(Server.MapPath("~/App_Data/image.jpg"), System.IO.FileMode.OpenOrCreate))
            {
                var arr = new byte[fileContent.Length];

                fileContent.Read(arr, 0, arr.Length);

                file.Write(arr, 0, arr.Length);
            }

            return Json(new { Result = "Ok" });
        }

        private static IEnumerable<Day> GetDays()
        {
            var date = DateTime.Today;

            return new List<Day>{
                new Day { Date = date.AddDays(1), Reserved = true },
                new Day { Date = date.AddDays(2), Reserved = false },
                new Day { Date = date.AddDays(3), Reserved = false },
                new Day { Date = date.AddDays(4), Reserved = true },
                new Day { Date = date.AddDays(5), Reserved = false },
                new Day { Date = date.AddDays(6), Reserved = false },
                new Day { Date = date.AddDays(7), Reserved = false }
            };
        }
    }
}
