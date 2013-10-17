namespace Maticsoft.Components
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Components.Filters;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public abstract class MvcApplication : HttpApplication
    {
        protected static IApplicationOption ApplicationOption;
        public static bool IsInstall = ConfigHelper.GetConfigBool("Installer");
        private const string KEY_ISAUTHORIZE = "Maticsoft_IsAuthorize";
        public static AreaRoute MainAreaRoute;
        public static string ProductInfo;
        public static string UploadFolder;
        public static string Version;

        public MvcApplication()
        {
            if (IsInstall)
            {
                UploadFolder = ConfigHelper.GetConfigString("UploadFolder");
                if (string.IsNullOrWhiteSpace(UploadFolder))
                {
                    UploadFolder = "Upload";
                }
                if (this.MainArea == null)
                {
                    throw new ArgumentNullException("MvcApplication: MainArea Is NULL!");
                }
                MainAreaRoute = Globals.SafeEnum<AreaRoute>(this.MainArea, AreaRoute.None, true);
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                if (base.Request.Cookies["language"] != null)
                {
                    string name = base.Request.Cookies["language"].Value.ToString();
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(name);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
                }
            }
            catch (Exception)
            {
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected virtual void Application_Start()
        {
            this.RegisterIgnoreRoutes(RouteTable.Routes);
            AreaRegistration.RegisterAllAreas();
            this.RegisterGlobalFilters(GlobalFilters.Filters);
            this.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.DefaultNamespaces.Add("Maticsoft.Web.Controllers");
            this.ApplicationStart();
        }

        protected abstract void ApplicationStart();
        public static AreaRoute GetCurrentAreaRoute(object currentArea)
        {
            if (currentArea == null)
            {
                return AreaRoute.None;
            }
            return Globals.SafeEnum<AreaRoute>(currentArea.ToString(), AreaRoute.None, true);
        }

        public static AreaRoute GetCurrentAreaRoute(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                return AreaRoute.None;
            }
            return GetCurrentAreaRoute(controllerContext.RouteData.DataTokens["area"]);
        }

        public static string GetCurrentRoutePath(AreaRoute currentArea)
        {
            if (currentArea == MainAreaRoute)
            {
                return "/";
            }
            return string.Format("/{0}/", currentArea);
        }

        public static string GetCurrentRoutePath(object currentArea)
        {
            return GetCurrentRoutePath(GetCurrentAreaRoute(currentArea));
        }

        public static string GetCurrentViewPath(AreaRoute currentArea)
        {
            if (currentArea == AreaRoute.None)
            {
                return string.Format("~/Views/", new object[0]);
            }
            return string.Format("~/Areas/{0}/Themes/{1}/Views/", currentArea, ThemeName);
        }

        public static string GetCurrentViewPath(object currentArea)
        {
            return GetCurrentViewPath(GetCurrentAreaRoute(currentArea));
        }

        public virtual void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AreaRouteFilter());
        }

        public virtual void RegisterIgnoreRoutes(RouteCollection routes)
        {
            RouteTable.Routes.RouteExistingFiles = false;
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("Admin/{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("API/{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("Upload/{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Ajax_Handle/{resource}.ashx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
        }

        public virtual void RegisterRoutes(RouteCollection routes)
        {
            switch (MainAreaRoute)
            {
                case AreaRoute.None:
                {
                    string name = "Default";
                    string url = "{controller}/{action}/{id}";
                    object defaults = new {
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    };
                    string[] namespaces = new string[] { "Maticsoft.Web.Controllers" };
                    routes.MapRoute(name, url, defaults, namespaces).DataTokens.Add("area", "None");
                    return;
                }
                case AreaRoute.CMS:
                {
                    string str3 = "Default";
                    string str4 = "{controller}/{action}/{id}";
                    object obj3 = new {
                        area = "CMS",
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    };
                    string[] strArray4 = new string[] { "Maticsoft.Web.Areas.CMS.*" };
                    routes.MapRoute(str3, str4, obj3, strArray4).DataTokens.Add("area", "CMS");
                    return;
                }
                case AreaRoute.Shop:
                {
                    string str5 = "Default";
                    string str6 = "{controller}/{action}/{id}";
                    object obj4 = new {
                        area = "Shop",
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    };
                    string[] strArray6 = new string[] { "Maticsoft.Web.Areas.Shop.*" };
                    routes.MapRoute(str5, str6, obj4, strArray6).DataTokens.Add("area", "Shop");
                    return;
                }
                case AreaRoute.SNS:
                {
                    string str7 = "Default";
                    string str8 = "{controller}/{action}/{id}";
                    object obj5 = new {
                        area = "SNS",
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    };
                    string[] strArray8 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
                    routes.MapRoute(str7, str8, obj5, strArray8).DataTokens.Add("area", "SNS");
                    return;
                }
                case AreaRoute.Tao:
                {
                    string str9 = "Default";
                    string str10 = "{controller}/{action}/{id}";
                    object obj6 = new {
                        area = "Tao",
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    };
                    string[] strArray10 = new string[] { "Maticsoft.Web.Areas.Tao.*" };
                    routes.MapRoute(str9, str10, obj6, strArray10).DataTokens.Add("area", "Tao");
                    return;
                }
                case AreaRoute.COM:
                {
                    string str13 = "Default";
                    string str14 = "{controller}/{action}/{id}";
                    object obj8 = new {
                        area = "COM",
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    };
                    string[] strArray14 = new string[] { "Maticsoft.Web.Areas.COM.*" };
                    routes.MapRoute(str13, str14, obj8, strArray14).DataTokens.Add("area", "COM");
                    return;
                }
                case AreaRoute.Mobile:
                {
                    string str11 = "Default";
                    string str12 = "{controller}/{action}/{id}";
                    object obj7 = new {
                        area = "Mobile",
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    };
                    string[] strArray12 = new string[] { "Maticsoft.Web.Areas.Mobile.*" };
                    routes.MapRoute(str11, str12, obj7, strArray12).DataTokens.Add("area", "Mobile");
                    return;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                string sessionID = base.Session.SessionID;
                DataTable table = (DataTable) base.Application["OnlineUsers"];
                if (table != null)
                {
                    foreach (DataRow row in table.Select("SessionID='" + sessionID + "'"))
                    {
                        table.Rows.Remove(row);
                    }
                    table.AcceptChanges();
                    base.Application.Lock();
                    base.Application["OnlineUsers"] = table;
                    base.Application.UnLock();
                }
            }
            catch
            {
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            base.Session["Style"] = 1;
            if (!IsInstall)
            {
                if (base.Request.UrlReferrer == null)
                {
                    base.Server.Transfer("/Installer/Default.aspx");
                }
            }
            else
            {
                base.Application["1xtop1_bgimage"] = "images/login1/top-1.gif";
                base.Application["1xtop2_bgimage"] = "images/login1/top-2.gif";
                base.Application["1xtop3_bgimage"] = "images/login1/top-3.gif";
                base.Application["1xtop4_bgimage"] = "images/login1/top-4.gif";
                base.Application["1xtop5_bgimage"] = "images/login1/top-5.gif";
                base.Application["1xtopbj_bgimage"] = "images/login1/top-bj.gif";
                base.Application["1xtopbar_bgimage"] = "images/login1/topbar_01.jpg";
                base.Application["1xfirstpage_bgimage"] = "images/login1/dbsx_01.gif";
                base.Application["1xforumcolor"] = "#f0f4fb";
                base.Application["1xleft_width"] = "204";
                base.Application["1xtree_bgcolor"] = "#e3eeff";
                base.Application["1xleft1_bgimage"] = "images/login1/left-1.gif";
                base.Application["1xleft2_bgimage"] = "images/login1/left-2.gif";
                base.Application["1xleft3_bgimage"] = "images/login1/left-3.gif";
                base.Application["1xleftbj_bgimage"] = "images/login1/left-bj.gif";
                base.Application["1xspliter_color"] = "#6B7DDE";
                base.Application["1xdesktop_bj"] = "";
                base.Application["1xdesktop_bgimage"] = "images/login1/desktop_01.gif";
                base.Application["1xtable_bgcolor"] = "#F5F9FF";
                base.Application["1xtable_bordercolorlight"] = "#CCC";
                base.Application["1xtable_bordercolordark"] = "#CCC";
                base.Application["1xtable_titlebgcolor"] = "#E3EFFF";
                base.Application["1xform_requestcolor"] = "#E78A29";
                base.Application["1xfirstpage_topimage"] = "images/login1/top_01.gif";
                base.Application["1xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
                base.Application["1xfirstpage_middleimage"] = "images/login1/bg_01.gif";
                base.Application["1xabout_bgimage"] = "images/login1/about_01.gif";
                base.Application["2xtop1_bgimage"] = "images/login1/top-1-2.gif";
                base.Application["2xtop2_bgimage"] = "images/login1/top-2-2.gif";
                base.Application["2xtop3_bgimage"] = "images/login1/top-3-2.gif";
                base.Application["2xtop4_bgimage"] = "images/login1/top-4-2.gif";
                base.Application["2xtop5_bgimage"] = "images/login1/top-5-2.gif";
                base.Application["2xtopbj_bgimage"] = "images/login1/top-bj-2.gif";
                base.Application["2xtopbar_bgimage"] = "images/login1/topbar_01.jpg";
                base.Application["2xfirstpage_bgimage"] = "images/login1/dbsx_01.gif";
                base.Application["2xforumcolor"] = "#f0f4fb";
                base.Application["2xleft_width"] = "204";
                base.Application["2xtree_bgcolor"] = "#e3ffe9";
                base.Application["2xleft1_bgimage"] = "images/login1/left-1-2.gif";
                base.Application["2xleft2_bgimage"] = "images/login1/left-2-2.gif";
                base.Application["2xleft3_bgimage"] = "images/login1/left-3-2.gif";
                base.Application["2xleftbj_bgimage"] = "images/login1/left-bj-2.gif";
                base.Application["2xspliter_color"] = "#51C94F";
                base.Application["2xdesktop_bj"] = "";
                base.Application["2xdesktop_bgimage"] = "images/login1/desktop_02.gif";
                base.Application["2xtable_bgcolor"] = "#F5FFF5";
                base.Application["2xtable_bordercolorlight"] = "#7DBD7B";
                base.Application["2xtable_bordercolordark"] = "#D3E0D3";
                base.Application["2xtable_titlebgcolor"] = "#E4FFE3";
                base.Application["2xform_requestcolor"] = "#E78A29";
                base.Application["2xfirstpage_topimage"] = "images/login1/top_01.gif";
                base.Application["2xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
                base.Application["2xfirstpage_middleimage"] = "images/login1/bg_01.gif";
                base.Application["3xtop1_bgimage"] = "images/login1/top-1-1.gif";
                base.Application["3xtop2_bgimage"] = "images/login1/top-2-1.gif";
                base.Application["3xtop3_bgimage"] = "images/login1/top-3-1.gif";
                base.Application["3xtop4_bgimage"] = "images/login1/top-4-1.gif";
                base.Application["3xtop5_bgimage"] = "images/login1/top-5-1.gif";
                base.Application["3xtopbj_bgimage"] = "images/login1/top-bj-1.gif";
                base.Application["3xtopbar_bgimage"] = "images/login1/topbar_01.jpg";
                base.Application["3xfirstpage_bgimage"] = "images/login1/dbsx_01.gif";
                base.Application["3xforumcolor"] = "#f0f4fb";
                base.Application["3xleft_width"] = "204";
                base.Application["3xtree_bgcolor"] = "#ffe3e5";
                base.Application["3xleft1_bgimage"] = "images/login1/left-1-1.gif";
                base.Application["3xleft2_bgimage"] = "images/login1/left-2-1.gif";
                base.Application["3xleft3_bgimage"] = "images/login1/left-3-1.gif";
                base.Application["3xleftbj_bgimage"] = "images/login1/left-bj-1.gif";
                base.Application["3xspliter_color"] = "#C94F4F";
                base.Application["3xdesktop_bj"] = "";
                base.Application["3xdesktop_bgimage"] = "images/login1/desktop_03.gif";
                base.Application["3xtable_bgcolor"] = "#FFF5F5";
                base.Application["3xtable_bordercolorlight"] = "#BD7B7B";
                base.Application["3xtable_bordercolordark"] = "#E1D3D3";
                base.Application["3xtable_titlebgcolor"] = "#FFE3E3";
                base.Application["3xform_requestcolor"] = "#E78A29";
                base.Application["3xfirstpage_topimage"] = "images/login1/top_01.gif";
                base.Application["3xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
                base.Application["3xfirstpage_middleimage"] = "images/login1/bg_01.gif";
                base.Application["4xtop1_bgimage"] = "images/login1/top-1-3.gif";
                base.Application["4xtop2_bgimage"] = "images/login1/top-2-3.gif";
                base.Application["4xtop3_bgimage"] = "images/login1/top-3-3.gif";
                base.Application["4xtop4_bgimage"] = "images/login1/top-4-3.gif";
                base.Application["4xtop5_bgimage"] = "images/login1/top-5-3.gif";
                base.Application["4xtopbj_bgimage"] = "images/login1/top-bj-3.gif";
                base.Application["4xtopbar_bgimage"] = "images/login1/topbar_01.jpg";
                base.Application["4xfirstpage_bgimage"] = "images/login1/dbsx_01.gif";
                base.Application["4xforumcolor"] = "#f0f4fb";
                base.Application["4xleft_width"] = "204";
                base.Application["4xtree_bgcolor"] = "#e3ffe9";
                base.Application["4xleft1_bgimage"] = "images/login1/left-1-3.gif";
                base.Application["4xleft2_bgimage"] = "images/login1/left-2-3.gif";
                base.Application["4xleft3_bgimage"] = "images/login1/left-3-3.gif";
                base.Application["4xleftbj_bgimage"] = "images/login1/left-bj-3.gif";
                base.Application["4xspliter_color"] = "#51C94F";
                base.Application["4xdesktop_bj"] = "";
                base.Application["4xdesktop_bgimage"] = "images/login1/desktop_02.gif";
                base.Application["4xtable_bgcolor"] = "#F5FFF5";
                base.Application["4xtable_bordercolorlight"] = "#7DBD7B";
                base.Application["4xtable_bordercolordark"] = "#D3E0D3";
                base.Application["4xtable_titlebgcolor"] = "#E4FFE3";
                base.Application["4xform_requestcolor"] = "#E78A29";
                base.Application["4xfirstpage_topimage"] = "images/login1/top_01.gif";
                base.Application["4xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
                base.Application["4xfirstpage_middleimage"] = "images/login1/bg_01.gif";
                if (IsInstall)
                {
                    IsAuthorize = AccountsPrincipal.CheckAuthorize(ApplicationOption.AuthorizeCode, ProductInfo);
                }
            }
        }

        public static bool IsAuthorize
        {
            get
            {
                if (ApplicationOption == null)
                {
                    return false;
                }
                if (HttpContext.Current == null)
                {
                    return false;
                }
                bool? nullable = Globals.SafeBool(HttpContext.Current.Session["Maticsoft_IsAuthorize"], (bool?) null);
                if (!nullable.HasValue)
                {
                    nullable = new bool?(AccountsPrincipal.CheckAuthorize(ApplicationOption.AuthorizeCode, ProductInfo));
                    HttpContext.Current.Session["Maticsoft_IsAuthorize"] = nullable;
                }
                return nullable.Value;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Application["Maticsoft_IsAuthorize"] = value;
                }
            }
        }

        protected abstract string MainArea { get; }

        public static string PageFootJs
        {
            get
            {
                if (ApplicationOption != null)
                {
                    return ApplicationOption.PageFootJs;
                }
                return string.Empty;
            }
        }

        public static string SiteName
        {
            get
            {
                if (ApplicationOption != null)
                {
                    return ApplicationOption.SiteName;
                }
                return string.Empty;
            }
        }

        public static string ThemeName
        {
            get
            {
                if (ApplicationOption != null)
                {
                    return ApplicationOption.ThemeName;
                }
                return string.Empty;
            }
        }

        public static string WebPowerBy
        {
            get
            {
                if (ApplicationOption != null)
                {
                    return ApplicationOption.WebPowerBy;
                }
                return string.Empty;
            }
        }

        public static string WebRecord
        {
            get
            {
                if (ApplicationOption != null)
                {
                    return ApplicationOption.WebRecord;
                }
                return string.Empty;
            }
        }
    }
}

