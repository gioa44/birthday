using Birthday.Domain.Services;
using Birthday.Web.ActionFilters;
using Birthday.Web.Helpers;
using Birthday.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Birthday.Web.Controllers
{
    public class VisualizationController : BaseController
    {
        private BirthdayService _BirthdayService = new BirthdayService();

        public ActionResult VisualizationLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VisualizationLogin(VisualizationLogin model)
        {
            if (VisualizationLoginHelper.ValidateUser(model.Email, model.Password, Session))
            {
                SetVisualizationAccessCookie(model.Email, model.Password);

                return RedirectToAction("Visualization");
            }

            return View();
        }

        [AccessActionFilter]
        public ActionResult Visualization()
        {
            var birthday = _BirthdayService.Get(BirthdayID);

            var model = new VisualizationViewModel { Html = birthday.Html };

            var templateList = new TemplateService(_BirthdayService).GetAll().ToList();

            var selectList = new SelectList(templateList, "TemplateID", "Title");

            model.TemplateList = selectList;
            model.TemplateID = birthday.TemplateID;
            model.TemplateName = birthday.Template.Title;

            model.ImageProps = ModelHelper.GetImageProps(BirthdayID, _BirthdayService);

            return View(model);
        }

        [HttpPost, AccessActionFilter]
        public ActionResult SetTemplate(int templateID)
        {
            try
            {
                _BirthdayService.SetTemplate(BirthdayID, templateID);
            }
            catch (Exception)
            {
                return JsonError();
            }

            return Json(new { Result = "Ok" });
        }

        [HttpPost, AccessActionFilter]
        public ActionResult Visualization(VisualizationViewModel model)
        {
            using (var service = new BirthdayImageService())
            {
                foreach (var item in model.ImageProps)
                {
                    service.UpdateImageProps(BirthdayID, item.Index, item.Left, item.Top, item.Width);
                }
            }

            return RedirectToAction("Visualization");
        }

        [HttpPost, AccessActionFilter]
        public JsonResult ImageUpload(int imageIndex)
        {
            try
            {
                var file = Request.Files[0];

                var content = new byte[file.InputStream.Length];

                file.InputStream.Read(content, 0, content.Length);

                using (var service = new BirthdayImageService())
                {
                    string errorMessage = null;

                    var image = service.SaveImage(content, file.ContentType, BirthdayID, imageIndex, UserID, ref errorMessage);
                    if (image != null)
                    {
                        return Json(new { image.ImageLeft, image.ImageTop, image.ImageWidth });
                    }
                    else
                    {
                        return JsonError(errorMessage);
                    }
                }
            }
            catch { }

            return JsonError();
        }

        public ImageResult GetBirthdayImage(int imageIndex)
        {
            using (var service = new BirthdayImageService())
            {
                var image = service.GetImage(BirthdayID, imageIndex);

                if (image == null || image.File == null)
                {
                    return new ImageResult(Server.MapPath("~/content/images/dot.png"));
                }

                return new ImageResult(new MemoryStream(image.File.Content), image.File.MimeType);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _BirthdayService.Dispose();
            base.Dispose(disposing);
        }
    }
}
