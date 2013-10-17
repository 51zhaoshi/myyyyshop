namespace Maticsoft.Web.Controls
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using System;
    using System.Web.Security;
    using System.Web.UI;

    [Obsolete]
    public class checkright : UserControl
    {
        public int PermissionID = -1;

        [Obsolete]
        private void InitializeComponent()
        {
            if (!this.Page.IsPostBack)
            {
                if (!this.Context.User.Identity.IsAuthenticated)
                {
                    string valueByCache = ConfigSystem.GetValueByCache("DefaultLoginAdmin");
                    FormsAuthentication.SignOut();
                    base.Session.Clear();
                    base.Session.Abandon();
                    base.Response.Clear();
                    base.Response.Write(@"<script defer>window.alert('You do not have permission to access this page or session expired！\n Please login again or contact your administrator！');parent.location='" + valueByCache + "';</script>");
                    base.Response.End();
                }
                else if (base.Session[Globals.SESSIONKEY_ADMIN] != null)
                {
                    AccountsPrincipal principal = new AccountsPrincipal(((User) base.Session[Globals.SESSIONKEY_ADMIN]).UserName);
                    if ((this.PermissionID != -1) && !principal.HasPermissionID(this.PermissionID))
                    {
                        base.Response.Clear();
                        base.Response.Write(@"<script defer>window.alert('You do not have permission to access this page！\n Please login again or contact your administrator');history.back();</script>");
                        base.Response.End();
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

