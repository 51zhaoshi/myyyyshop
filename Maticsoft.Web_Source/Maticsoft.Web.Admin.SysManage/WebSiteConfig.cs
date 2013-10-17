namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;

    public class WebSiteConfig : PageBaseAdmin
    {
        protected int Act_UpdateData = 0xa1;
        protected Button btnReset;
        protected Button btnSave;
        protected CheckBox chk_OpenLogin;
        protected CheckBox chk_OpenRegister;
        protected CheckBox chk_OpenRegisterSendEmail;
        protected CheckBox chk_ThumbAddWater;
        protected HiddenField hfs_ICOPath;
        protected HiddenField HiddenField_ID;
        protected Image imgLogo;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        public string SeoSetting = "";
        protected TextBox txtAuthorize;
        protected TextBox txtBaiduShareUserId;
        protected TextBox txtBaseHost;
        protected TextBox txtCopyRight;
        protected TextBox txtDes;
        protected TextBox txtKeyWords;
        protected TextBox txtPageFootJs;
        protected TextBox txtTitle;
        protected TextBox txtWebRecord;
        protected TextBox txtWebSiteName;
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        private void BoundData()
        {
            this.txtWebSiteName.Text = Globals.HtmlDecode(this.WebSiteSet.WebName);
            this.txtBaseHost.Text = Globals.HtmlDecode(this.WebSiteSet.BaseHost);
            this.txtCopyRight.Text = Globals.HtmlDecode(this.WebSiteSet.WebPowerBy);
            if (!string.IsNullOrWhiteSpace(this.WebSiteSet.LogoPath))
            {
                this.imgLogo.ImageUrl = this.WebSiteSet.LogoPath;
            }
            this.txtWebRecord.Text = Globals.HtmlDecode(this.WebSiteSet.WebRecord);
            this.txtPageFootJs.Text = this.WebSiteSet.PageFootJs;
            this.txtAuthorize.Text = this.WebSiteSet.AuthorizeCode;
            this.txtBaiduShareUserId.Text = this.WebSiteSet.BaiduShareUserId;
            this.chk_OpenLogin.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            this.chk_OpenRegister.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Register");
            this.chk_OpenRegisterSendEmail.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_RegisterEmailCheck");
            this.chk_ThumbAddWater.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_ThumbImage_AddWater");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.BoundData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.WebSiteSet.WebName = Globals.HtmlEncode(this.txtWebSiteName.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.BaseHost = Globals.HtmlEncode(this.txtBaseHost.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.WebPowerBy = Globals.HtmlEncode(this.txtCopyRight.Text.Trim().Replace("\n", ""));
                string oldValue = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string newValue = "/Upload/WebSiteLogo";
                ArrayList fileNameList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.hfs_ICOPath.Value))
                {
                    string str3 = string.Format(this.hfs_ICOPath.Value, "");
                    fileNameList.Add(str3.Replace(oldValue, ""));
                    this.WebSiteSet.LogoPath = str3.Replace(oldValue, newValue);
                }
                this.WebSiteSet.WebRecord = Globals.HtmlEncode(this.txtWebRecord.Text.Trim().Replace("\n", "").Trim());
                this.WebSiteSet.AuthorizeCode = this.txtAuthorize.Text.Trim().Replace("\n", "");
                this.WebSiteSet.PageFootJs = this.txtPageFootJs.Text.Trim();
                this.WebSiteSet.BaiduShareUserId = this.txtBaiduShareUserId.Text.Trim();
                this.UpdateKey("System_Close_Login", this.chk_OpenLogin.Checked.ToString(), "是否关闭社区的用户登录功能");
                this.UpdateKey("System_Close_Register", this.chk_OpenRegister.Checked.ToString(), "是否关闭社区的用户注册功能");
                this.UpdateKey("System_Close_RegisterEmailCheck", this.chk_OpenRegisterSendEmail.Checked.ToString(), "是否关闭商城注册邮件验证功能");
                base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);
                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;
                FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "WebSiteConfig.aspx");
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, Site.TooltipTryAgainLater, "WebSiteConfig.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    this.btnSave.Visible = false;
                }
                this.BoundData();
            }
            if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.CMS)
            {
                this.SeoSetting = "/Admin/CMS/Setting/SEOConfig.aspx";
            }
            if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
            {
                this.SeoSetting = "/Admin/SNS/Setting/SEOConfig.aspx";
            }
        }

        public bool UpdateKey(string keyName, string value, string desc)
        {
            return Maticsoft.BLL.SysManage.ConfigSystem.Modify(keyName, value, desc, ApplicationKeyType.Shop);
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 160;
            }
        }
    }
}

