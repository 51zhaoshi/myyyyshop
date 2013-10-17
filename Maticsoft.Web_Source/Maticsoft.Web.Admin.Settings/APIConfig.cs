namespace Maticsoft.Web.Admin.Settings
{
    using Maticsoft.BLL.Shop;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class APIConfig : PageBaseAdmin
    {
        protected int Act_UpdateData = 0x71;
        private const ApplicationKeyType applicationKeyType = ApplicationKeyType.OpenAPI;
        protected string BaiduStr = string.Empty;
        protected Button btnReset;
        protected Button btnSave;
        protected CheckBox chkIsOpenR;
        protected string QQStr = string.Empty;
        private Maticsoft.BLL.Shop.TaoBaoConfig ShopTaoConfig = new Maticsoft.BLL.Shop.TaoBaoConfig(ApplicationKeyType.OpenAPI);
        protected string ShopTaoStr = string.Empty;
        protected string SinaStr = string.Empty;
        private Maticsoft.BLL.SNS.TaoBaoConfig TaoBaoConfig = new Maticsoft.BLL.SNS.TaoBaoConfig(ApplicationKeyType.OpenAPI);
        protected string TaoCodeStr = string.Empty;
        protected string TaokeStr = string.Empty;
        protected string TencentStr = string.Empty;
        protected TextBox txtApiUrl;
        protected TextBox txtAppKey;
        protected TextBox txtAppsecret;
        protected TextBox txtBaiDuPushApiKey;
        protected TextBox txtBaiDuPushSecretKey;
        protected TextBox txtDengluAPIKEY;
        protected TextBox txtDengluAPPID;
        protected TextBox txtQQAPPID;
        protected TextBox txtQQAPPKEY;
        protected TextBox txtShopApiUrl;
        protected TextBox txtShopAppKey;
        protected TextBox txtShopAppsecret;
        protected TextBox txtSinaAppKey;
        protected TextBox txtSinaAppSercet;
        protected TextBox txtSinaCallBack;
        protected TextBox txtTaoCallback;
        protected TextBox txtTaoCode;
        protected TextBox txtTencentAppId;
        protected TextBox txtTencentSercet;
        protected TextBox txtYouKuAPI;
        protected string VideoStr = string.Empty;

        private void BoundData()
        {
            this.txtDengluAPPID.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DengluAPPID", ApplicationKeyType.OpenAPI);
            this.txtDengluAPIKEY.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("DengluAPIKEY", ApplicationKeyType.OpenAPI);
            this.txtSinaAppKey.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaAppId", ApplicationKeyType.OpenAPI);
            this.txtSinaAppSercet.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaSercet", ApplicationKeyType.OpenAPI);
            this.txtQQAPPID.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQAppId", ApplicationKeyType.OpenAPI);
            this.txtQQAPPKEY.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQSercet", ApplicationKeyType.OpenAPI);
            this.txtTencentAppId.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentAppId", ApplicationKeyType.OpenAPI);
            this.txtTencentSercet.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentSercet", ApplicationKeyType.OpenAPI);
            this.txtAppKey.Text = Globals.HtmlDecode(this.TaoBaoConfig.TaoBaoAppkey);
            this.txtAppsecret.Text = Globals.HtmlDecode(this.TaoBaoConfig.TaobaoAppsecret);
            this.txtApiUrl.Text = Globals.HtmlDecode(this.TaoBaoConfig.TaobaoApiUrl);
            this.txtShopAppKey.Text = Globals.HtmlDecode(this.ShopTaoConfig.TaoBaoAppkey);
            this.txtShopAppsecret.Text = Globals.HtmlDecode(this.ShopTaoConfig.TaobaoAppsecret);
            this.txtShopApiUrl.Text = Globals.HtmlDecode(this.ShopTaoConfig.TaobaoApiUrl);
            this.chkIsOpenR.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Product_OpenRedirect");
            this.txtBaiDuPushApiKey.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("API_BaiDuPushApiKey", ApplicationKeyType.OpenAPI);
            this.txtBaiDuPushSecretKey.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("API_BaiDuPushSecretKey", ApplicationKeyType.OpenAPI);
            this.txtYouKuAPI.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("YouKuAPI", ApplicationKeyType.OpenAPI);
            this.txtTaoCode.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_SNS_TaoBaoCode", ApplicationKeyType.OpenAPI);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.BoundData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.TaoBaoConfig.TaoBaoAppkey = Globals.HtmlEncode(this.txtAppKey.Text);
                this.TaoBaoConfig.TaobaoAppsecret = Globals.HtmlEncode(this.txtAppsecret.Text);
                this.TaoBaoConfig.TaobaoApiUrl = Globals.HtmlEncode(this.txtApiUrl.Text);
                this.ShopTaoConfig.TaoBaoAppkey = Globals.HtmlEncode(this.txtShopAppKey.Text);
                this.ShopTaoConfig.TaobaoAppsecret = Globals.HtmlEncode(this.txtShopAppsecret.Text);
                this.ShopTaoConfig.TaobaoApiUrl = Globals.HtmlEncode(this.txtShopApiUrl.Text);
                Maticsoft.BLL.SysManage.ConfigSystem.Modify("SNS_Product_OpenRedirect", this.chkIsOpenR.Checked.ToString(), "分享商品开启本地重定向跳转，True 为确认开启，False 为不开启，默认为不开启", ApplicationKeyType.SNS);
                this.EditKey("YouKuAPI", this.txtYouKuAPI.Text, "优酷视频接口");
                this.EditKey("Social_SinaAppId", this.txtSinaAppKey.Text, "新浪媒体SinaAppKey");
                this.EditKey("Social_SinaSercet", this.txtSinaAppSercet.Text, "新浪媒体SinaAppSerce");
                this.EditKey("Social_QQAppId", this.txtQQAPPID.Text, "QQ媒体QQAPPID");
                this.EditKey("Social_QQSercet", this.txtQQAPPKEY.Text, "QQ媒体txtQQAPPKEY");
                this.EditKey("Social_TencentAppId", this.txtTencentAppId.Text, "腾讯微博媒体txtTencentAppId");
                this.EditKey("Social_TencentSercet", this.txtTencentSercet.Text, "腾讯微博媒体Social_TencentSercet");
                Maticsoft.BLL.SysManage.ConfigSystem.Modify("OpenAPI_SNS_TaoBaoCode", this.txtTaoCode.Text, "淘点金代码", ApplicationKeyType.OpenAPI);
                this.EditKey("API_BaiDuPushApiKey", this.txtBaiDuPushApiKey.Text, "百度云推送API_BaiDuPushApiKey");
                this.EditKey("API_BaiDuPushSecretKey", this.txtBaiDuPushSecretKey.Text, "百度云推送API_BaiDuPushSecretKey");
                base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.SNS);
                base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.OpenAPI);
                base.Cache.Remove("ConfigSystemHashList_" + this.TaoBaoConfig.applicationKeyType);
                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "设置三方登录、淘宝客、百度云推送、视频接口成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "APIConfig.aspx");
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, Site.TooltipTryAgainLater, "APIConfig.aspx");
            }
        }

        public bool EditKey(string key, string value, string description)
        {
            return Maticsoft.BLL.SysManage.ConfigSystem.Modify(key, value, description, ApplicationKeyType.OpenAPI);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.btnSave.Visible = false;
            }
            if (!base.IsPostBack)
            {
                this.TaokeStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_TaokeApi_IsShow") ? this.TaokeStr : "display: none";
                this.SinaStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_SinaApi_IsShow") ? this.SinaStr : "display: none";
                this.QQStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_QQApi_IsShow") ? this.QQStr : "display: none";
                this.TencentStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_TencentApi_IsShow") ? this.TencentStr : "display: none";
                this.BaiduStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_BaiduApi_IsShow") ? this.BaiduStr : "display: none";
                this.VideoStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_VideoApi_IsShow") ? this.VideoStr : "display: none";
                this.ShopTaoStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_ShopTaoApi_IsShow") ? this.ShopTaoStr : "display: none";
                this.TaoCodeStr = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_TaoCode_IsShow") ? this.TaoCodeStr : "display: none";
                this.txtSinaCallBack.Text = "http://" + Globals.DomainFullName + "/social/sinacallback";
                this.txtTaoCallback.Text = "http://" + Globals.DomainFullName + "/Social/TaoBaoCallback";
                this.BoundData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x70;
            }
        }
    }
}

