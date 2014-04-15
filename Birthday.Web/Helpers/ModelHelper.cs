using Birthday.Domain.Services;
using Birthday.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Birthday.Web.Helpers
{
    public static class ModelHelper
    {
        public static List<ImageInfo> GetImageProps(int birthdayID, BirthdayService service)
        {
            var imageProps = new List<ImageInfo>();
            var images = service.GetBirthdayImages(birthdayID);

            if (images.Any())
            {
                images.ForEach(x => imageProps.Add(new ImageInfo
                {
                    Index = x.ImageIndex,
                    Left = x.ImageLeft,
                    Top = x.ImageTop,
                    Width = x.ImageWidth
                }));
            }

            return imageProps;
        }
    }
}