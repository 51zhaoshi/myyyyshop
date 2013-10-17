namespace Maticsoft.Web.Areas
{
    using Maticsoft.Components;
    using Maticsoft.Web;
    using System;
    using System.Web.Mvc;

    public abstract class AreaRegistrationBase : AreaRegistration
    {
        protected AreaRoute CurrentArea;
        protected string CurrentRouteName;
        protected string CurrentRoutePath;
        public const int OutputCacheDuration = 300;

        public AreaRegistrationBase(AreaRoute currentArea)
        {
            this.CurrentArea = currentArea;
            this.CurrentRouteName = string.Format("{0}_Default", this.AreaName);
            this.CurrentRoutePath = (Maticsoft.Components.MvcApplication.MainAreaRoute == currentArea) ? "" : (this.AreaName + "/");
        }

        protected static string PathUploadFolderBase(AreaRoute currentArea)
        {
            return string.Format("/{0}/{1}/", Maticsoft.Components.MvcApplication.UploadFolder, currentArea.ToString());
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string name = string.Format("{0}_RSSDemo", this.AreaName);
            string url = this.CurrentRoutePath + "RSSDemo";
            object defaults = new {
                controller = "RSS",
                action = "Index",
                AlbumID = UrlParameter.Optional
            };
            string[] namespaces = new string[] { "Maticsoft.Web.Controllers.*" };
            context.MapRoute(name, url, defaults, namespaces);
            if (Maticsoft.Components.MvcApplication.MainAreaRoute != this.CurrentArea)
            {
                string currentRouteName = this.CurrentRouteName;
                string str4 = this.AreaName + "/{controller}/{action}/{id}";
                object obj3 = new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                };
                string[] strArray4 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
                context.MapRoute(currentRouteName, str4, obj3, strArray4);
            }
        }

        public sealed override string AreaName
        {
            get
            {
                return this.CurrentArea.ToString();
            }
        }
    }
}

