namespace Maticsoft.Common
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Web;

    public class ImageTools
    {
        public static void addWatermarkImage(string oldpath, string newpath, string WaterMarkPicPath, string _watermarkPosition = "WM_CENTER", int transparentValue = 30, InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2, PixelFormat pixelFormat = 0x21808)
        {
            Image image = Image.FromFile(oldpath);
            Bitmap bitmap = new Bitmap(image.Width, image.Height, pixelFormat);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.SmoothingMode = smoothingMode;
            graphics.InterpolationMode = interpolationMode;
            graphics.DrawImage(image, 0, 0, image.Width, image.Height);
            Image image2 = new Bitmap(WaterMarkPicPath);
            ImageAttributes imageAttr = new ImageAttributes();
            ColorMap map = new ColorMap {
                OldColor = Color.FromArgb(0xff, 0, 0xff, 0),
                NewColor = Color.FromArgb(0, 0, 0, 0)
            };
            ColorMap[] mapArray = new ColorMap[] { map };
            imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
            float num = ((float) transparentValue) / 100f;
            float[][] numArray2 = new float[5][];
            float[] numArray3 = new float[5];
            numArray3[0] = 1f;
            numArray2[0] = numArray3;
            float[] numArray4 = new float[5];
            numArray4[1] = 1f;
            numArray2[1] = numArray4;
            float[] numArray5 = new float[5];
            numArray5[2] = 1f;
            numArray2[2] = numArray5;
            float[] numArray6 = new float[5];
            numArray6[3] = num;
            numArray2[3] = numArray6;
            float[] numArray7 = new float[5];
            numArray7[4] = 1f;
            numArray2[4] = numArray7;
            float[][] newColorMatrix = numArray2;
            ColorMatrix matrix = new ColorMatrix(newColorMatrix);
            imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            double num6 = 1.0;
            if ((image.Width > (image2.Width * 4)) && (image.Height > (image2.Height * 4)))
            {
                num6 = 1.0;
            }
            else if ((image.Width > (image2.Width * 4)) && (image.Height < (image2.Height * 4)))
            {
                num6 = Convert.ToDouble((int) (image.Height / 4)) / Convert.ToDouble(image2.Height);
            }
            else if ((image.Width < (image2.Width * 4)) && (image.Height > (image2.Height * 4)))
            {
                num6 = Convert.ToDouble((int) (image.Width / 4)) / Convert.ToDouble(image2.Width);
            }
            else if ((image.Width * image2.Height) > (image.Height * image2.Width))
            {
                num6 = Convert.ToDouble((int) (image.Height / 4)) / Convert.ToDouble(image2.Height);
            }
            else
            {
                num6 = Convert.ToDouble((int) (image.Width / 4)) / Convert.ToDouble(image2.Width);
            }
            width = Convert.ToInt32((double) (image2.Width * num6));
            height = Convert.ToInt32((double) (image2.Height * num6));
            string str = _watermarkPosition;
            if (str != null)
            {
                if (!(str == "WM_TOP_LEFT"))
                {
                    if (str == "WM_TOP_RIGHT")
                    {
                        x = (image.Width - width) - 10;
                        y = 10;
                    }
                    else if (str == "WM_BOTTOM_RIGHT")
                    {
                        x = (image.Width - width) - 10;
                        y = (image.Height - height) - 10;
                    }
                    else if (str == "WM_BOTTOM_LEFT")
                    {
                        x = 10;
                        y = (image.Height - height) - 10;
                    }
                    else if (str == "WM_CENTER")
                    {
                        x = (image.Width / 2) - (width / 2);
                        y = (image.Height / 2) - (height / 2);
                    }
                }
                else
                {
                    x = 10;
                    y = 10;
                }
            }
            graphics.DrawImage(image2, new Rectangle(x, y, width, height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
            image2.Dispose();
            imageAttr.Dispose();
            bitmap.Save(newpath, ImageFormat.Jpeg);
            bitmap.Dispose();
            image.Dispose();
        }

        public static void addWatermarkText(string oldpath, string newpath, string _watermarkText, string _watermarkPosition = "WM_CENTER", string fontStyle = "arial", int fontSize = 14, string color = "#FFFFFF", InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2, PixelFormat pixelFormat = 0x21808, int alpha = 0x9c)
        {
            Image image = Image.FromFile(oldpath);
            Bitmap bitmap = new Bitmap(image.Width, image.Height, pixelFormat);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.SmoothingMode = smoothingMode;
            graphics.InterpolationMode = interpolationMode;
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            Font font = null;
            SizeF ef = new SizeF();
            font = new Font(fontStyle, (float) fontSize);
            ef = graphics.MeasureString(_watermarkText, font);
            float x = 0f;
            float y = 0f;
            string str = _watermarkPosition;
            if (str != null)
            {
                if (!(str == "WM_TOP_LEFT"))
                {
                    if (str == "WM_TOP_RIGHT")
                    {
                        x = (image.Width * 1f) - (ef.Width / 2f);
                        y = 8f;
                    }
                    else if (str == "WM_BOTTOM_RIGHT")
                    {
                        y = image.Height - ef.Height;
                        x = image.Width - ef.Width;
                    }
                    else if (str == "WM_BOTTOM_LEFT")
                    {
                        x = ef.Width / 2f;
                        y = (image.Height * 1f) - ef.Height;
                    }
                    else if (str == "WM_CENTER")
                    {
                        x = image.Width * 0.5f;
                        y = (image.Height * 0.5f) - (ef.Height / 2f);
                    }
                }
                else
                {
                    x = ef.Width / 2f;
                    y = 8f;
                }
            }
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center
            };
            SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
            graphics.DrawString(_watermarkText, font, brush, x + 1f, y + 1f, format);
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(alpha, ColorTranslator.FromHtml(color)));
            graphics.DrawString(_watermarkText, font, brush2, x, y, format);
            brush.Dispose();
            brush2.Dispose();
            bitmap.Save(newpath, ImageFormat.Jpeg);
            bitmap.Dispose();
            image.Dispose();
        }

        public static Bitmap GetThumbnail(Image originalImage, int width, int height, MakeThumbnailMode mode, out Graphics graphics, InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2)
        {
            Bitmap bitmap;
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = originalImage.Width;
            int num6 = originalImage.Height;
            if (mode == MakeThumbnailMode.Auto)
            {
                if (num > num2)
                {
                    mode = MakeThumbnailMode.W;
                }
                else
                {
                    mode = MakeThumbnailMode.H;
                }
            }
            if ((num5 < num) && (num6 < num2))
            {
                num = num5;
                num2 = num6;
            }
            else
            {
                switch (mode)
                {
                    case MakeThumbnailMode.W:
                        num2 = (originalImage.Height * width) / originalImage.Width;
                        goto Label_00DE;

                    case MakeThumbnailMode.H:
                        num = (originalImage.Width * height) / originalImage.Height;
                        goto Label_00DE;

                    case MakeThumbnailMode.HW:
                        goto Label_00DE;

                    case MakeThumbnailMode.Cut:
                        if ((((double) originalImage.Width) / ((double) originalImage.Height)) <= (((double) num) / ((double) num2)))
                        {
                            num5 = originalImage.Width;
                            num6 = (originalImage.Width * height) / num;
                            x = 0;
                            y = (originalImage.Height - num6) / 2;
                        }
                        else
                        {
                            num6 = originalImage.Height;
                            num5 = (originalImage.Height * num) / num2;
                            y = 0;
                            x = (originalImage.Width - num5) / 2;
                        }
                        goto Label_00DE;
                }
            }
        Label_00DE:
            bitmap = new Bitmap(num, num2);
            bitmap.MakeTransparent(Color.Transparent);
            graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = interpolationMode;
            graphics.SmoothingMode = smoothingMode;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(originalImage, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            return bitmap;
        }

        public static void MakeImageWaterThumbnail(string originalImagePath, string thumbnailPath, int width, int height, MakeThumbnailMode mode, string waterMarkPicPath, string watermarkPosition = "WM_CENTER", int transparentValue = 30, InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2)
        {
            MakeImageWaterThumbnail(originalImagePath, thumbnailPath, width, height, mode, ImageFormat.Png, waterMarkPicPath, watermarkPosition, transparentValue, interpolationMode, smoothingMode);
        }

        public static void MakeImageWaterThumbnail(string originalImagePath, string thumbnailPath, int width, int height, MakeThumbnailMode mode, ImageFormat imageFormat, string waterMarkPicPath, string watermarkPosition = "WM_CENTER", int transparentValue = 30, InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2)
        {
            using (Image image = Image.FromFile(originalImagePath))
            {
                Graphics graphics;
                Bitmap bitmap = GetThumbnail(image, width, height, mode, out graphics, interpolationMode, smoothingMode);
                Image image2 = new Bitmap(waterMarkPicPath);
                ImageAttributes imageAttr = new ImageAttributes();
                ColorMap map = new ColorMap {
                    OldColor = Color.FromArgb(0xff, 0, 0xff, 0),
                    NewColor = Color.FromArgb(0, 0, 0, 0)
                };
                ColorMap[] mapArray = new ColorMap[] { map };
                imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
                float num = ((float) transparentValue) / 100f;
                float[][] numArray2 = new float[5][];
                float[] numArray3 = new float[5];
                numArray3[0] = 1f;
                numArray2[0] = numArray3;
                float[] numArray4 = new float[5];
                numArray4[1] = 1f;
                numArray2[1] = numArray4;
                float[] numArray5 = new float[5];
                numArray5[2] = 1f;
                numArray2[2] = numArray5;
                float[] numArray6 = new float[5];
                numArray6[3] = num;
                numArray2[3] = numArray6;
                float[] numArray7 = new float[5];
                numArray7[4] = 1f;
                numArray2[4] = numArray7;
                float[][] newColorMatrix = numArray2;
                ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int x = 0;
                int y = 0;
                int num4 = 0;
                int num5 = 0;
                double num6 = 1.0;
                if ((bitmap.Width > (image2.Width * 4)) && (bitmap.Height > (image2.Height * 4)))
                {
                    num6 = 1.0;
                }
                else if ((bitmap.Width > (image2.Width * 4)) && (bitmap.Height < (image2.Height * 4)))
                {
                    num6 = Convert.ToDouble((int) (bitmap.Height / 4)) / Convert.ToDouble(image2.Height);
                }
                else if ((bitmap.Width < (image2.Width * 4)) && (bitmap.Height > (image2.Height * 4)))
                {
                    num6 = Convert.ToDouble((int) (bitmap.Width / 4)) / Convert.ToDouble(image2.Width);
                }
                else if ((bitmap.Width * image2.Height) > (bitmap.Height * image2.Width))
                {
                    num6 = Convert.ToDouble((int) (bitmap.Height / 4)) / Convert.ToDouble(image2.Height);
                }
                else
                {
                    num6 = Convert.ToDouble((int) (bitmap.Width / 4)) / Convert.ToDouble(image2.Width);
                }
                num4 = Convert.ToInt32((double) (image2.Width * num6));
                num5 = Convert.ToInt32((double) (image2.Height * num6));
                string str = watermarkPosition;
                if (str != null)
                {
                    if (!(str == "WM_TOP_LEFT"))
                    {
                        if (str == "WM_TOP_RIGHT")
                        {
                            goto Label_02A3;
                        }
                        if (str == "WM_BOTTOM_RIGHT")
                        {
                            goto Label_02B7;
                        }
                        if (str == "WM_BOTTOM_LEFT")
                        {
                            goto Label_02D5;
                        }
                        if (str == "WM_CENTER")
                        {
                            goto Label_02E9;
                        }
                    }
                    else
                    {
                        x = 10;
                        y = 10;
                    }
                }
                goto Label_0307;
            Label_02A3:
                x = (bitmap.Width - num4) - 10;
                y = 10;
                goto Label_0307;
            Label_02B7:
                x = (bitmap.Width - num4) - 10;
                y = (bitmap.Height - num5) - 10;
                goto Label_0307;
            Label_02D5:
                x = 10;
                y = (bitmap.Height - num5) - 10;
                goto Label_0307;
            Label_02E9:
                x = (bitmap.Width / 2) - (num4 / 2);
                y = (bitmap.Height / 2) - (num5 / 2);
            Label_0307:
                graphics.DrawImage(image2, new Rectangle(x, y, num4, num5), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
                try
                {
                    bitmap.Save(thumbnailPath, imageFormat);
                }
                finally
                {
                    image2.Dispose();
                    imageAttr.Dispose();
                    bitmap.Dispose();
                    graphics.Dispose();
                }
            }
        }

        public static void MakeTextWaterThumbnail(string originalImagePath, string thumbnailPath, int width, int height, MakeThumbnailMode mode, string watermarkText, string watermarkPosition = "WM_CENTER", string fontFamily = "arial", int fontSize = 14, string fontColor = "#FFFFFF", InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2)
        {
            MakeTextWaterThumbnail(originalImagePath, thumbnailPath, width, height, mode, ImageFormat.Png, watermarkText, watermarkPosition, fontFamily, fontSize, fontColor, interpolationMode, smoothingMode);
        }

        public static void MakeTextWaterThumbnail(string originalImagePath, string thumbnailPath, int width, int height, MakeThumbnailMode mode, ImageFormat imageFormat, string watermarkText, string watermarkPosition = "WM_CENTER", string fontFamily = "arial", int fontSize = 14, string fontColor = "#FFFFFF", InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2)
        {
            string path = "/Upload/Temp/" + new Guid() + ".jpg";
            addWatermarkText(originalImagePath, HttpContext.Current.Server.MapPath(path), watermarkText, watermarkPosition, fontFamily, fontSize, fontColor, interpolationMode, smoothingMode, PixelFormat.Format24bppRgb, 0x9c);
            MakeThumbnail(HttpContext.Current.Server.MapPath(path), thumbnailPath, width, height, mode, InterpolationMode.High, SmoothingMode.HighQuality);
        }

        [Obsolete]
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            MakeThumbnailMode none = MakeThumbnailMode.None;
            string str = mode;
            if (str != null)
            {
                if (!(str == "HW"))
                {
                    if (str == "W")
                    {
                        none = MakeThumbnailMode.W;
                    }
                    else if (str == "H")
                    {
                        none = MakeThumbnailMode.H;
                    }
                    else if (str == "Cut")
                    {
                        none = MakeThumbnailMode.Cut;
                    }
                }
                else
                {
                    none = MakeThumbnailMode.HW;
                }
            }
            MakeThumbnail(originalImagePath, thumbnailPath, width, height, none, InterpolationMode.High, SmoothingMode.HighQuality);
        }

        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, MakeThumbnailMode mode, InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2)
        {
            MakeThumbnail(originalImagePath, thumbnailPath, width, height, mode, ImageFormat.Png, interpolationMode, smoothingMode);
        }

        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, MakeThumbnailMode mode, ImageFormat imageFormat, InterpolationMode interpolationMode = 2, SmoothingMode smoothingMode = 2)
        {
            using (Image image = Image.FromFile(originalImagePath))
            {
                Graphics graphics;
                Bitmap bitmap = GetThumbnail(image, width, height, mode, out graphics, interpolationMode, smoothingMode);
                try
                {
                    bitmap.Save(thumbnailPath, imageFormat);
                }
                finally
                {
                    bitmap.Dispose();
                    graphics.Dispose();
                }
            }
        }
    }
}

