﻿using System;
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
                //switch (original.Orientation)
                //{
                //    case ImageMagick.OrientationType.BottomLeft:
                //        original.Flip();
                //        break;
                //    case ImageMagick.OrientationType.BottomRight:
                //        original.Rotate(180);
                //        break;
                //    case ImageMagick.OrientationType.LeftBotom:
                //        original.Rotate(270);
                //        break;
                //    case ImageMagick.OrientationType.LeftTop:
                //        original.Rotate(90);
                //        original.Flop();
                //        break;
                //    case ImageMagick.OrientationType.RightBottom:
                //        original.Rotate(270);
                //        original.Flop();
                //        break;
                //    case ImageMagick.OrientationType.RightTop:
                //        original.Rotate(90);
                //        break;
                //    case ImageMagick.OrientationType.TopLeft:
                //        break;
                //    case ImageMagick.OrientationType.TopRight:
                //        original.Flop();
                //        break;
                //    default:
                //        break;
                //}
                original.AutoOrient();

                return original.ToByteArray();
            }
        }

        public class ImageInfo
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public ImageInfo(byte[] data)
            {
                var image = new ImageMagick.MagickImage(data);

                Width = image.Width;
                Height = image.Height;
            }
        }
    }
}
