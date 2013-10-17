namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web.Controllers;
    using System;
    using System.Runtime.CompilerServices;

    public class CMSControllerBase : ControllerBase
    {
        public CMSControllerBase()
        {
            ((dynamic) base.ViewBag).BaiduShareUserId = Globals.SafeInt(ConfigSystem.GetValueByCache("BaiduShareUserId"), 0);
        }

        [CompilerGenerated]
        private static class ctor>o__SiteContainer0
        {
            public static CallSite<Func<CallSite, object, int, object>> <>p__Site1;
        }
    }
}

