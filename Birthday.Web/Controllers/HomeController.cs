using Birthday.Domain.Services;
using Birthday.Properties.Resources;
using Birthday.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Birthday.Web.Controllers
{
    public class HomeController : BaseController
    {
        private static object lockObject = new object();
        private BirthdayService _BirthdayService = new BirthdayService();
        private int _BirthdayID = 9;
        private int _UserID = 3;

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
        public ActionResult Reserve(ReserveInfo info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lock (lockObject)
                    {
                        var vacantDays = GetVacantDays();

                        var day = vacantDays.FirstOrDefault(x => x.Date == info.Date);

                        if (day != null && !day.Reserved)
                        {
                            var id = _BirthdayService.ReserveBirthday(day.Date, info.Email);
                            if (id > 0)
                            {
                                return Json(new { Result = string.Format(GeneralResource.DaySuccessfullyReserved, day.Date) });
                            }
                        }
                        else
                        {
                            return Json(new { Result = string.Format(GeneralResource.AlreadyReserved, day.Date) });
                        }
                    }
                }
            }
            catch
            {

            }
            return JsonError();
        }

        public ActionResult Visualization()
        {
            var tmpl = _BirthdayService.GetTemplate(1);
            var birthday = _BirthdayService.Get(_BirthdayID);

            var model = new VisualizationViewModel { Html = birthday.Html ?? tmpl.Html };

            model.ImageProps = new List<ImageInfo>();

            using (var service = new BirthdayImageService())
            {
                service.GetImages(_BirthdayID).ForEach(x => model.ImageProps.Add(new ImageInfo
                {
                    Index = x.ImageIndex,
                    Left = x.ImageLeft,
                    Top = x.ImageTop,
                    Width = x.ImageWidth
                }));
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Visualization(VisualizationViewModel model)
        {
            //var tmpl = _BirthdayService.GetTemplate(1);

            //var model = new VisualizationViewModel { Html = tmpl.Html };

            using (var service = new BirthdayImageService())
            {
                foreach (var item in model.ImageProps)
                {
                    service.UpdateImageProps(_BirthdayID, item.Index, item.Left, item.Top, item.Width);
                }
            }

            return RedirectToAction("Visualization");
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
            return PartialView("_ReserveDays", GetVacantDays());
        }

        public ImageResult GetBirthdayImage(int imageIndex)
        {
            using (var service = new BirthdayImageService())
            {
                var image = service.GetImage(_BirthdayID, imageIndex);

                return new ImageResult(new MemoryStream(image.File.Content), image.File.MimeType);
            }
        }

        [HttpPost]
        public JsonResult ImageUpload(int imageIndex)
        {
            try
            {
                var file = Request.Files[0];

                var content = new byte[file.InputStream.Length];

                file.InputStream.Read(content, 0, content.Length);

                using (var service = new BirthdayImageService())
                {
                    var uploaded = service.SaveImage(content, file.ContentType, _BirthdayID, imageIndex, _UserID);
                    if (uploaded)
                    {
                        return Json(new { Result = "Ok" });
                    }
                    else
                    {
                        return JsonError();
                    }
                }
            }
            catch { }

            return JsonError();
        }

        private IEnumerable<Day> GetVacantDays()
        {
            var days = new List<Day>();
            var date = DateTime.Today.AddDays(1); //Begin from tomorrow
            var endDate = date.AddDays(6);

            var reservedDays = _BirthdayService.GetAll()
                .Where(x => x.EventDate >= date && x.EventDate <= endDate)
                .Select(x => x.EventDate);

            for (int i = 0; i < 7; i++)
            {
                date = date.AddDays(i);
                days.Add(new Day
                {
                    Date = date,
                    Reserved = reservedDays.Contains(date)
                });
            }

            return days;
        }

        protected override void Dispose(bool disposing)
        {
            _BirthdayService.Dispose();
            base.Dispose(disposing);
        }
    }
}
