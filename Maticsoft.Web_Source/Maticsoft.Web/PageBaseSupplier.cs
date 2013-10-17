namespace Maticsoft.Web
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Resources;
    using System;
    using System.Data.SqlClient;
    using System.Web.Security;

    public class PageBaseSupplier : PageBase
    {
        protected int permissionid = -1;

        private string GetSqlExceptionMessage(int number)
        {
            string errorMessageSQL = Site.ErrorMessageSQL;
            switch (number)
            {
                case 0x11:
                    return Site.ErrorMessageSQL17;

                case 0x223:
                    return Site.ErrorMessageSQL547;

                case 0x4b5:
                    return Site.ErrorMessageSQL1205;

                case 0xfdc:
                    return Site.ErrorMessageSQL4060;

                case 0x4818:
                    return Site.ErrorMessageSQL18456;

                case 0xa29:
                    return Site.ErrorMessageSQL2601;

                case 0xa43:
                    return Site.ErrorMessageSQL2627;
            }
            return Site.ErrorMessageSQL;
        }

        public override void InitializeComponent()
        {
            if (this.Context.User.Identity.IsAuthenticated && (this.Session[Globals.SESSIONKEY_SUPPLIER] != null))
            {
                base.currentUser = (User) this.Session[Globals.SESSIONKEY_SUPPLIER];
                this.Session["Style"] = base.currentUser.Style;
                if (base.currentUser.UserType != "SP")
                {
                    FormsAuthentication.SignOut();
                    this.Session.Clear();
                    this.Session.Abandon();
                    base.Response.Clear();
                    base.Response.Write("<script defer>parent.location='" + DefaultLoginSupplier + "';</script>");
                    base.Response.End();
                }
                else
                {
                    base.userPrincipal = new AccountsPrincipal(base.currentUser.UserName);
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ValidatingPermission();
        }

        protected override void PageError(object sender, EventArgs e)
        {
            string str = "";
            Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog();
            Exception lastError = base.Server.GetLastError();
            if (lastError is SqlException)
            {
                SqlException exception2 = (SqlException) lastError;
                if (exception2 != null)
                {
                    string sqlExceptionMessage = this.GetSqlExceptionMessage(exception2.Number);
                    if (exception2.Number == 0x223)
                    {
                        string str3 = str;
                        str = str3 + "<h1 class=\"SystemTip\">" + Site.ErrorSystemTip + "</h1><br/> <font class=\"ErrorPageText\">" + sqlExceptionMessage + "</font>";
                    }
                    else
                    {
                        string str4 = str;
                        str = str4 + "<h1 class=\"ErrorMessage\">" + Site.ErrorSystemTip + "</h1><hr/> 该信息已被系统记录，请稍后重试或与管理员联系。<br/>错误信息： <font class=\"ErrorPageText\">" + sqlExceptionMessage + "</font>";
                        model.Loginfo = sqlExceptionMessage;
                        model.StackTrace = lastError.ToString();
                        model.Url = base.Request.Url.AbsoluteUri;
                    }
                }
            }
            else
            {
                string str5 = str;
                str = str5 + "<h1 class=\"ErrorMessage\">" + Site.ErrorSystemTip + "</h1><hr/> 该信息已被系统记录，请稍后重试或与管理员联系。<br/>错误信息： <font class=\"ErrorPageText\">" + lastError.Message.ToString() + "<hr/><b>Stack Trace:</b><br/>" + lastError.StackTrace + "</font>";
                model.Loginfo = lastError.Message;
                model.StackTrace = lastError.StackTrace;
                model.Url = base.Request.Url.AbsoluteUri;
            }
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            this.Session["ErrorMsg"] = str;
            base.Server.Transfer("~/Supplier/ErrorPage.aspx", true);
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
                base.Response.Redirect(DefaultLoginSupplier);
            }
        }

        public static string DefaultLoginSupplier
        {
            get
            {
                string valueByCache = ConfigSystem.GetValueByCache("DefaultLoginSupplier");
                if (string.IsNullOrWhiteSpace(valueByCache))
                {
                    valueByCache = "/supplier/login.aspx";
                }
                return valueByCache;
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

        public int SupplierId
        {
            get
            {
                return Globals.SafeInt(base.currentUser.DepartmentID, -1);
            }
        }
    }
}

