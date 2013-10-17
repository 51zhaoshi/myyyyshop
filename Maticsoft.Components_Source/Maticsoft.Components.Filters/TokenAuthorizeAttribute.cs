namespace Maticsoft.Components.Filters
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited=true, AllowMultiple=true)]
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        private const string LockKey = "LOCKKEY";
        public const int STATUSCODE_ERROR_INTERNAL = 0x325;
        public const int STATUSCODE_ERROR_PARAM = 0x326;
        public const int STATUSCODE_UNAUTHORIZED = 0x323;
        public const int STATUSCODE_UNLOGON = 0x321;

        public TokenAuthorizeAttribute()
        {
            this.RequiredType = -1;
            this.PermissionId = -1;
        }

        public TokenAuthorizeAttribute(AccountType userType)
        {
            this.RequiredType = userType;
            this.PermissionId = -1;
        }

        public TokenAuthorizeAttribute(int permissionId)
        {
            this.RequiredType = -1;
            this.PermissionId = permissionId;
        }

        public TokenAuthorizeAttribute(AccountType userType, int permissionId)
        {
            this.RequiredType = userType;
            this.PermissionId = permissionId;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            AccountsPrincipal principal;
            AccountType requiredType = this.RequiredType;
            int permissionId = this.PermissionId;
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.StatusCode = 0x321;
                return false;
            }
            if (!Enum.IsDefined(typeof(AccountType), requiredType))
            {
                httpContext.Response.StatusCode = 0x326;
                return false;
            }
            try
            {
                principal = new AccountsPrincipal(httpContext.User.Identity.Name);
            }
            catch (IdentityNotMappedException)
            {
                FormsAuthentication.SignOut();
                if (httpContext.Session != null)
                {
                    httpContext.Session.Remove(Globals.SESSIONKEY_USER);
                    httpContext.Session.Clear();
                    httpContext.Session.Abandon();
                }
                httpContext.Response.StatusCode = 0x321;
                return false;
            }
            User user = null;
            if (httpContext.Session[Globals.SESSIONKEY_USER] == null)
            {
                user = new User(principal);
                httpContext.Session[Globals.SESSIONKEY_USER] = user;
            }
            else
            {
                user = (User) httpContext.Session[Globals.SESSIONKEY_USER];
            }
            if (requiredType != -1)
            {
                switch (user.UserType)
                {
                    case "UU":
                        if (requiredType != AccountType.User)
                        {
                            httpContext.Response.StatusCode = 0x323;
                            return false;
                        }
                        goto Label_01CA;

                    case "AA":
                        if (requiredType != AccountType.Admin)
                        {
                            httpContext.Response.StatusCode = 0x323;
                            return false;
                        }
                        goto Label_01CA;

                    case "EE":
                        if (requiredType != AccountType.Enterprise)
                        {
                            httpContext.Response.StatusCode = 0x323;
                            return false;
                        }
                        goto Label_01CA;

                    case "AG":
                        if (requiredType != AccountType.Agent)
                        {
                            httpContext.Response.StatusCode = 0x323;
                            return false;
                        }
                        goto Label_01CA;
                }
                httpContext.Response.StatusCode = 0x326;
                return false;
            }
        Label_01CA:
            if ((permissionId != -1) && !principal.HasPermissionID(permissionId))
            {
                httpContext.Response.StatusCode = 0x323;
                return false;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            switch (filterContext.HttpContext.Response.StatusCode)
            {
                case 0x321:
                    filterContext.Result = this.RedirectToLogon(filterContext);
                    return;

                case 0x323:
                    filterContext.Result = this.RedirectToUnauthorized(filterContext.HttpContext);
                    return;

                case 0x326:
                    filterContext.Result = this.RedirectToErrorParam(filterContext.HttpContext);
                    return;
            }
            base.HandleUnauthorizedRequest(filterContext);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            lock ("LOCKKEY")
            {
                base.OnAuthorization(filterContext);
            }
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }

        public void OnException(ExceptionContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        internal bool PerformAuthorizeCore(HttpContextBase httpContext)
        {
            return this.AuthorizeCore(httpContext);
        }

        public virtual ActionResult RedirectToErrorParam(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(0x326);
            }
            return new HttpNotFoundResult("TokenAuthorizeAttribute-IllegalUserType");
        }

        public virtual ActionResult RedirectToLogon(AuthorizationContext filterContext)
        {
            if (((filterContext == null) || (filterContext.HttpContext == null)) || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(0x321);
            }
            string currentRoutePath = MvcApplication.GetCurrentRoutePath(filterContext.RouteData.DataTokens["area"]);
            if (filterContext.HttpContext.Request.Url != null)
            {
                return new RedirectResult(currentRoutePath + "Account/Login?ReturnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.PathAndQuery));
            }
            return new RedirectResult((currentRoutePath + "Account/Login"));
        }

        public virtual ActionResult RedirectToUnauthorized(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(0x323);
            }
            return new HttpUnauthorizedResult("您的权限不足, 无法访问此页面!");
        }

        public int PermissionId { get; set; }

        public AccountType RequiredType { get; set; }
    }
}

