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
        public static List<ImageInfo> GetImageProps(Birthday.Domain.Birthday birthday)
        {
            var imageProps = new List<ImageInfo>();
            var images = birthday.BirthdayImages.ToList();

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

        public static List<BirthdayText> GetBirthdayTexts(Birthday.Domain.Birthday birthday)
        {
            return birthday.BirthdayTexts
                .Select(x => new BirthdayText
                {
                    Index = x.TextIndex,
                    Text = x.Text
                })
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}