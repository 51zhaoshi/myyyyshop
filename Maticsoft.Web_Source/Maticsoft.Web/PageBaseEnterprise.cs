namespace Maticsoft.Web
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Resources;
    using System;
    using System.Web.Security;

    public class PageBaseEnterprise : PageBase
    {
        protected int permissionid = -1;

        public override void InitializeComponent()
        {
            if (this.Context.User.Identity.IsAuthenticated && (this.Session[Globals.SESSIONKEY_ENTERPRISE] != null))
            {
                base.currentUser = (User) this.Session[Globals.SESSIONKEY_ENTERPRISE];
                this.Session["Style"] = base.currentUser.Style;
                base.userPrincipal = new AccountsPrincipal(base.currentUser.UserName);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ValidatingPermission();
        }

        private void ValidatingPermission()
        {
            if (this.Context.User.Identity.IsAuthenticated && (base.userPrincipal != null))
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
                FormsAuthentication.SignOut();
                this.Session.Clear();
                this.Session.Abandon();
                base.Response.Redirect(base.DefaultLoginEnterprise);
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

