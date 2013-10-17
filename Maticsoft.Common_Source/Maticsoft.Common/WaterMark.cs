namespace Maticsoft.Common
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using System.Web;

    public class WaterMark
    {
        public static bool addWatermarkImage(string oldpath, string newpath, string WaterMarkPicPath, string _watermarkPosition = "WM_CENTER", int transparentValue = 30)
        {
            try
            {
                Image image = Image.FromFile(HttpContext.Current.Server.MapPath(oldpath));
                Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                Image image2 = new Bitmap(HttpContext.Current.Server.MapPath(WaterMarkPicPath));
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
                            goto Label_02FA;
                        }
                        if (str == "WM_BOTTOM_RIGHT")
                        {
                            goto Label_030E;
                        }
                        if (str == "WM_BOTTOM_LEFT")
                        {
                            goto Label_032C;
                        }
                        if (str == "WM_CENTER")
                        {
                            goto Label_0340;
                        }
                    }
                    else
                    {
                        x = 10;
                        y = 10;
                    }
                }
                goto Label_035E;
            Label_02FA:
                x = (image.Width - width) - 10;
                y = 10;
                goto Label_035E;
            Label_030E:
                x = (image.Width - width) - 10;
                y = (image.Height - height) - 10;
                goto Label_035E;
            Label_032C:
                x = 10;
                y = (image.Height - height) - 10;
                goto Label_035E;
            Label_0340:
                x = (image.Width / 2) - (width / 2);
                y = (image.Height / 2) - (height / 2);
            Label_035E:
                graphics.DrawImage(image2, new Rectangle(x, y, width, height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
                image2.Dispose();
                imageAttr.Dispose();
                bitmap.Save(HttpContext.Current.Server.MapPath(newpath));
                bitmap.Dispose();
                image.Dispose();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static bool addWatermarkText(string oldpath, string newpath, string _watermarkText, string _watermarkPosition = "WM_CENTER", string fontStyle = "arial", int fontSize = 14, string color = "#FFFFFF")
        {
            try
            {
                StringFormat format;
                Image image = Image.FromFile(HttpContext.Current.Server.MapPath(oldpath));
                Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
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
                            goto Label_0104;
                        }
                        if (str == "WM_BOTTOM_RIGHT")
                        {
                            goto Label_012D;
                        }
                        if (str == "WM_BOTTOM_LEFT")
                        {
                            goto Label_0163;
                        }
                        if (str == "WM_CENTER")
                        {
                            goto Label_018B;
                        }
                    }
                    else
                    {
                        x = ef.Width / 2f;
                        y = 8f;
                    }
                }
                goto Label_01B7;
            Label_0104:
                x = (image.Width * 1f) - (ef.Width / 2f);
                y = 8f;
                goto Label_01B7;
            Label_012D:
                x = (image.Width * 1f) - (ef.Width / 2f);
                y = (image.Height * 1f) - ef.Height;
                goto Label_01B7;
            Label_0163:
                x = ef.Width / 2f;
                y = (image.Height * 1f) - ef.Height;
                goto Label_01B7;
            Label_018B:
                x = image.Width * 0.5f;
                y = (image.Height * 0.5f) - (ef.Height / 2f);
            Label_01B7:
                format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                SolidBrush brush = new SolidBrush(Color.FromArgb(0x99, 0, 0, 0));
                graphics.DrawString(_watermarkText, font, brush, x + 1f, y + 1f, format);
                SolidBrush brush2 = new SolidBrush(Color.FromArgb(0x99, ColorTranslator.FromHtml(color)));
                graphics.DrawString(_watermarkText, font, brush2, x, y, format);
                brush.Dispose();
                brush2.Dispose();
                bitmap.Save(HttpContext.Current.Server.MapPath(newpath));
                bitmap.Dispose();
                image.Dispose();
                return true;
            }
            catch
            {
            }
            return false;
        }
    }
}

