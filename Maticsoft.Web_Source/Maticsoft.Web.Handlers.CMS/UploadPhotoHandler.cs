namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public class UploadPhotoHandler : UploadImageHandlerBase
    {
        public UploadPhotoHandler() : base(MakeThumbnailMode.None, true, ApplicationKeyType.None)
        {
            if (ConfigSystem.GetValueByCache("CMS_ImageStoreWay") == "1")
            {
                base.IsLocalSave = false;
                base.ApplicationKeyType = ApplicationKeyType.CMS;
            }
        }

        public static string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        protected override List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, MvcApplication.ThemeName);
        }

        public string MoveImage(string ImageUrl, string savePath, string saveThumbsPath)
        {
            try
            {
                if (ConfigSystem.GetValueByCache("CMS_ImageStoreWay") == "1")
                {
                    return (ImageUrl + "|" + ImageUrl);
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                    }
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(saveThumbsPath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(saveThumbsPath));
                    }
                    List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, MvcApplication.ThemeName);
                    string str = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string path = "";
                    string str3 = "";
                    string format = saveThumbsPath + str;
                    if (File.Exists(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, ""))))
                    {
                        str3 = string.Format(savePath + str, "");
                        File.Move(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(str3));
                    }
                    if ((thumSizeList != null) && (thumSizeList.Count > 0))
                    {
                        foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                        {
                            if (File.Exists(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, size.ThumName))))
                            {
                                path = string.Format(format, size.ThumName);
                                File.Move(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, size.ThumName)), HttpContext.Current.Server.MapPath(path));
                            }
                        }
                    }
                    return (str3 + "|" + format);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            if (!string.IsNullOrWhiteSpace(context.Request.Params["album"]))
            {
                string s = context.Request.Params["album"];
                if (!string.IsNullOrWhiteSpace(context.Request.Params["userId"]))
                {
                    string str2 = context.Request.Params["userId"];
                    string str3 = context.Request.Params["folder"];
                    Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
                    HttpPostedFile file = context.Request.Files["Filedata"];
                    string path = str3 + DateTime.Now.ToString("yyyyMMdd") + "/";
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                    }
                    string str5 = path + fileName;
                    string format = path + "{0}" + fileName;
                    string str7 = "";
                    if (!base.IsLocalSave)
                    {
                        str5 = fileName;
                        format = str5;
                    }
                    Maticsoft.Model.CMS.Photo photo3 = new Maticsoft.Model.CMS.Photo {
                        PhotoName = file.FileName,
                        ImageUrl = str5,
                        Description = "",
                        AlbumID = int.Parse(s),
                        State = 1,
                        CreatedUserID = int.Parse(str2),
                        CreatedDate = DateTime.Now,
                        PVCount = 0,
                        ClassID = 1,
                        ThumbImageUrl = format,
                        NormalImageUrl = str7,
                        Sequence = new int?(photo.GetMaxSequence()),
                        IsRecomend = false,
                        CommentCount = 0,
                        Tags = ""
                    };
                    Maticsoft.Model.CMS.Photo model = photo3;
                    int num = photo.Add(model);
                    if ((num > 0) && base.IsLocalSave)
                    {
                        try
                        {
                            string str8 = uploadPath + fileName;
                            string str9 = uploadPath + "{0}" + fileName;
                            if (File.Exists(HttpContext.Current.Server.MapPath(str8)))
                            {
                                File.Move(HttpContext.Current.Server.MapPath(str8), HttpContext.Current.Server.MapPath(str5));
                            }
                            List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, MvcApplication.ThemeName);
                            if ((thumSizeList != null) && (thumSizeList.Count > 0))
                            {
                                string str10 = "";
                                foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                                {
                                    if (File.Exists(HttpContext.Current.Server.MapPath(string.Format(str9, size.ThumName))))
                                    {
                                        str10 = string.Format(format, size.ThumName);
                                        File.Move(HttpContext.Current.Server.MapPath(string.Format(str9, size.ThumName)), HttpContext.Current.Server.MapPath(str10));
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    context.Response.Write(num.ToString());
                }
            }
        }
    }
}

