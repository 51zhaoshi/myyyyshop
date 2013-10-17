namespace Maticsoft.Web.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Web;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections;
    using System.IO;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Security.Principal;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Security;

    public abstract class ControllerBaseAbs : Controller
    {
        protected int Act_AddData = 15;
        protected int Act_ApproveList = 4;
        protected int Act_CloseList = 3;
        protected int Act_DeleteList = 1;
        protected int Act_OpenList = 8;
        protected int Act_SetInvalid = 5;
        protected int Act_SetValid = 6;
        protected int Act_ShowInvalid = 2;
        protected static Hashtable ActHashtab;
        protected static IPageBaseOption ControllerBaseOption;
        protected AreaRoute CurrentArea;
        protected string CurrentThemeName = "Default";
        protected string CurrentThemeViewPath = "Default";
        protected User currentUser;
        protected readonly string DefaultLogin;
        protected bool IncludeProduct = true;
        protected static readonly string P_DATA = Hex16.Decode("00200050006F00770065007200650064002000620079002052A88F6F53538D8A");
        protected int UserAlbumDetailType = -1;
        protected AccountsPrincipal userPrincipal;

        public ControllerBaseAbs(IPageBaseOption option)
        {
            ControllerBaseOption = option;
            this.DefaultLogin = ControllerBaseOption.DefaultLogin;
        }

        protected abstract void ControllerException(ExceptionContext filterContext);
        public int GetPermidByActID(int ActionID)
        {
            if (ActHashtab != null)
            {
                object obj2 = ActHashtab[ActionID.ToString()];
                if ((obj2 != null) && (obj2.ToString().Length > 0))
                {
                    return Convert.ToInt32(obj2);
                }
            }
            return -1;
        }

        protected virtual bool InitializeComponent(ActionExecutingContext filterContext)
        {
            if (base.HttpContext.User.Identity.IsAuthenticated)
            {
                try
                {
                    this.userPrincipal = new AccountsPrincipal(base.HttpContext.User.Identity.Name);
                }
                catch (IdentityNotMappedException)
                {
                    FormsAuthentication.SignOut();
                    base.Session.Remove(Globals.SESSIONKEY_USER);
                    base.Session.Clear();
                    base.Session.Abandon();
                    return false;
                }
                if (base.Session[Globals.SESSIONKEY_USER] == null)
                {
                    this.currentUser = new User(this.userPrincipal);
                    base.Session[Globals.SESSIONKEY_USER] = this.currentUser;
                    base.Session["Style"] = this.currentUser.Style;
                }
                else
                {
                    this.currentUser = (User) base.Session[Globals.SESSIONKEY_USER];
                    base.Session["Style"] = this.currentUser.Style;
                    ((dynamic) base.ViewBag).UserType = this.currentUser.UserType;
                }
                ((dynamic) base.ViewBag).CurrentUid = this.currentUser.UserID;
            }
            return true;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!MvcApplication.IsInstall)
            {
                filterContext.Result = this.Redirect("/Installer/Default.aspx");
            }
            else
            {
                this.CurrentArea = MvcApplication.GetCurrentAreaRoute(filterContext.RouteData.DataTokens["area"]);
                ((dynamic) base.ViewBag).BasePath = MvcApplication.GetCurrentRoutePath(this.CurrentArea);
                this.CurrentThemeName = MvcApplication.ThemeName;
                this.CurrentThemeName = Directory.Exists(filterContext.HttpContext.Server.MapPath(string.Concat(new object[] { "/Areas/", MvcApplication.MainAreaRoute, "/Themes/", this.CurrentThemeName }))) ? this.CurrentThemeName : "Default";
                this.CurrentThemeViewPath = MvcApplication.GetCurrentViewPath(this.CurrentArea);
                if (this.CurrentThemeName == "TufenXiang")
                {
                    this.IncludeProduct = false;
                    this.UserAlbumDetailType = 0;
                }
                ((dynamic) base.ViewBag).SiteName = MvcApplication.SiteName;
                ((dynamic) base.ViewBag).CurrentUid = 0;
                if (this.InitializeComponent(filterContext))
                {
                    ActHashtab = new Actions().GetHashListByCache();
                    if (((ActHashtab != null) && (this.UserPrincipal != null)) && !this.UserPrincipal.HasPermissionID(this.GetPermidByActID(this.Act_DeleteList)))
                    {
                        ((dynamic) base.ViewBag).DeleteAuthority = true;
                    }
                    base.OnActionExecuting(filterContext);
                }
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            this.ControllerException(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                base.OnResultExecuted(filterContext);
            }
            else
            {
                if (!MvcApplication.IsAuthorize)
                {
                    string str = ((dynamic) filterContext.Controller.ViewBag).Title as string;
                    if (!str.Contains(P_DATA))
                    {
                        filterContext.Result = base.RedirectToAction("ERROR");
                    }
                }
                base.OnResultExecuted(filterContext);
            }
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                base.OnResultExecuting(filterContext);
            }
            else
            {
                string str = filterContext.RequestContext.HttpContext.Response.Output.NewLine + "        ";
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />{0}", str);
                if (!MvcApplication.IsAuthorize)
                {
                    dynamic viewBag = base.ViewBag;
                    string str2 = P_DATA;
                    if (<OnResultExecuting>o__SiteContainer8.<>p__Site9 == null)
                    {
                        <OnResultExecuting>o__SiteContainer8.<>p__Site9 = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "Title", typeof(ControllerBaseAbs)));
                    }
                    if (!<OnResultExecuting>o__SiteContainer8.<>p__Site9.Target(<OnResultExecuting>o__SiteContainer8.<>p__Site9, viewBag))
                    {
                        if (<OnResultExecuting>o__SiteContainer8.<>p__Siteb == null)
                        {
                            <OnResultExecuting>o__SiteContainer8.<>p__Siteb = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(ControllerBaseAbs), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                        }
                        viewBag.Title = <OnResultExecuting>o__SiteContainer8.<>p__Siteb.Target(<OnResultExecuting>o__SiteContainer8.<>p__Siteb, viewBag.Title, str2);
                    }
                    else
                    {
                        viewBag.add_Title(str2);
                    }
                    builder.AppendFormat("<title>{1}</title>{0}", str, ((dynamic) base.ViewBag).Title);
                    builder.AppendFormat("<meta name=\"generator\" content=\"{0} {1}\" />{2}", MvcApplication.ProductInfo, MvcApplication.Version, str);
                    builder.AppendFormat("<meta name=\"author\" content=\"动软卓越（北京）科技有限公司\" />{0}", str);
                    builder.AppendFormat("<meta name=\"copyright\" content=\"2011-{0} Maticsoft Inc.\" />{1}", DateTime.Now.Year, str);
                }
                else
                {
                    builder.AppendFormat("<title>{1}</title>{0}", str, ((dynamic) base.ViewBag).Title);
                }
                builder.AppendFormat("<meta name=\"keywords\" content=\"{1}\">{0}<meta name=\"description\" content=\"{2}\">{0}", str, ((dynamic) base.ViewBag).Keywords, ((dynamic) base.ViewBag).Description);
                switch (Globals.SafeEnum<AreaRoute>(filterContext.RouteData.DataTokens["area"].ToString(), AreaRoute.None, false))
                {
                    case AreaRoute.CMS:
                        builder.AppendFormat("<link href=\"/Areas/CMS/Themes/" + MvcApplication.ThemeName + "/Content/Css/Site.css\" rel=\"stylesheet\" type=\"text/css\" />", new object[0]);
                        break;
                }
                ((dynamic) base.ViewBag).BaseHead = builder.ToString();
                base.OnResultExecuting(filterContext);
            }
        }

        public User CurrentUser
        {
            get
            {
                return this.currentUser;
            }
        }

        public AccountsPrincipal UserPrincipal
        {
            get
            {
                return this.userPrincipal;
            }
        }
    }
}

