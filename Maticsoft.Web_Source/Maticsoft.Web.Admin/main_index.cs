namespace Maticsoft.Web.Admin
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class main_index : PageBaseAdmin
    {
        public string CurrentUserName = string.Empty;
        protected HtmlForm form1;
        public string GetDateTime = string.Empty;
        protected Literal litDotNetVersion;
        protected Literal LitLastLoginTime;
        protected Literal litOperatingSystem;
        protected Literal litProductLine;
        protected Literal litServerDomain;
        protected Literal litWebServerVersion;
        private UsersExp uBll = new UsersExp();
        private UsersExpModel uModel = new UsersExpModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(base.CurrentUser.TrueName))
                {
                    this.CurrentUserName = base.CurrentUser.TrueName;
                }
                else
                {
                    this.CurrentUserName = base.CurrentUser.UserName;
                }
                if ((DateTime.Now.Hour > 6) && (DateTime.Now.Hour < 12))
                {
                    this.GetDateTime = "早上好";
                }
                else if ((DateTime.Now.Hour >= 12) && (DateTime.Now.Hour < 0x12))
                {
                    this.GetDateTime = "下午好";
                }
                else
                {
                    this.GetDateTime = "晚上好";
                }
                this.uModel = this.uBll.GetUsersExpModel(base.CurrentUser.UserID);
                if (this.uModel != null)
                {
                    this.LitLastLoginTime.Text = this.uModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    this.LitLastLoginTime.Text = base.CurrentUser.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                this.litProductLine.Text = Maticsoft.Components.MvcApplication.ProductInfo + " " + Maticsoft.Components.MvcApplication.Version;
                this.litOperatingSystem.Text = SystemInfo.OperatingSystemSimple;
                this.litServerDomain.Text = SystemInfo.ServerDomain;
                this.litDotNetVersion.Text = SystemInfo.DotNetVersion.ToString();
                this.litWebServerVersion.Text = SystemInfo.WebServerVersion;
            }
        }
    }
}

