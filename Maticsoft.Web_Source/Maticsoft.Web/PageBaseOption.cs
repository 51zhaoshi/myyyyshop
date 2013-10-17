namespace Maticsoft.Web
{
    using Maticsoft.BLL.SysManage;
    using System;

    public class PageBaseOption : IPageBaseOption
    {
        public string DefaultLogin
        {
            get
            {
                return ConfigSystem.GetValueByCache("DefaultLogin");
            }
        }

        public string DefaultLoginAdmin
        {
            get
            {
                return ConfigSystem.GetValueByCache("DefaultLoginAdmin");
            }
        }

        public string DefaultLoginEnterprise
        {
            get
            {
                return ConfigSystem.GetValueByCache("DefaultLoginEnterprise");
            }
        }
    }
}

