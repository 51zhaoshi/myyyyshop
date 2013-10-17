namespace Maticsoft.Web.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Common.DEncrypt;
    using Maticsoft.Components;
    using Maticsoft.Web;
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Login : Page
    {
        protected ImageButton btnLogin;
        protected HtmlInputText CheckCode;
        protected DropDownList dropLanguage;
        protected HtmlForm form1;
        protected Label lblGUID;
        protected Label lblMsg;
        protected TextBox txtPass;
        protected TextBox txtUsername;

        public void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            if (((this.Session["PassErrorCountAdmin"] != null) && (this.Session["PassErrorCountAdmin"].ToString() != "")) && (Convert.ToInt32(this.Session["PassErrorCountAdmin"]) > 3))
            {
                this.txtUsername.Enabled = false;
                this.txtPass.Enabled = false;
                this.btnLogin.Enabled = false;
                this.lblMsg.Text = "对不起，你已经登录错误三次，系统锁定，请联系管理员！";
            }
            else
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
                    if (user.UserType != "AA")
                    {
                        this.lblMsg.Text = "您非管理员用户，您没有权限登录后台系统！";
                    }
                    else
                    {
                        this.Context.User = existingPrincipal;
                        if (((SiteIdentity) base.User.Identity).TestPassword(password) != 0)
                        {
                            if (!user.Activity)
                            {
                                MessageBox.ShowSuccessTip(this, "对不起，该帐号已被冻结，请联系管理员！");
                            }
                            else
                            {
                                FormsAuthentication.SetAuthCookie(userName, false);
                                this.Session[Globals.SESSIONKEY_ADMIN] = user;
                                this.Session["Style"] = user.Style;
                                LogHelp.AddUserLog(user.UserName, user.UserType, "登录成功", this);
                                string selectedValue = this.dropLanguage.SelectedValue;
                                this.Session["language"] = selectedValue;
                                HttpCookie cookie = new HttpCookie("language") {
                                    Value = selectedValue,
                                    Expires = DateTime.MaxValue
                                };
                                base.Response.AppendCookie(cookie);
                                if (this.Session["returnPage"] != null)
                                {
                                    string url = this.Session["returnPage"].ToString();
                                    this.Session["returnPage"] = null;
                                    base.Response.Redirect(url);
                                }
                                else
                                {
                                    base.Response.Redirect("main.htm");
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
                                base.Response.Redirect("Login.aspx");
                            }
                        }
                    }
                }
                else
                {
                    this.lblMsg.Text = "登录失败，请确认用户名或密码是否正确。";
                    if ((this.Session["PassErrorCountAdmin"] != null) && (this.Session["PassErrorCountAdmin"].ToString() != ""))
                    {
                        int num2 = Convert.ToInt32(this.Session["PassErrorCountAdmin"]);
                        this.Session["PassErrorCountAdmin"] = num2 + 1;
                    }
                    else
                    {
                        this.Session["PassErrorCountAdmin"] = 1;
                    }
                    LogHelp.AddUserLog(userName, "", "登录失败!", this);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Maticsoft.Components.MvcApplication.IsInstall)
            {
                base.Response.Redirect("/Installer/Default.aspx");
            }
            else
            {
                this.Page.Title = "动软系统框架-系统登录" + (!Maticsoft.Components.MvcApplication.IsAuthorize ? Hex16.Decode("00200050006F00770065007200650064002000620079002052A88F6F53538D8A") : "");
                if (ConfigHelper.GetConfigBool("LocalTest"))
                {
                    AccountsPrincipal existingPrincipal = AccountsPrincipal.ValidateLogin("admin", "1");
                    User user = new User(existingPrincipal);
                    this.Context.User = existingPrincipal;
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    this.Session[Globals.SESSIONKEY_ADMIN] = user;
                    this.Session["Style"] = user.Style;
                    this.Session["language"] = "zh-CN";
                    HttpCookie cookie = new HttpCookie("language") {
                        Value = "zh-CN",
                        Expires = DateTime.MaxValue
                    };
                    base.Response.AppendCookie(cookie);
                    MessageBox.ShowSuccessTip(this, "自动登录成功, 正在为您跳转..", "main.htm");
                }
            }
        }
    }
}

