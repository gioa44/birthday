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

using Birthday.Tools;
using Birthday.Web.ActionFilters;
using Birthday.Web.Helpers;
namespace Birthday.Web.Controllers
{
    public class HomeController : BaseController
    {
        private static object lockObject = new object();
        private BirthdayService _BirthdayService = new BirthdayService();

        public ActionResult Index()
        {
            var birthday = _BirthdayService.GetCurrentBirthday();

            var model = new CurrentBirthday();

            if(birthday != null)
            {
                model.TemplateName = birthday.Template.Title;
                model.Html = birthday.Html;
            }

            model.ImageProps = ModelHelper.GetImageProps(BirthdayID, _BirthdayService);

            return View(model);
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
                            string pwd = null;
                            var id = _BirthdayService.ReserveBirthday(day.Date, info.Email, ref pwd);
                            if (id > 0)
                            {
                                MailSender.SendMail(info.Email, GeneralResource.BirthdayReservation, string.Format(GeneralResource.BirthdayReservationMailBody, day.Date, info.Email, pwd));
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
                var image = service.GetCurrentImage(imageIndex);

                if (image == null || image.File == null)
                {
                    return new ImageResult(Server.MapPath("~/content/images/dot.png"));
                }

                return new ImageResult(new MemoryStream(image.File.Content), image.File.MimeType);
            }
        }

        private IEnumerable<Day> GetVacantDays()
        {
            var days = new List<Day>();
            var date = DateTime.Today.AddDays(1); //Begin from tomorrow
            var endDate = date.AddDays(6);

            var reservedDays = _BirthdayService.GetAll()
                .Where(x => x.EventDate >= date && x.EventDate <= endDate)
                .Select(x => x.EventDate);

            for (; date <= endDate; date = date.AddDays(1))
            {
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
