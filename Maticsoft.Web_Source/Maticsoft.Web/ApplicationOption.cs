namespace Maticsoft.Web
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components;
    using System;

    public class ApplicationOption : IApplicationOption
    {
        private static readonly Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        public string AuthorizeCode
        {
            get
            {
                return ConfigSystem.GetValueByCache("AuthorizeCode");
            }
        }

        public string PageFootJs
        {
            get
            {
                return WebSiteSet.PageFootJs;
            }
        }

        public string SiteName
        {
            get
            {
                return WebSiteSet.WebName;
            }
        }

        public string ThemeName
        {
            get
            {
                return ConfigSystem.GetValueByCache("ThemeCurrent");
            }
        }

        public string WebPowerBy
        {
            get
            {
                return WebSiteSet.WebPowerBy;
            }
        }

        public string WebRecord
        {
            get
            {
                return WebSiteSet.WebRecord;
            }
        }
    }
}

