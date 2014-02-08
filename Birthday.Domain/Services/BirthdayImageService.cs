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
        public bool SaveImage(byte[] content, string mimeType, int birthdayID, int imageIndex, int userID)
        {
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
                        Content = ImageTool.AutoOrientImageFile(content),
                        ContentLength = content.Length,
                        CreateDate = DateTime.Now,
                        CreateUserID = userID,
                        MimeType = mimeType
                    }
                };

                Add(image);
            }
            else
            {
                _DbContext.Files.Remove(image.File);
                image.File = new File
                    {
                        Name = string.Empty,
                        Content = content,
                        ContentLength = content.Length,
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

        public List<BirthdayImage> GetImages(int birthdayID)
        {
            return GetAll().Where(x => x.BirthdayID == birthdayID).ToList();
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
