namespace Maticsoft.Web.Components.Setting.SNS
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class PageSetting : PageSettingBase
    {
        public PageSetting(string pageName, ApplicationKeyType applicationType = 1) : base(pageName, applicationType)
        {
        }

        public static PageSetting GetBlogDetailSetting(Maticsoft.Model.SNS.UserBlog userblog, string pageName = "BlogDetail", ApplicationKeyType applicationType = 2)
        {
            if (userblog == null)
            {
                userblog = new Maticsoft.Model.SNS.UserBlog();
            }
            PageSetting setting = new PageSetting(pageName, applicationType);
            if (!string.IsNullOrWhiteSpace(userblog.Meta_Title))
            {
                setting._title = userblog.Meta_Title;
            }
            else
            {
                setting._title = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache(setting.KeyTitle, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._title))
                {
                    setting._title = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Title", ApplicationKeyType.System);
                }
            }
            if (!string.IsNullOrWhiteSpace(userblog.Meta_Keywords))
            {
                setting._keywords = userblog.Meta_Keywords;
            }
            else
            {
                setting._keywords = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache(setting.KeyKeywords, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._keywords))
                {
                    setting._keywords = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Keywords", ApplicationKeyType.System);
                }
            }
            if (!string.IsNullOrWhiteSpace(userblog.Meta_Description))
            {
                setting._description = userblog.Meta_Description;
            }
            else
            {
                setting._description = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache(setting.KeyDescription, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._description))
                {
                    setting._description = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Description", ApplicationKeyType.System);
                }
            }
            if (setting._title.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._title = PageSettingBase.ReplaceHostName(setting._title);
            }
            if (setting._keywords.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._keywords = PageSettingBase.ReplaceHostName(setting._keywords);
            }
            if (setting._description.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._description = PageSettingBase.ReplaceHostName(setting._description);
            }
            setting.Replace(new string[][] { new string[] { "{cname}", userblog.Title }, new string[] { "{cid}", userblog.BlogID.ToString() } });
            return setting;
        }

        public static IPageSetting GetPageSetting(string pageName, ApplicationKeyType applicationType = 1)
        {
            PageSetting setting = new PageSetting(pageName, applicationType);
            if (setting._title.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._title = PageSettingBase.ReplaceHostName(setting._title);
            }
            if (setting._keywords.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._keywords = PageSettingBase.ReplaceHostName(setting._keywords);
            }
            if (setting._description.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._description = PageSettingBase.ReplaceHostName(setting._description);
            }
            return setting;
        }

        public static IPageSetting GetPhotoListSetting(int cid, ApplicationKeyType applicationType = 1)
        {
            Maticsoft.Model.SNS.Categories modelByCache = new Maticsoft.BLL.SNS.Categories().GetModelByCache(cid);
            PageSetting setting = new PageSetting("PhotoList", applicationType);
            if (setting._title.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._title = PageSettingBase.ReplaceHostName(setting._title);
            }
            if ((modelByCache != null) && !string.IsNullOrWhiteSpace(modelByCache.Meta_Title))
            {
                setting._title = PageSettingBase.ReplaceHostName(modelByCache.Meta_Title);
            }
            if (setting._keywords.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._keywords = PageSettingBase.ReplaceHostName(setting._keywords);
            }
            if ((modelByCache != null) && !string.IsNullOrWhiteSpace(modelByCache.Meta_Keywords))
            {
                setting._keywords = PageSettingBase.ReplaceHostName(modelByCache.Meta_Keywords);
            }
            if (setting._description.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._description = PageSettingBase.ReplaceHostName(setting._description);
            }
            if ((modelByCache != null) && !string.IsNullOrWhiteSpace(modelByCache.Meta_Description))
            {
                setting._description = PageSettingBase.ReplaceHostName(modelByCache.Meta_Description);
            }
            return setting;
        }

        public static string GetPhotoUrl(Maticsoft.Model.SNS.Photos PhotosInfo)
        {
            if (!string.IsNullOrWhiteSpace(PhotosInfo.StaticUrl))
            {
                return PhotosInfo.StaticUrl;
            }
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PhotoStaticRoot");
            valueByCache = (valueByCache.LastIndexOf("/") > -1) ? valueByCache : (valueByCache + "/");
            List<Maticsoft.Model.SNS.UserAlbumDetail> modelList = detail.GetModelList("Type=0 and TargetID=" + PhotosInfo.PhotoID);
            if ((modelList != null) && (modelList.Count > 0))
            {
                Maticsoft.Model.SNS.UserAlbums modelByCache = albums.GetModelByCache(modelList.FirstOrDefault<Maticsoft.Model.SNS.UserAlbumDetail>().AlbumID);
                if (modelByCache != null)
                {
                    return string.Concat(new object[] { valueByCache, PinyinHelper.GetPinyin(modelByCache.AlbumName), "/", PhotosInfo.PhotoID, ".html" }).Replace("--", "-").ToLower();
                }
            }
            return "";
        }

        public static string GetPhotoUrl(int photoId)
        {
            Maticsoft.Model.SNS.Photos model = new Maticsoft.BLL.SNS.Photos().GetModel(photoId);
            if (model != null)
            {
                return GetPhotoUrl(model);
            }
            return "";
        }

        public static string GetProductUrl(Maticsoft.Model.SNS.Products productInfo)
        {
            Maticsoft.BLL.SNS.Categories categories = new Maticsoft.BLL.SNS.Categories();
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ProductStaticRoot");
            valueByCache = (valueByCache.LastIndexOf("/") > -1) ? valueByCache : (valueByCache + "/");
            int id = productInfo.CategoryID.HasValue ? productInfo.CategoryID.Value : -1;
            return string.Concat(new object[] { valueByCache, categories.GetUrlByIdCache(id), "/", productInfo.ProductID, ".html" }).Replace("--", "-").ToLower();
        }

        public static string GetProductUrl(long productId)
        {
            Maticsoft.Model.SNS.Products model = new Maticsoft.BLL.SNS.Products().GetModel(productId);
            if (model != null)
            {
                return GetProductUrl(model);
            }
            return "";
        }
    }
}

