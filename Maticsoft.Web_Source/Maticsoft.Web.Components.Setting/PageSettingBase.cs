namespace Maticsoft.Web.Components.Setting
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Runtime.InteropServices;

    public abstract class PageSettingBase : IPageSetting
    {
        protected ApplicationKeyType _applicationType;
        protected string _description;
        protected string _keywords;
        protected string _title;
        protected const string BaseKeyDescription = "Description";
        protected const string BaseKeyKeywords = "Keywords";
        protected const string BaseKeyTitle = "Title";
        public readonly string KeyDescription;
        public readonly string KeyKeywords;
        protected const string KeyRule = "{0}_{1}_{2}";
        public readonly string KeyTitle;
        public const string RKEY_CATEID = "{cateid}";
        public const string RKEY_CDES = "{cdes}";
        public const string RKEY_CID = "{cid}";
        public const string RKEY_CNAME = "{cname}";
        public const string RKEY_CTAG = "{ctag}";
        public const string RKEY_CTNAME = "{ctname}";
        public const string RKEY_HOSTNAME = "{hostname}";

        public PageSettingBase(string pageName, ApplicationKeyType applicationType = 1)
        {
            this._applicationType = applicationType;
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

        public void Replace(params string[][] values)
        {
            if ((values != null) && (values.Length >= 1))
            {
                this._title = this.ReplaceTitle(values);
                this._keywords = this.ReplaceKeywords(values);
                this._description = this.ReplaceDescription(values);
            }
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

        protected static string ReplaceHostName(string target)
        {
            return target.Replace("{hostname}", GetHostName(ApplicationKeyType.System));
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

        public virtual string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                ConfigSystem.Modify(this.KeyDescription, value, this.KeyTitle, this._applicationType);
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
                ConfigSystem.Modify(this.KeyKeywords, value, this.KeyTitle, this._applicationType);
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
    }
}

