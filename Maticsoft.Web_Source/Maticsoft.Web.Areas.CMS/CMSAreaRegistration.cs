namespace Maticsoft.Web.Areas.CMS
{
    using Maticsoft.Web.Areas;
    using System;
    using System.Web.Mvc;

    public class CMSAreaRegistration : AreaRegistrationBase
    {
        public CMSAreaRegistration() : base(AreaRoute.CMS)
        {
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string name = string.Format("{0}_Home", this.AreaName);
            string url = base.CurrentRoutePath + "Index";
            object defaults = new {
                controller = "Home",
                action = "Index"
            };
            string[] namespaces = new string[] { "Maticsoft.Web.Areas.CMS.*" };
            context.MapRoute(name, url, defaults, namespaces);
            string str3 = string.Format("{0}_Category", this.AreaName);
            string str4 = base.CurrentRoutePath + "Category/{cid}";
            object obj3 = new {
                controller = "Category",
                action = "List",
                cid = UrlParameter.Optional
            };
            string[] strArray4 = new string[] { "Maticsoft.Web.Areas.CMS.*" };
            context.MapRoute(str3, str4, obj3, strArray4);
            string str5 = string.Format("{0}_Rss", this.AreaName);
            string str6 = base.CurrentRoutePath + "Rss";
            object obj4 = new {
                controller = "Rss",
                action = "List"
            };
            string[] strArray6 = new string[] { "Maticsoft.Web.Areas.CMS.*" };
            context.MapRoute(str5, str6, obj4, strArray6);
            string str7 = string.Format("{0}_About", this.AreaName);
            string str8 = base.CurrentRoutePath + "About";
            object obj5 = new {
                controller = "About",
                action = "Index"
            };
            string[] strArray8 = new string[] { "Maticsoft.Web.Areas.CMS.*" };
            context.MapRoute(str7, str8, obj5, strArray8);
            base.RegisterArea(context);
        }
    }
}

