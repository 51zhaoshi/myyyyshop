namespace Maticsoft.ViewEngine
{
    using Maticsoft.Components;
    using Maticsoft.Web;
    using System;
    using System.Web.Mvc;

    public class ThemeViewEngine : RazorViewEngine
    {
        protected readonly string ThemeName;

        public ThemeViewEngine(string themeName)
        {
            this.ThemeName = themeName;
            string str = "~/Areas/{2}/Themes/" + this.ThemeName + "/Views/{1}/{0}.cshtml";
            string str2 = "~/Areas/{2}/Themes/" + this.ThemeName + "/Views/Shared/{0}.cshtml";
            base.AreaViewLocationFormats = new string[] { str, str2 };
            base.AreaMasterLocationFormats = new string[] { str, str2 };
            base.AreaPartialViewLocationFormats = new string[] { str, str2 };
            string str3 = "~/Views/{1}/{0}.cshtml";
            string str4 = "~/Views/Shared/{0}.cshtml";
            base.ViewLocationFormats = new string[] { str3, str4 };
            base.MasterLocationFormats = new string[] { str3, str4 };
            base.PartialViewLocationFormats = new string[] { str3, str4 };
            base.FileExtensions = new string[] { "cshtml" };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            if ((controllerContext.IsChildAction && (controllerContext.RouteData.Values != null)) && (controllerContext.RouteData.DataTokens != null))
            {
                this.UseAction(controllerContext, new Action<ControllerContext, AreaRoute>(Analytic.CreateBegin));
            }
            return base.CreatePartialView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            if ((controllerContext.IsChildAction && (controllerContext.RouteData.Values != null)) && (controllerContext.RouteData.DataTokens != null))
            {
                this.UseAction(controllerContext, new Action<ControllerContext, AreaRoute>(Analytic.CreateBegin));
            }
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        public override void ReleaseView(ControllerContext controllerContext, IView view)
        {
            if ((!controllerContext.IsChildAction || (controllerContext.RouteData.Values == null)) || (controllerContext.RouteData.DataTokens == null))
            {
                base.ReleaseView(controllerContext, view);
            }
            else
            {
                this.UseAction(controllerContext, new Action<ControllerContext, AreaRoute>(Analytic.CreateEnd));
                base.ReleaseView(controllerContext, view);
            }
        }

        protected void UseAction(ControllerContext controllerContext, Action<ControllerContext, AreaRoute> action)
        {
            AreaRoute none = AreaRoute.None;
            if (((controllerContext.RouteData != null) && (controllerContext.RouteData.DataTokens != null)) && controllerContext.RouteData.DataTokens.ContainsKey("area"))
            {
                none = MvcApplication.GetCurrentAreaRoute(controllerContext.RouteData.DataTokens["area"]);
            }
            switch (none)
            {
                case AreaRoute.None:
                case AreaRoute.UserCenter:
                case AreaRoute.Tao:
                case AreaRoute.COM:
                    break;

                case AreaRoute.CMS:
                case AreaRoute.Shop:
                case AreaRoute.SNS:
                    action(controllerContext, none);
                    break;

                default:
                    return;
            }
        }
    }
}

