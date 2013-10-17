namespace Maticsoft.Web.Supplier
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Login : Page
    {
        protected ImageButton btnLogin;
        protected HtmlInputText CheckCode;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Image ImageCheck;
        protected Label lblGUID;
        protected Label lblMsg;
        protected TextBox txtPass;
        protected TextBox txtUsername;

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            if ((this.Session["CheckCode"] != null) && (this.Session["CheckCode"].ToString() != ""))
            {
                if (this.Session["CheckCode"].ToString().ToLower() != this.CheckCode.Value.ToLower())
                {
                    this.lblMsg.Text = "验证码错误!";
                    this.Session["CheckCode"] = null;
                    return;
                }
                this.Session["CheckCode"] = null;
            }
            else
            {
                base.Response.Redirect("Login.aspx");
            }
            string userName = PageValidate.InputText(this.txtUsername.Text.Trim(), 30);
            string password = PageValidate.InputText(this.txtPass.Text.Trim(), 30);
            AccountsPrincipal existingPrincipal = AccountsPrincipal.ValidateLogin(userName, password);
            if (existingPrincipal != null)
            {
                User user = new User(existingPrincipal);
                if (user.UserType != "SP")
                {
                    this.lblMsg.Text = "您不是供应商不能从该入口登录！";
                }
                else
                {
                    this.Context.User = existingPrincipal;
                    if (((SiteIdentity) base.User.Identity).TestPassword(password) != 0)
                    {
                        if (!user.Activity)
                        {
                            MessageBox.Show(this, "对不起，该帐号尚未激活，请联系管理员！");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(userName, false);
                            this.Session[Globals.SESSIONKEY_SUPPLIER] = user;
                            this.Session["Style"] = user.Style;
                            LogHelp.AddUserLog(user.UserName, user.UserType, "登录成功", this);
                            if (this.Session["returnPage"] != null)
                            {
                                string url = this.Session["returnPage"].ToString();
                                this.Session["returnPage"] = null;
                                base.Response.Redirect(url);
                            }
                            else
                            {
                                base.Response.Redirect("index.html");
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            this.lblMsg.Text = "密码错误！";
                            LogHelp.AddUserLog(userName, "", this.lblMsg.Text, this);
                        }
                        catch
                        {
                            base.Response.Redirect("/Member/Login.aspx");
                        }
                    }
                }
            }
            else
            {
                this.lblMsg.Text = "登录失败，请确认用户名或密码是否正确。";
                LogHelp.AddUserLog(userName, "", "登录失败!", this);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigHelper.GetConfigBool("LocalTest"))
            {
                AccountsPrincipal existingPrincipal = AccountsPrincipal.ValidateLogin("sp", "1");
                User user = new User(existingPrincipal);
                this.Context.User = existingPrincipal;
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                this.Session[Globals.SESSIONKEY_SUPPLIER] = user;
                this.Session["Style"] = user.Style;
                MessageBox.ShowSuccessTip(this, "自动登录成功, 正在为您跳转..", "index.html");
            }
        }
    }
}

