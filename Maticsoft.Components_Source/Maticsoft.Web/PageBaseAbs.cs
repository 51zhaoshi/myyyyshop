namespace Maticsoft.Web
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Web.UI;

    public abstract class PageBaseAbs : Page
    {
        protected static Hashtable ActHashtab;
        protected User currentUser;
        protected readonly string DefaultLogin;
        protected readonly string DefaultLoginEnterprise;
        protected static IPageBaseMessageTip PageBaseMessageTip;
        protected static IPageBaseOption PageBaseOption;
        public readonly string ToolTipDelete;
        protected AccountsPrincipal userPrincipal;

        public PageBaseAbs(IPageBaseOption option, IPageBaseMessageTip message)
        {
            PageBaseOption = option;
            PageBaseMessageTip = message;
            this.DefaultLogin = PageBaseOption.DefaultLogin;
            this.DefaultLoginEnterprise = PageBaseOption.DefaultLoginEnterprise;
            this.ToolTipDelete = PageBaseMessageTip.TooltipDelConfirm;
        }

        public string GetApprovedText(object Value)
        {
            bool flag = false;
            if (((Value != null) && (Value.ToString().Length > 0)) && (Convert.ToInt32(Value) > 0))
            {
                flag = true;
            }
            if (!flag)
            {
                return ("<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>");
            }
            return ("<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>");
        }

        public string GetboolText(string boolValue)
        {
            if (!(boolValue.Trim().ToLower() == "true"))
            {
                return ("<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>");
            }
            return ("<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>");
        }

        public int GetPermidByActID(int ActionID)
        {
            object obj2 = ActHashtab[ActionID.ToString()];
            if ((obj2 != null) && (obj2.ToString().Length > 0))
            {
                return Convert.ToInt32(obj2);
            }
            return -1;
        }

        protected void GoPage()
        {
            if (!string.IsNullOrWhiteSpace(base.Request.QueryString["return"]))
            {
                base.Response.Redirect(base.Request.QueryString["return"]);
            }
            else
            {
                string str;
                if ((this.currentUser != null) && ((str = this.currentUser.UserType) != null))
                {
                    if (!(str == "AA"))
                    {
                        if (!(str == "UU"))
                        {
                            if (!(str == "EE"))
                            {
                                if (str == "AG")
                                {
                                    base.Response.Redirect("/Agent/index.aspx");
                                }
                                return;
                            }
                            base.Response.Redirect("/Enterprise/index.aspx");
                            return;
                        }
                    }
                    else
                    {
                        base.Response.Redirect("/Admin/Main.htm");
                        return;
                    }
                    base.Response.Redirect("/Member/index.aspx");
                }
            }
        }

        public virtual void InitializeComponent()
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                this.userPrincipal = new AccountsPrincipal(this.Context.User.Identity.Name);
                if (this.Session[Globals.SESSIONKEY_ADMIN] == null)
                {
                    this.currentUser = new User(this.userPrincipal);
                    this.Session[Globals.SESSIONKEY_ADMIN] = this.currentUser;
                    this.Session["Style"] = this.currentUser.Style;
                }
                else
                {
                    this.currentUser = (User) this.Session[Globals.SESSIONKEY_ADMIN];
                    this.Session["Style"] = this.currentUser.Style;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
            base.Error += new EventHandler(this.PageBase_Error);
            ActHashtab = new Actions().GetHashListByCache();
        }

        protected void PageBase_Error(object sender, EventArgs e)
        {
            this.PageError(sender, e);
        }

        protected abstract void PageError(object sender, EventArgs e);
        protected void SignOut(string sessionKey)
        {
            bool isPublicSession = Globals.IsPublicSession;
        }

        public string TranslateToPercent(string str)
        {
            return Convert.ToDecimal(str).ToString("#0.##%", CultureInfo.InvariantCulture);
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

