using Birthday.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Domain.Services
{
    public class BirthdayImageService : DomainServiceBase<BirthdayImage>
    {
        public bool SaveImage(byte[] data, string mimeType, int birthdayID, int imageIndex, int userID, ref string errorMessage)
        {
            //Get TemplateID
            var templateID = new BirthdayService(_DbContext).GetTemplateID(birthdayID);
            int? widthFix = null;

            if (!new TemplateService(_DbContext).ValidateImage(templateID, imageIndex, data, ref widthFix, ref errorMessage))
            {
                return false;
            }

            var image = GetAll().FirstOrDefault(x => x.BirthdayID == birthdayID && x.ImageIndex == imageIndex);

            if (image == null)
            {
                image = new BirthdayImage
                {
                    BirthdayID = birthdayID,
                    ImageIndex = imageIndex,
                    ImageLeft = 0,
                    ImageTop = 0,
                    ImageWidth = 0,
                    File = new File
                    {
                        Name = string.Empty,
                        Content = ImageTool.AutoOrientImageFile(data),
                        ContentLength = data.Length,
                        CreateDate = DateTime.Now,
                        CreateUserID = userID,
                        MimeType = mimeType
                    }
                };

                Add(image);
            }
            else
            {
                if (image.File != null)
                {
                    _DbContext.Files.Remove(image.File);
                }

                image.ImageWidth = widthFix ?? image.ImageWidth;

                image.File = new File
                    {
                        Name = string.Empty,
                        Content = data,
                        ContentLength = data.Length,
                        CreateDate = DateTime.Now,
                        CreateUserID = userID,
                        MimeType = mimeType
                    };
            }

            SaveChanges();

            return image.FileID > 0;
        }

        public BirthdayImage GetImage(int birthdayID, int imageIndex)
        {
            var image = GetAll().FirstOrDefault(x => x.BirthdayID == birthdayID && x.ImageIndex == imageIndex);

            return image;
        }

        public void UpdateImageProps(int birthdayID, int imageIndex, int left, int top, int width)
        {
            var image = GetAll().FirstOrDefault(x => x.BirthdayID == birthdayID && x.ImageIndex == imageIndex);

            image.ImageLeft = left;
            image.ImageTop = top;
            image.ImageWidth = width;

            Update(image);

            SaveChanges();
        }
    }
}
