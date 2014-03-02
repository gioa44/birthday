using Birthday.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Properties.Resources;

namespace Birthday.Domain.Services
{

    public class TemplateService : DomainServiceBase<Template>
    {
        public TemplateService() { }

        public TemplateService(BirthdayContext context)
            : base(context) { }

        public TemplateService(BirthdayContextContainer container)
            : base(container) { }

        public IEnumerable<TemplateImage> GetTemplateImages(int templateID)
        {
            return _DbContext.TemplateImages.Where(x => x.TemplateID == templateID);
        }

        public TemplateImage GetTemplateImage(int templateID, int imageIndex)
        {
            return _DbContext.TemplateImages.FirstOrDefault(x => x.TemplateID == templateID && x.ImageIndex == imageIndex);
        }

        public bool ValidateImage(int templateID, int imageIndex, byte[] imageData, ref int? widthFix, ref string errorMessage)
        {
            var imageInfo = new ImageTool.ImageInfo(imageData);

            var templateImage = GetTemplateImage(templateID, imageIndex);

            if (imageInfo.Width < templateImage.ImageWidth
                || imageInfo.Height < templateImage.ImageHeight)
            {
                errorMessage = string.Format(GeneralResource.ImageDimensionsWarning, templateImage.ImageWidth, templateImage.ImageHeight);
                return false;
            }
            else
            {
                var originalHeight = imageInfo.Height;
                var originalWidth = imageInfo.Width;
                var targetHeight = templateImage.ImageHeight;
                var targetWidth = templateImage.ImageWidth;

                var resultHeight = (int)Math.Ceiling(originalHeight * ((float)targetWidth / (float)originalWidth));

                if (resultHeight < targetHeight)
                {
                    widthFix = (int)Math.Ceiling(targetWidth * ((float)targetHeight / (float)resultHeight));

                    if (widthFix > originalWidth)
                    {
                        errorMessage = string.Format(GeneralResource.ImageDimensionsWarning, templateImage.ImageWidth, templateImage.ImageHeight);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
