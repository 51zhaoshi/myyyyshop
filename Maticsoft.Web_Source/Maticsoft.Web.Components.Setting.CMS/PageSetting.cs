namespace Maticsoft.Web.Components.Setting.CMS
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class PageSetting : IPageSetting
    {
        protected string _alt;
        protected ApplicationKeyType _applicationType;
        protected string _description;
        protected string _imagetitle;
        protected bool _isimage;
        protected string _keywords;
        protected string _title;
        protected string _url;
        protected const string BaseKeyAlt = "KeyAlt";
        protected const string BaseKeyDescription = "Description";
        protected const string BaseKeyImageTitle = "ImageTitle";
        protected const string BaseKeyKeywords = "Keywords";
        protected const string BaseKeyTitle = "Title";
        protected const string BaseKeyUrl = "KeyUrl";
        public readonly string KeyAlt;
        public readonly string KeyDescription;
        public readonly string KeyImageTitle;
        public readonly string KeyKeywords;
        protected const string KeyRule = "{0}_{1}_{2}";
        public readonly string KeyTitle;
        public readonly string KeyUrl;
        public const string RKEY_CATEDIR = "{catedir}";
        public const string RKEY_CATEID = "{cateid}";
        public const string RKEY_CATEPATH = "{catepath}";
        public const string RKEY_CDES = "{cdes}";
        public const string RKEY_CID = "{cid}";
        public const string RKEY_CNAME = "{cname}";
        public const string RKEY_CNAME_P = "{cname_p}";
        public const string RKEY_CTAG = "{ctag}";
        public const string RKEY_CTNAME = "{ctname}";
        public const string RKEY_CTNAME_P = "{ctname_p}";
        public const string RKEY_HOSTNAME = "{hostname}";
        public const string RKEY_NAMEPATH = "{namepath}";
        public const string RKEY_NAMEPATH_P = "{namepath_p}";

        public PageSetting()
        {
        }

        public PageSetting(string pageName, ApplicationKeyType applicationType = 1, string type = "Base")
        {
            this._applicationType = applicationType;
            if (type == "Base")
            {
                this.KeyTitle = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "Title");
                this.KeyKeywords = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "Keywords");
                this.KeyDescription = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "Description");
                this._title = ConfigSystem.GetValueByCache(this.KeyTitle, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._title))
                {
                    this._title = ConfigSystem.GetValueByCache("Title", ApplicationKeyType.System);
                }
                this._keywords = ConfigSystem.GetValueByCache(this.KeyKeywords, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._keywords))
                {
                    this._keywords = ConfigSystem.GetValueByCache("Keywords", ApplicationKeyType.System);
                }
                this._description = ConfigSystem.GetValueByCache(this.KeyDescription, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._description))
                {
                    this._description = ConfigSystem.GetValueByCache("Description", ApplicationKeyType.System);
                }
                this._isimage = false;
            }
            if (type == "Url")
            {
                this.KeyTitle = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "Title");
                this.KeyKeywords = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "Keywords");
                this.KeyDescription = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "Description");
                this.KeyUrl = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "KeyUrl");
                this._title = ConfigSystem.GetValueByCache(this.KeyTitle, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._title))
                {
                    this._title = ConfigSystem.GetValueByCache("Title", ApplicationKeyType.System);
                }
                this._url = ConfigSystem.GetValueByCache(this.KeyUrl, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._url))
                {
                    this._url = ConfigSystem.GetValueByCache("KeyUrl", ApplicationKeyType.System);
                }
                this._keywords = ConfigSystem.GetValueByCache(this.KeyKeywords, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._keywords))
                {
                    this._keywords = ConfigSystem.GetValueByCache("Keywords", ApplicationKeyType.System);
                }
                this._description = ConfigSystem.GetValueByCache(this.KeyDescription, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._description))
                {
                    this._description = ConfigSystem.GetValueByCache("Description", ApplicationKeyType.System);
                }
                this._isimage = false;
            }
            if (type == "Image")
            {
                this._isimage = true;
                this.KeyAlt = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "KeyAlt");
                this.KeyImageTitle = string.Format("{0}_{1}_{2}", this._applicationType, pageName, "ImageTitle");
                this._alt = ConfigSystem.GetValueByCache(this.KeyAlt, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._alt))
                {
                    this._alt = ConfigSystem.GetValueByCache("KeyAlt", ApplicationKeyType.System);
                }
                this._imagetitle = ConfigSystem.GetValueByCache(this.KeyImageTitle, this._applicationType);
                if (string.IsNullOrWhiteSpace(this._imagetitle))
                {
                    this._imagetitle = ConfigSystem.GetValueByCache("ImageTitle", ApplicationKeyType.System);
                }
            }
        }

        public static PageSetting GetArticleSetting(Maticsoft.Model.CMS.Content contModel, string pageName = "CMS", ApplicationKeyType applicationType = 4)
        {
            if (contModel == null)
            {
                contModel = new Maticsoft.Model.CMS.Content();
            }
            PageSetting setting = new PageSetting(pageName, applicationType, "Base");
            if (!string.IsNullOrWhiteSpace(contModel.Meta_Title))
            {
                setting._title = contModel.Meta_Title;
            }
            else
            {
                setting._title = ConfigSystem.GetValueByCache(setting.KeyTitle, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._title))
                {
                    setting._title = ConfigSystem.GetValueByCache("Title", ApplicationKeyType.System);
                }
            }
            if (!string.IsNullOrWhiteSpace(contModel.Meta_Keywords))
            {
                setting._keywords = contModel.Meta_Keywords;
            }
            else
            {
                setting._keywords = ConfigSystem.GetValueByCache(setting.KeyKeywords, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._keywords))
                {
                    setting._keywords = ConfigSystem.GetValueByCache("Keywords", ApplicationKeyType.System);
                }
            }
            if (!string.IsNullOrWhiteSpace(contModel.Meta_Description))
            {
                setting._description = contModel.Meta_Description;
            }
            else
            {
                setting._description = ConfigSystem.GetValueByCache(setting.KeyDescription, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._description))
                {
                    setting._description = ConfigSystem.GetValueByCache("Description", ApplicationKeyType.System);
                }
            }
            if (setting._title.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._title = ReplaceHostName(setting._title);
            }
            if (setting._keywords.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._keywords = ReplaceHostName(setting._keywords);
            }
            if (setting._description.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._description = ReplaceHostName(setting._description);
            }
            setting.Replace(new string[][] { new string[] { "{cname}", contModel.Title }, new string[] { "{cid}", contModel.ContentID.ToString() }, new string[] { "{ctname}", contModel.ClassName }, new string[] { "{cateid}", contModel.ClassID.ToString() } });
            return setting;
        }

        public static string GetCMSUrl(int NewId, string pageName = "CMS", ApplicationKeyType applicationType = 4)
        {
            Maticsoft.BLL.CMS.Content content = new Maticsoft.BLL.CMS.Content();
            Maticsoft.Model.CMS.Content modelByCache = content.GetModelByCache(NewId);
            Maticsoft.BLL.CMS.ContentClass class2 = new Maticsoft.BLL.CMS.ContentClass();
            if (modelByCache != null)
            {
                string str = content.GetContentUrl(modelByCache) + ".html";
                string valueByCache = ConfigSystem.GetValueByCache("ArticleStaticRoot");
                valueByCache = (valueByCache.LastIndexOf("/") > -1) ? valueByCache : (valueByCache + "/");
                return (valueByCache + class2.GetClassUrl(modelByCache.ClassID) + "/" + str).Replace("--", "-").ToLower();
            }
            return "";
        }

        public static PageSetting GetContentClassSetting(Maticsoft.Model.CMS.ContentClass classModel, string pageName = "CMSSelf", ApplicationKeyType applicationType = 4)
        {
            if (classModel == null)
            {
                classModel = new Maticsoft.Model.CMS.ContentClass();
            }
            PageSetting setting = new PageSetting(pageName, applicationType, "Base");
            if (!string.IsNullOrWhiteSpace(classModel.Meta_Title))
            {
                setting._title = classModel.Meta_Title;
            }
            else
            {
                setting._title = ConfigSystem.GetValueByCache(setting.KeyTitle, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._title))
                {
                    setting._title = ConfigSystem.GetValueByCache("Title", ApplicationKeyType.System);
                }
            }
            if (!string.IsNullOrWhiteSpace(classModel.Meta_Keywords))
            {
                setting._keywords = classModel.Meta_Keywords;
            }
            else
            {
                setting._keywords = ConfigSystem.GetValueByCache(setting.KeyKeywords, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._keywords))
                {
                    setting._keywords = ConfigSystem.GetValueByCache("Keywords", ApplicationKeyType.System);
                }
            }
            if (!string.IsNullOrWhiteSpace(classModel.Meta_Description))
            {
                setting._description = classModel.Meta_Description;
            }
            else
            {
                setting._description = ConfigSystem.GetValueByCache(setting.KeyDescription, setting._applicationType);
                if (string.IsNullOrWhiteSpace(setting._description))
                {
                    setting._description = ConfigSystem.GetValueByCache("Description", ApplicationKeyType.System);
                }
            }
            if (setting._title.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._title = ReplaceHostName(setting._title);
            }
            if (setting._keywords.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._keywords = ReplaceHostName(setting._keywords);
            }
            if (setting._description.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._description = ReplaceHostName(setting._description);
            }
            setting.Replace(new string[][] { new string[] { "{ctname}", classModel.ClassName }, new string[] { "{cateid}", classModel.ClassID.ToString() } });
            return setting;
        }

        public static string GetDescription(string pageName, ApplicationKeyType applicationType = 1)
        {
            string valueByCache = ConfigSystem.GetValueByCache(string.Format("{0}_{1}_{2}", applicationType, pageName, "Description"), applicationType);
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = ConfigSystem.GetValueByCache("Description", ApplicationKeyType.System);
            }
            if (valueByCache.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                return ReplaceHostName(valueByCache);
            }
            return valueByCache;
        }

        public static string GetHostName(ApplicationKeyType applicationType = 1)
        {
            return ConfigSystem.GetValueByCache("WebName", applicationType);
        }

        public static string GetKeywords(string pageName, ApplicationKeyType applicationType = 1)
        {
            string valueByCache = ConfigSystem.GetValueByCache(string.Format("{0}_{1}_{2}", applicationType, pageName, "Keywords"), applicationType);
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = ConfigSystem.GetValueByCache("Keywords", ApplicationKeyType.System);
            }
            if (valueByCache.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                return ReplaceHostName(valueByCache);
            }
            return valueByCache;
        }

        public static PageSetting GetPageSetting(string pageName, ApplicationKeyType applicationType = 1)
        {
            PageSetting setting = new PageSetting(pageName, applicationType, "Base");
            if (setting._title.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._title = ReplaceHostName(setting._title);
            }
            if (setting._keywords.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._keywords = ReplaceHostName(setting._keywords);
            }
            if (setting._description.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                setting._description = ReplaceHostName(setting._description);
            }
            return setting;
        }

        public static string GetTitle(string pageName, ApplicationKeyType applicationType = 1)
        {
            string valueByCache = ConfigSystem.GetValueByCache(string.Format("{0}_{1}_{2}", applicationType, pageName, "Title"), applicationType);
            if (string.IsNullOrWhiteSpace(valueByCache))
            {
                valueByCache = ConfigSystem.GetValueByCache("Title", ApplicationKeyType.System);
            }
            if (valueByCache.IndexOf("{hostname}", StringComparison.Ordinal) > -1)
            {
                return ReplaceHostName(valueByCache);
            }
            return valueByCache;
        }

        public static bool IsHasKeyword(string values, string[] keywords)
        {
            values = values.ToLower();
            return keywords.Contains<string>(values);
        }

        public void Replace(params string[][] values)
        {
            if ((values != null) && (values.Length >= 1))
            {
                this._title = this.ReplaceTitle(values);
                this._keywords = this.ReplaceKeywords(values);
                this._description = this.ReplaceDescription(values);
            }
        }

        public string ReplaceAlt(params string[][] values)
        {
            if ((values == null) || (values.Length < 1))
            {
                return this._alt;
            }
            string str = this._alt;
            foreach (string[] strArray in values)
            {
                if (strArray.Length == 2)
                {
                    str = str.Replace(strArray[0], Globals.HtmlDecode(strArray[1]));
                }
            }
            return str;
        }

        public string ReplaceDescription(params string[][] values)
        {
            if ((values == null) || (values.Length < 1))
            {
                return this._description;
            }
            string str = this._description;
            foreach (string[] strArray in values)
            {
                if (strArray.Length == 2)
                {
                    str = str.Replace(strArray[0], StringPlus.SubString(Globals.HtmlDecode(strArray[1]), 140, ".."));
                }
            }
            return str;
        }

        private static string ReplaceHostName(string target)
        {
            return target.Replace("{hostname}", GetHostName(ApplicationKeyType.System));
        }

        public string ReplaceImageTitle(params string[][] values)
        {
            if ((values == null) || (values.Length < 1))
            {
                return this._imagetitle;
            }
            string str = this._imagetitle;
            foreach (string[] strArray in values)
            {
                if (strArray.Length == 2)
                {
                    str = str.Replace(strArray[0], Globals.HtmlDecode(strArray[1]));
                }
            }
            return str;
        }

        public string ReplaceKeywords(params string[][] values)
        {
            if ((values == null) || (values.Length < 1))
            {
                return this._keywords;
            }
            string str = this._keywords;
            foreach (string[] strArray in values)
            {
                if (strArray.Length == 2)
                {
                    str = str.Replace(strArray[0], StringPlus.SubString(Globals.HtmlDecode(strArray[1]), 30, ".."));
                }
            }
            return str;
        }

        public string ReplaceTitle(params string[][] values)
        {
            if ((values == null) || (values.Length < 1))
            {
                return this._title;
            }
            string str = this._title;
            foreach (string[] strArray in values)
            {
                if (strArray.Length == 2)
                {
                    str = str.Replace(strArray[0], StringPlus.SubString(Globals.HtmlDecode(strArray[1]), 30, ".."));
                }
            }
            return str;
        }

        public string ReplaceURL(params string[][] values)
        {
            if ((values == null) || (values.Length < 1))
            {
                return this._url;
            }
            string str = this._url;
            foreach (string[] strArray in values)
            {
                if (strArray.Length == 2)
                {
                    str = str.Replace(strArray[0], Globals.HtmlEncode(strArray[1]));
                }
            }
            return str;
        }

        public virtual string Alt
        {
            get
            {
                return this._alt;
            }
            set
            {
                this._alt = value;
                ConfigSystem.Modify(this.KeyAlt, value, this.KeyAlt, this._applicationType);
            }
        }

        public virtual string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                ConfigSystem.Modify(this.KeyDescription, value, this.KeyDescription, this._applicationType);
            }
        }

        public virtual string ImageTitle
        {
            get
            {
                return this._imagetitle;
            }
            set
            {
                this._imagetitle = value;
                ConfigSystem.Modify(this.KeyImageTitle, value, this.KeyImageTitle, this._applicationType);
            }
        }

        public virtual bool IsImage
        {
            get
            {
                return this._isimage;
            }
            set
            {
                this._isimage = value;
            }
        }

        public virtual string Keywords
        {
            get
            {
                return this._keywords;
            }
            set
            {
                this._keywords = value;
                ConfigSystem.Modify(this.KeyKeywords, value, this.KeyKeywords, this._applicationType);
            }
        }

        public virtual string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                ConfigSystem.Modify(this.KeyTitle, value, this.KeyTitle, this._applicationType);
            }
        }

        public virtual string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
                ConfigSystem.Modify(this.KeyUrl, value, this.KeyUrl, this._applicationType);
            }
        }
    }
}

