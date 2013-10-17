namespace Maticsoft.Web.Controllers
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Resources;
    using System;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Security;

    public abstract class ControllerBaseUser : Maticsoft.Web.Controllers.ControllerBase
    {
        protected int permissionid = -1;

        protected ControllerBaseUser()
        {
        }

        protected override bool InitializeComponent(ActionExecutingContext filterContext)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = this.RedirectToLogin(filterContext.Result);
                return false;
            }
            try
            {
                base.userPrincipal = new AccountsPrincipal(base.HttpContext.User.Identity.Name);
            }
            catch (IdentityNotMappedException)
            {
                FormsAuthentication.SignOut();
                base.Session.Remove(Globals.SESSIONKEY_USER);
                base.Session.Clear();
                base.Session.Abandon();
                filterContext.Result = this.RedirectToLogin(filterContext.Result);
                return false;
            }
            if (base.Session[Globals.SESSIONKEY_USER] == null)
            {
                base.currentUser = new User(base.userPrincipal);
                base.Session[Globals.SESSIONKEY_USER] = base.currentUser;
                base.Session["Style"] = base.currentUser.Style;
            }
            else
            {
                base.currentUser = (User) base.Session[Globals.SESSIONKEY_USER];
                base.Session["Style"] = base.currentUser.Style;
            }
            if ((base.CurrentUser == null) || (base.CurrentUser.UserType == "AA"))
            {
                filterContext.Result = this.RedirectToLogin(filterContext.Result);
                return false;
            }
            this.ValidatingPermission();
            return true;
        }

        public abstract ActionResult RedirectToLogin(ActionResult result);
        private void ValidatingPermission()
        {
            if (base.HttpContext.User.Identity.IsAuthenticated && (base.userPrincipal != null))
            {
                if ((this.PermissionID != -1) && !base.userPrincipal.HasPermissionID(this.PermissionID))
                {
                    base.Response.Clear();
                    base.Response.Write("<script defer>window.alert('" + Site.TooltipNoPermission + "');history.back();</script>");
                    base.Response.End();
                }
            }
            else
            {
                base.Response.Clear();
                base.Response.Write("<script defer>window.alert('" + Site.TooltipNoAuthenticated + "');parent.location='" + base.DefaultLogin + "';</script>");
                base.Response.End();
            }
        }

        public int PermissionID
        {
            get
            {
                return this.permissionid;
            }
            set
            {
                this.permissionid = value;
            }
        }
    }
}

