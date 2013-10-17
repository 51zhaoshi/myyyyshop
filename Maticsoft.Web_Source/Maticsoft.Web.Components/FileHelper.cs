namespace Maticsoft.Web.Components
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Settings;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ZipLib.Zip;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;
    using System.Xml;

    public class FileHelper
    {
        private static WebSiteSet WebSiteSetShop = new WebSiteSet(ApplicationKeyType.Shop);

        public static string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        public static string DateToString(DateTime dt)
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.System);
            string str = string.IsNullOrWhiteSpace(set.Date_Format) ? "yyyy-MM-dd" : set.Date_Format;
            string str2 = string.IsNullOrWhiteSpace(set.Time_Format) ? "HH:mm:ss" : set.Time_Format;
            return dt.ToString(str + " " + str2);
        }

        public static string DateToString(object dt)
        {
            return DateToString(Globals.SafeDateTime(dt.ToString(), DateTime.Now));
        }

        public static bool DeleteFile(List<string> FileUrls, ref string Error)
        {
            try
            {
                if ((FileUrls != null) && (FileUrls.Count > 0))
                {
                    foreach (string str in FileUrls)
                    {
                        if (File.Exists(HttpContext.Current.Server.MapPath(str)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(str));
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                Error = exception.Message;
                return false;
            }
        }

        public static bool DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType areaType, string path)
        {
            try
            {
                ApplicationKeyType sNS = ApplicationKeyType.SNS;
                switch (areaType)
                {
                    case Maticsoft.Model.Ms.EnumHelper.AreaType.CMS:
                        sNS = ApplicationKeyType.CMS;
                        break;

                    case Maticsoft.Model.Ms.EnumHelper.AreaType.SNS:
                        sNS = ApplicationKeyType.SNS;
                        break;

                    case Maticsoft.Model.Ms.EnumHelper.AreaType.Shop:
                        sNS = ApplicationKeyType.Shop;
                        break;

                    default:
                        sNS = ApplicationKeyType.SNS;
                        break;
                }
                if (path.Contains("http://"))
                {
                    return UpYunManager.DeleteImage(path, sNS);
                }
                List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(areaType, "");
                if ((thumSizeList != null) && (thumSizeList.Count > 0))
                {
                    foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                    {
                        string str = string.Format(path, size.ThumName);
                        if (File.Exists(HttpContext.Current.Server.MapPath(str)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(str));
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                    Loginfo = "删除图片【" + path + "】失败",
                    OPTime = DateTime.Now,
                    StackTrace = exception.Message,
                    Url = ""
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                return false;
            }
        }

        public static bool DeleteFile(List<Maticsoft.Model.Ms.ThumbnailSize> thumbnailSizes, string path, ref string Error)
        {
            try
            {
                if ((thumbnailSizes != null) && (thumbnailSizes.Count > 0))
                {
                    foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumbnailSizes)
                    {
                        string str = string.Format(path, size.ThumName);
                        if (File.Exists(HttpContext.Current.Server.MapPath(str)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(str));
                        }
                    }
                }
                return true;
            }
            catch (Exception exception)
            {
                Error = exception.Message;
                return false;
            }
        }

        public static int EndIndex(int PageSize, int PageIndex)
        {
            return (PageSize * PageIndex);
        }

        public static bool FileRemove(string OldPath, string NewPath, ref string RefNewPath)
        {
            if (string.IsNullOrEmpty(OldPath) || string.IsNullOrEmpty(NewPath))
            {
                return true;
            }
            try
            {
                string fileName = Path.GetFileName(OldPath);
                string path = HttpContext.Current.Server.MapPath(OldPath);
                string str3 = HttpContext.Current.Server.MapPath(NewPath);
                if (File.Exists(path))
                {
                    RefNewPath = NewPath + fileName;
                    if (File.Exists(str3 + fileName))
                    {
                        File.Delete(str3 + fileName);
                    }
                    File.Move(path, str3 + fileName);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GeThumbImage(string imageurl, string thumbName)
        {
            if (string.IsNullOrWhiteSpace(imageurl) || string.IsNullOrWhiteSpace(thumbName))
            {
                return string.Empty;
            }
            if (imageurl.Contains("taobaocdn.com"))
            {
                return imageurl;
            }
            if (imageurl.StartsWith("http://"))
            {
                return (imageurl + Maticsoft.BLL.Ms.ThumbnailSize.GetCloudName(thumbName));
            }
            string path = string.Format(imageurl, "T_");
            if (File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                return path;
            }
            return string.Format(imageurl, thumbName);
        }

        public static string GetNewFileName(string OldFileName)
        {
            if (!string.IsNullOrEmpty(OldFileName) && OldFileName.Contains("."))
            {
                return (CreateIDCode() + "." + OldFileName.Substring(OldFileName.LastIndexOf(".") + 1));
            }
            return "";
        }

        private static Size GetNormalImageSize()
        {
            int num = Globals.SafeInt(WebSiteSetShop.Shop_NormalImageWidth, 0);
            int num2 = Globals.SafeInt(WebSiteSetShop.Shop_NormalImageHeight, 0);
            return StringPlus.SplitToSize(ConfigSystem.GetValueByCache("ProductNormalImageSize"), '|', (num == 0) ? SettingConstant.ProductNormalSize.Width : num, (num2 == 0) ? SettingConstant.ProductNormalSize.Height : num2);
        }

        public static List<DirectoryInfo> GetThemeList(string AreaName)
        {
            string format = "/Areas/{0}/Themes";
            List<DirectoryInfo> list = new List<DirectoryInfo>();
            if (Directory.Exists(HttpContext.Current.Server.MapPath(string.Format(format, AreaName))))
            {
                string[] directories = Directory.GetDirectories(HttpContext.Current.Server.MapPath(string.Format(format, AreaName)));
                if ((directories == null) || (directories.Length <= 0))
                {
                    return list;
                }
                foreach (string str2 in directories)
                {
                    DirectoryInfo item = new DirectoryInfo(str2);
                    list.Add(item);
                }
            }
            return list;
        }

        public static Maticsoft.Model.Ms.Theme GetThemeModel(string FilePath, Maticsoft.Model.Ms.Theme model)
        {
            string str = "/Theme.xml";
            string str2 = "/Theme.png";
            string path = FilePath + str;
            if (File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                XmlDocument document = new XmlDocument();
                document.Load(HttpContext.Current.Server.MapPath(path));
                XmlElement documentElement = document.DocumentElement;
                if (documentElement == null)
                {
                    return model;
                }
                model.Author = documentElement.SelectSingleNode("Author").InnerText;
                model.Description = documentElement.SelectSingleNode("Description").InnerText;
                model.Language = documentElement.SelectSingleNode("Language").InnerText;
            }
            string str4 = FilePath + str2;
            model.PreviewPhotoSrc = str4;
            return model;
        }

        public static List<Maticsoft.Model.Ms.Theme> GetThemes(string AreaName)
        {
            string format = "/Areas/{0}/Themes";
            List<Maticsoft.Model.Ms.Theme> list = new List<Maticsoft.Model.Ms.Theme>();
            if (Directory.Exists(HttpContext.Current.Server.MapPath(string.Format(format, AreaName))))
            {
                string[] directories = Directory.GetDirectories(HttpContext.Current.Server.MapPath(string.Format(format, AreaName)));
                if ((directories == null) || (directories.Length <= 0))
                {
                    return list;
                }
                foreach (string str2 in directories)
                {
                    Maticsoft.Model.Ms.Theme model = new Maticsoft.Model.Ms.Theme();
                    DirectoryInfo info = new DirectoryInfo(str2);
                    model.Name = info.Name;
                    model = GetThemeModel(string.Format(format, AreaName) + "/" + info.Name, model);
                    list.Add(model);
                }
            }
            return list;
        }

        private static Size GetThumbImageSize()
        {
            int num = Globals.SafeInt(WebSiteSetShop.Shop_ThumbImageWidth, 0);
            int num2 = Globals.SafeInt(WebSiteSetShop.Shop_ThumbImageHeight, 0);
            return StringPlus.SplitToSize(ConfigSystem.GetValueByCache("ProductNormalImageSize"), '|', (num == 0) ? SettingConstant.ProductThumbSize.Width : num, (num2 == 0) ? SettingConstant.ProductThumbSize.Height : num2);
        }

        public static bool ImageCutMethod(string imgname, string uploadpath, string SmallImageSize, string BigImageSize, ref string SmallImagePath, ref string BigImagePath)
        {
            try
            {
                string str = "S_" + imgname;
                string newpath = HttpContext.Current.Server.MapPath(SmallImagePath + str);
                int width = 400;
                int height = 400;
                if ((SmallImageSize != null) && (SmallImageSize.Split(new char[] { 'X' }).Length > 1))
                {
                    string[] strArray = SmallImageSize.Split(new char[] { 'X' });
                    width = Globals.SafeInt(strArray[0], 400);
                    height = Globals.SafeInt(strArray[1], 400);
                }
                MakeWaterThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), newpath, width, height, MakeThumbnailMode.Auto);
                string str3 = "B_" + imgname;
                string str4 = HttpContext.Current.Server.MapPath(BigImagePath + str3);
                int num3 = 800;
                int num4 = 800;
                if ((BigImageSize != null) && (BigImageSize.Split(new char[] { 'X' }).Length > 1))
                {
                    string[] strArray2 = BigImageSize.Split(new char[] { 'X' });
                    num3 = Globals.SafeInt(strArray2[0], 800);
                    num4 = Globals.SafeInt(strArray2[1], 800);
                }
                MakeWaterThumbnail(HttpContext.Current.Server.MapPath(uploadpath + imgname), str4, num3, num4, MakeThumbnailMode.Auto);
                SmallImagePath = uploadpath + str;
                BigImagePath = uploadpath + str3;
                return true;
            }
            catch (Exception)
            {
                SmallImagePath = "";
                BigImagePath = "";
                return false;
            }
        }

        public static void MakeImageWater(string oldpath, string newpath)
        {
            string valueByCache = ConfigSystem.GetValueByCache("System_waterImageMarkPosition");
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = "WM_CENTER";
            }
            string str2 = ConfigSystem.GetValueByCache("System_waterMarkTransparent");
            if (string.IsNullOrEmpty(str2))
            {
                str2 = "30";
            }
            string str3 = ConfigSystem.GetValue("System_waterMarkPhotoUrl");
            if (string.IsNullOrEmpty(str3))
            {
                str3 = "/Upload/WebSiteLogo/sitelogo.png";
            }
            try
            {
                ImageTools.addWatermarkImage(oldpath, newpath, HttpContext.Current.Server.MapPath(str3), valueByCache, Globals.SafeInt(str2, 30), InterpolationMode.High, SmoothingMode.HighQuality, PixelFormat.Format24bppRgb);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void MakeImageWaterThumbnail(string oldpath, string newpath, int width, int Height, MakeThumbnailMode mode)
        {
            string valueByCache = ConfigSystem.GetValueByCache("System_waterImageMarkPosition");
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = "WM_CENTER";
            }
            string str2 = ConfigSystem.GetValueByCache("System_waterMarkTransparent");
            if (string.IsNullOrEmpty(str2))
            {
                str2 = "30";
            }
            string str3 = ConfigSystem.GetValue("System_waterMarkPhotoUrl");
            if (string.IsNullOrEmpty(str3))
            {
                str3 = "/Upload/WebSiteLogo/sitelogo.png";
            }
            try
            {
                ImageTools.MakeImageWaterThumbnail(oldpath, newpath, width, Height, mode, ImageFormat.Png, HttpContext.Current.Server.MapPath(str3), valueByCache, Globals.SafeInt(str2, 30), InterpolationMode.High, SmoothingMode.HighQuality);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void MakeTextWater(string oldpath, string newpath)
        {
            string valueByCache = ConfigSystem.GetValueByCache("System_waterMarkContent");
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = "MH ";
            }
            string str2 = ConfigSystem.GetValueByCache("System_waterMarkFont");
            if (string.IsNullOrWhiteSpace(str2))
            {
                str2 = "arial";
            }
            string str3 = ConfigSystem.GetValueByCache("System_waterMarkFontSize");
            if (string.IsNullOrWhiteSpace(str3))
            {
                str3 = "14";
            }
            string str4 = ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (string.IsNullOrWhiteSpace(str4))
            {
                str4 = "WM_CENTER";
            }
            string str5 = ConfigSystem.GetValueByCache("System_waterMarkFontColor");
            if (string.IsNullOrWhiteSpace(str5))
            {
                str5 = "#FFFFFF";
            }
            try
            {
                ImageTools.addWatermarkText(oldpath, newpath, valueByCache, str4, str2, Globals.SafeInt(str3, 14), str5, InterpolationMode.High, SmoothingMode.HighQuality, PixelFormat.Format24bppRgb, 0x9c);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void MakeTextWaterThumbnail(string oldpath, string newpath, int width, int Height, MakeThumbnailMode mode)
        {
            string valueByCache = ConfigSystem.GetValueByCache("System_waterMarkContent");
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = "MH ";
            }
            string str2 = ConfigSystem.GetValueByCache("System_waterMarkFont");
            if (string.IsNullOrWhiteSpace(str2))
            {
                str2 = "arial";
            }
            string str3 = ConfigSystem.GetValueByCache("System_waterMarkFontSize");
            if (string.IsNullOrWhiteSpace(str3))
            {
                str3 = "14";
            }
            string str4 = ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (string.IsNullOrWhiteSpace(str4))
            {
                str4 = "WM_CENTER";
            }
            string str5 = ConfigSystem.GetValueByCache("System_waterMarkFontColor");
            if (string.IsNullOrWhiteSpace(str5))
            {
                str5 = "#FFFFFF";
            }
            try
            {
                ImageTools.MakeTextWaterThumbnail(oldpath, newpath, width, Height, mode, ImageFormat.Png, valueByCache, str4, str2, Globals.SafeInt(str3, 14), str5, InterpolationMode.High, SmoothingMode.HighQuality);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void MakeWater(string oldpath, string newpath)
        {
            if (Globals.SafeInt(ConfigSystem.GetValueByCache("System_waterMarkType"), 0) == 0)
            {
                MakeTextWater(oldpath, newpath);
            }
            else
            {
                MakeImageWater(oldpath, newpath);
            }
        }

        public static void MakeWaterThumbnail(string oldpath, string newpath, int width, int Height, MakeThumbnailMode mode)
        {
            if (Globals.SafeInt(ConfigSystem.GetValueByCache("System_waterMarkType"), 0) == 0)
            {
                MakeTextWaterThumbnail(oldpath, newpath, width, Height, mode);
            }
            else
            {
                MakeImageWaterThumbnail(oldpath, newpath, width, Height, mode);
            }
        }

        public static int StartIndex(int PageSize, int PageIndex)
        {
            return ((PageSize * (PageIndex - 1)) + 1);
        }

        public static bool UnpackFiles(string zipFile, string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                ZipInputStream stream = new ZipInputStream(File.OpenRead(zipFile));
                ZipEntry entry = null;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);
                    if (directoryName != string.Empty)
                    {
                        Directory.CreateDirectory(directory + directoryName);
                    }
                    if (fileName != string.Empty)
                    {
                        FileStream stream2 = File.Create(Path.Combine(directory, entry.Name));
                        int count = 0x800;
                        byte[] buffer = new byte[count];
                        while (true)
                        {
                            count = stream.Read(buffer, 0, buffer.Length);
                            if (count <= 0)
                            {
                                break;
                            }
                            stream2.Write(buffer, 0, count);
                        }
                        stream2.Close();
                    }
                }
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

