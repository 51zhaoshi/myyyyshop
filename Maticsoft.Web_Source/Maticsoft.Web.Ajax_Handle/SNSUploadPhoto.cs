namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using System.Web.SessionState;

    public class SNSUploadPhoto : IHttpHandler, IRequiresSessionState
    {
        public static string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files[0];
            string uploadpath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            int contentLength = file.ContentLength;
            string valueByCache = ConfigSystem.GetValueByCache("SNSPhotoSizes");
            int num2 = 0x9c4000;
            if (string.IsNullOrEmpty(valueByCache))
            {
                num2 = Globals.SafeInt(valueByCache, 0x9c4000);
            }
            string s = "-1";
            if (contentLength <= num2)
            {
                byte[] buffer = new byte[contentLength];
                file.InputStream.Read(buffer, 0, contentLength);
                string ext = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                if (ConfigSystem.GetValueByCache("SNS_ImageStoreWay") != "1")
                {
                    s = UploadImage(buffer, uploadpath, ext, context);
                }
                else
                {
                    string imageUrl = "";
                    string str7 = "";
                    string str8 = "";
                    string fileName = CreateIDCode() + "." + ext;
                    if (UpYunManager.UploadExecute(buffer, fileName, ApplicationKeyType.SNS, out imageUrl))
                    {
                        s = imageUrl + "|" + str7 + "|" + str8;
                    }
                }
            }
            context.Response.Write(s);
        }

        public static string UploadImage(byte[] imgBuffer, string uploadpath, string ext, HttpContext context)
        {
            MemoryStream stream = new MemoryStream(imgBuffer);
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(uploadpath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(uploadpath));
            }
            string str = CreateIDCode() + "." + ext;
            if (context.Request["UserID"] != null)
            {
                str = "B" + context.Request["UserID"] + "." + ext;
            }
            string filename = HttpContext.Current.Server.MapPath(uploadpath) + str;
            Image image = Image.FromStream(stream);
            if (ext == "jpg")
            {
                image.Save(filename, ImageFormat.Jpeg);
            }
            if (ext == "png")
            {
                image.Save(filename, ImageFormat.Png);
            }
            if (ext == "gif")
            {
                image.Save(filename, ImageFormat.Gif);
            }
            if ((ext != null) && (ext.ToLower() == "jpeg"))
            {
                image.Save(filename, ImageFormat.Jpeg);
            }
            stream.Close();
            string str3 = "";
            string str4 = "";
            if (context.Request["Type"] != null)
            {
                str3 = "S_" + str;
                string thumbnailPath = HttpContext.Current.Server.MapPath(uploadpath + str3);
                string valueByCache = ConfigSystem.GetValueByCache("SNS_SmallPhotoSize");
                int width = 200;
                int height = 200;
                if (!string.IsNullOrEmpty(valueByCache))
                {
                    string[] strArray = valueByCache.Split(new char[] { 'X' });
                    if (strArray.Length == 1)
                    {
                        int num3 = Globals.SafeInt(valueByCache, 200);
                        width = num3;
                        height = num3;
                    }
                    else if (strArray.Length == 2)
                    {
                        width = Globals.SafeInt(strArray[0], 200);
                        height = Globals.SafeInt(strArray[1], 200);
                    }
                }
                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + str), thumbnailPath, width, height, MakeThumbnailMode.W, InterpolationMode.High, SmoothingMode.HighQuality);
                str4 = "B_" + str;
                string str7 = HttpContext.Current.Server.MapPath(uploadpath + str4);
                string str8 = ConfigSystem.GetValueByCache("SNS_BigPhotoSize");
                int num4 = 800;
                int num5 = 800;
                if (!string.IsNullOrEmpty(str8))
                {
                    string[] strArray2 = str8.Split(new char[] { 'X' });
                    if (strArray2.Length == 1)
                    {
                        int num6 = Globals.SafeInt(str8, 800);
                        num4 = num6;
                        num5 = num6;
                    }
                    else if (strArray2.Length == 2)
                    {
                        num4 = Globals.SafeInt(strArray2[0], 800);
                        num5 = Globals.SafeInt(strArray2[1], 800);
                    }
                }
                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + str), str7, num4, num5, MakeThumbnailMode.W, InterpolationMode.High, SmoothingMode.HighQuality);
            }
            if (context.Request["UserID"] != null)
            {
                string str9 = context.Request["UserID"] + "." + ext;
                string str10 = HttpContext.Current.Server.MapPath(uploadpath + str9);
                ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(uploadpath + str), str10, 100, 100, MakeThumbnailMode.Auto, InterpolationMode.High, SmoothingMode.HighQuality);
                context.Session["Gravatar"] = context.Request["UserID"] + "." + ext;
                return (uploadpath + str + "?" + DateTime.Now.ToString());
            }
            return (uploadpath + str + "|" + uploadpath + str3 + "|" + uploadpath + str4);
        }

        public bool IsReusable
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

