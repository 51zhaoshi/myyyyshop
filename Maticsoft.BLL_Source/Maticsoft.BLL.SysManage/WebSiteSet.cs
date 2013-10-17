namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Web;

    public class WebSiteSet
    {
        private ApplicationKeyType applicationKeyType = ApplicationKeyType.System;
        public const string BASE_HOST = "BaseHost";
        public const string COMPANY_ADDRESS = "CompanyAddress";
        public const string COMPANY_FAX = "CompanyFax";
        public const string COMPANY_MAIL = "CompanyMail";
        public const string COMPANY_NAME = "CompanyName";
        public const string COMPANY_TELEPHONE = "CompanyTelephone";
        public const string DATE_FORMAT = "DateFormat";
        public const string FOREGROUND_LANGUAGE = "ForegroundLanguage";
        public const string KEY_PAGEFOOTJS = "PageFootJs";
        public const string KEY_WORDS = "Keywords";
        public const string LOGO_PATH = "LogoPath";
        public const string REGIST_STATEMENT = "RegistStatement";
        public const string SHOP_IMAGESIZES = "Shop_ImageSizes";
        public const string SHOP_NORMALIMAGEHEIGHT = "Shop_NormalImageHeight";
        public const string SHOP_NORMALIMAGEWIDTH = "Shop_NormalImageWidth";
        public const string SHOP_THUMBIMAGEHEIGHT = "Shop_ThumbImageHeight";
        public const string SHOP_THUMBIMAGEWIDTH = "Shop_ThumbImageWidth";
        public const string TIME_FORMAT = "TimeFormat";
        public const string TIMEZONE_INFORMATION = "TimeZoneInformation";
        public const string WEB_AUTHORIZECODE = "AuthorizeCode";
        public const string WEB_BAIDUSHAREUSERID = "BaiduShareUserId";
        public const string WEB_DESCRIPTION = "Description";
        public const string WEB_NAME = "WebName";
        public const string WEB_POWERBY = "WebPowerBy";
        public const string WEB_RECORD = "WebRecord";
        public const string WEB_TITLE = "Title";
        public const string WEBSITE_DOMAIN = "WebSiteDomain";
        public const string WEBSITE_LOGO = "WebSiteLogo";

        public WebSiteSet(ApplicationKeyType keyType)
        {
            this.applicationKeyType = keyType;
        }

        public string AuthorizeCode
        {
            get
            {
                return ConfigSystem.GetValueByCache("AuthorizeCode", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("AuthorizeCode", value, "AuthorizeCode", this.applicationKeyType);
                HttpRuntime.UnloadAppDomain();
            }
        }

        public string BaiduShareUserId
        {
            get
            {
                return ConfigSystem.GetValueByCache("BaiduShareUserId", this.applicationKeyType);
            }
            set
            {
                string str = (Globals.SafeInt(value, 0) == 0) ? "0" : value;
                ConfigSystem.Modify("BaiduShareUserId", str, "百度分享的用户id", this.applicationKeyType);
            }
        }

        public string BaseHost
        {
            get
            {
                return ConfigSystem.GetValueByCache("BaseHost", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("BaseHost", value, "BaseHost", this.applicationKeyType);
            }
        }

        public string Company_Address
        {
            get
            {
                return ConfigSystem.GetValueByCache("CompanyAddress", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("CompanyAddress", value, "CompanyAddress", this.applicationKeyType);
            }
        }

        public string Company_Fax
        {
            get
            {
                return ConfigSystem.GetValueByCache("CompanyFax", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("CompanyFax", value, "CompanyFax", this.applicationKeyType);
            }
        }

        public string Company_Mail
        {
            get
            {
                return ConfigSystem.GetValueByCache("CompanyMail", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("CompanyMail", value, "CompanyMail", this.applicationKeyType);
            }
        }

        public string Company_Name
        {
            get
            {
                return ConfigSystem.GetValueByCache("CompanyName", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("CompanyName", value, "CompanyName", this.applicationKeyType);
            }
        }

        public string Company_Telephone
        {
            get
            {
                return ConfigSystem.GetValueByCache("CompanyTelephone", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("CompanyTelephone", value, "CompanyTelephone", this.applicationKeyType);
            }
        }

        public string Date_Format
        {
            get
            {
                return ConfigSystem.GetValueByCache("DateFormat", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("DateFormat", value, "DateFormat", this.applicationKeyType);
            }
        }

        public string Description
        {
            get
            {
                return ConfigSystem.GetValueByCache("Description", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Description", value, "Description", this.applicationKeyType);
            }
        }

        public string ForeGround_Language
        {
            get
            {
                return ConfigSystem.GetValueByCache("ForegroundLanguage", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("ForegroundLanguage", value, "ForegroundLanguage", this.applicationKeyType);
            }
        }

        public string KeyWords
        {
            get
            {
                return ConfigSystem.GetValueByCache("Keywords", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Keywords", value, "Keywords", this.applicationKeyType);
            }
        }

        public string LogoPath
        {
            get
            {
                return ConfigSystem.GetValueByCache("LogoPath", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("LogoPath", value, "LogoPath", this.applicationKeyType);
            }
        }

        public string PageFootJs
        {
            get
            {
                return ConfigSystem.GetValueByCache("PageFootJs", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("PageFootJs", value, "PageFootJs", this.applicationKeyType);
            }
        }

        public string RegistStatement
        {
            get
            {
                return ConfigSystem.GetValueByCache("RegistStatement", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("RegistStatement", value, "RegistStatement", this.applicationKeyType);
            }
        }

        public string Shop_ImageSizes
        {
            get
            {
                return ConfigSystem.GetValueByCache("Shop_ImageSizes", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Shop_ImageSizes", value, "Shop_ImageSizes", this.applicationKeyType);
            }
        }

        public string Shop_NormalImageHeight
        {
            get
            {
                return ConfigSystem.GetValueByCache("Shop_NormalImageHeight", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Shop_NormalImageHeight", value, "Shop_NormalImageHeight", this.applicationKeyType);
            }
        }

        public string Shop_NormalImageWidth
        {
            get
            {
                return ConfigSystem.GetValueByCache("Shop_NormalImageWidth", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Shop_NormalImageWidth", value, "Shop_NormalImageWidth", this.applicationKeyType);
            }
        }

        public string Shop_ThumbImageHeight
        {
            get
            {
                return ConfigSystem.GetValueByCache("Shop_ThumbImageHeight", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Shop_ThumbImageHeight", value, "Shop_ThumbImageHeight", this.applicationKeyType);
            }
        }

        public string Shop_ThumbImageWidth
        {
            get
            {
                return ConfigSystem.GetValueByCache("Shop_ThumbImageWidth", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Shop_ThumbImageWidth", value, "Shop_ThumbImageWidth", this.applicationKeyType);
            }
        }

        public string Time_Format
        {
            get
            {
                return ConfigSystem.GetValueByCache("TimeFormat", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("TimeFormat", value, "TimeFormat", this.applicationKeyType);
            }
        }

        public string Timezone_Information
        {
            get
            {
                return ConfigSystem.GetValueByCache("TimeZoneInformation", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("TimeZoneInformation", value, "TimeZoneInformation", this.applicationKeyType);
            }
        }

        public string WebName
        {
            get
            {
                return ConfigSystem.GetValueByCache("WebName", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("WebName", value, "WebName", this.applicationKeyType);
            }
        }

        public string WebPowerBy
        {
            get
            {
                return ConfigSystem.GetValueByCache("WebPowerBy", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("WebPowerBy", value, "WebPowerBy", this.applicationKeyType);
            }
        }

        public string WebRecord
        {
            get
            {
                return ConfigSystem.GetValueByCache("WebRecord", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("WebRecord", value, "WebRecord", this.applicationKeyType);
            }
        }

        public string WebSite_Domain
        {
            get
            {
                return ConfigSystem.GetValueByCache("WebSiteDomain", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("WebSiteDomain", value, "WebSiteDomain", this.applicationKeyType);
            }
        }

        public string WebSite_Logo
        {
            get
            {
                return ConfigSystem.GetValueByCache("WebSiteLogo", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("WebSiteLogo", value, "WebSiteLogo", this.applicationKeyType);
            }
        }

        public string WebTitle
        {
            get
            {
                return ConfigSystem.GetValueByCache("Title", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("Title", value, "Title", this.applicationKeyType);
            }
        }
    }
}

