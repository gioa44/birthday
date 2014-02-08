using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Tools
{
    public static class ImageTool
    {
        public static byte[] ResizeImageFile(byte[] imageFile, int targetWidth, int targetHeight)
        {
            using (var original = new ImageMagick.MagickImage(imageFile))
            {
                #region Calculating width and height

                if (original.Height > original.Width)
                {
                    targetWidth = (int)Math.Ceiling(original.Width * ((float)targetHeight / (float)original.Height));
                }
                else
                {
                    targetHeight = (int)Math.Ceiling(original.Height * ((float)targetWidth / (float)original.Width));
                }

                #endregion

                original.Resize(targetWidth, targetHeight);

                return original.ToByteArray();
            }
        }

        public static byte[] CropImageFile(byte[] imageFile, Rectangle cropArea)
        {
            // Create a new blank canvas.  The resized image will be drawn on this canvas.
            using (Bitmap bmPhoto = new Bitmap(new MemoryStream(imageFile)))
            {
                using (var cropped = bmPhoto.Clone(cropArea, bmPhoto.PixelFormat))
                {
                    using (MemoryStream mm = new MemoryStream())
                    {
                        cropped.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return mm.GetBuffer();
                    }
                }
            }
        }

        public static byte[] AutoOrientImageFile(byte[] imageFile)
        {
            using (var original = new ImageMagick.MagickImage(imageFile))
            {
                original.AutoOrient();

                return original.ToByteArray();
            }
        }
    }
}
