namespace Maticsoft.Web.Handlers.Tools
{
    using Maticsoft.Common;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using ZXing;
    using ZXing.Common;
    using ZXing.QrCode;
    using ZXing.QrCode.Internal;

    public class QRHandler : IHttpHandler
    {
        private static void GenQRCode(HttpContext context)
        {
            ErrorCorrectionLevel m;
            ImageFormat png;
            QrCodeEncodingOptions options2;
            string str5;
            string str = context.Request.QueryString["content"];
            if (string.IsNullOrWhiteSpace(str))
            {
                return;
            }
            str = Globals.UrlDecode(str);
            string str2 = context.Request.QueryString["level"];
            str2 = string.IsNullOrWhiteSpace(str2) ? "M" : str2.ToUpper();
            string str3 = context.Request.QueryString["format"];
            str3 = string.IsNullOrWhiteSpace(str3) ? "png" : str3.ToLower();
            int num = Globals.SafeInt(context.Request.QueryString["margin"], 4);
            int num2 = Globals.SafeInt(context.Request.QueryString["size"], 100);
            BarcodeFormat format2 = Globals.SafeEnum<BarcodeFormat>(context.Request.QueryString["mod"], BarcodeFormat.QR_CODE);
            string str4 = str2;
            if (str4 != null)
            {
                if (!(str4 == "L"))
                {
                    if (str4 == "M")
                    {
                        m = ErrorCorrectionLevel.M;
                        goto Label_0144;
                    }
                    if (str4 == "Q")
                    {
                        m = ErrorCorrectionLevel.Q;
                        goto Label_0144;
                    }
                    if (str4 == "H")
                    {
                        m = ErrorCorrectionLevel.H;
                        goto Label_0144;
                    }
                }
                else
                {
                    m = ErrorCorrectionLevel.L;
                    goto Label_0144;
                }
            }
            m = ErrorCorrectionLevel.M;
        Label_0144:
            if ((str5 = str3) != null)
            {
                if (!(str5 == "jpeg"))
                {
                    if (str5 == "gif")
                    {
                        png = ImageFormat.Gif;
                        goto Label_0198;
                    }
                    if (str5 == "bmp")
                    {
                        png = ImageFormat.Bmp;
                        goto Label_0198;
                    }
                }
                else
                {
                    png = ImageFormat.Jpeg;
                    goto Label_0198;
                }
            }
            png = ImageFormat.Png;
        Label_0198:
            options2 = new QrCodeEncodingOptions();
            options2.DisableECI = true;
            options2.CharacterSet = "UTF-8";
            options2.Width = num2;
            options2.Height = num2;
            options2.Margin = num;
            options2.ErrorCorrection = m;
            EncodingOptions options = options2;
            BarcodeWriter writer = new BarcodeWriter {
                Format = format2,
                Options = options
            };
            context.Response.Clear();
            context.Response.ContentType = png.GetMimeType();
            using (Bitmap bitmap = writer.Write(str))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, png);
                    stream.WriteTo(context.Response.OutputStream);
                }
            }
            context.Response.Output.Flush();
            context.Response.End();
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "GET")
            {
                string str = context.Request.QueryString["action"];
                GenQRCode(context);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

