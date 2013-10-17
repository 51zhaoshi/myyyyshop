namespace Maticsoft.Web.Admin.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class BasicConfig : PageBaseAdmin
    {
        protected int Act_UpdateData = 0xa1;
        protected Button btnReset;
        protected Button btnSave;
        protected DropDownList DateFormat;
        protected DropDownList ForeLanguage;
        protected TextBox ImageSizes;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected TextBox NormalImgHeight;
        protected TextBox NormalImgWidth;
        protected TextBox ThumbImgHeight;
        protected TextBox ThumbImgWidth;
        protected DropDownList TimeFormat;
        protected DropDownList TimeZone;
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.System);
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSetShop = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.Shop);

        private void BoundDate()
        {
            this.ForeLanguage.Text = Globals.HtmlDecode(this.WebSiteSet.ForeGround_Language);
            this.TimeZone.Text = Globals.HtmlDecode(this.WebSiteSet.Timezone_Information);
            this.TimeFormat.Text = Globals.HtmlDecode(this.WebSiteSet.Time_Format);
            this.DateFormat.Text = Globals.HtmlDecode(this.WebSiteSet.Date_Format);
            this.ImageSizes.Text = Globals.HtmlDecode(this.WebSiteSetShop.Shop_ImageSizes);
            this.ThumbImgWidth.Text = Globals.HtmlDecode(this.WebSiteSetShop.Shop_ThumbImageWidth);
            this.ThumbImgHeight.Text = Globals.HtmlDecode(this.WebSiteSetShop.Shop_ThumbImageHeight);
            this.NormalImgWidth.Text = Globals.HtmlDecode(this.WebSiteSetShop.Shop_NormalImageWidth);
            this.NormalImgHeight.Text = Globals.HtmlDecode(this.WebSiteSetShop.Shop_NormalImageHeight);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.BoundDate();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.WebSiteSet.ForeGround_Language = Globals.HtmlEncode(this.ForeLanguage.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Timezone_Information = Globals.HtmlEncode(this.TimeZone.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Time_Format = Globals.HtmlEncode(this.TimeFormat.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Date_Format = Globals.HtmlEncode(this.DateFormat.Text.Trim().Replace("\n", ""));
                this.WebSiteSetShop.Shop_ImageSizes = Globals.HtmlEncode(this.ImageSizes.Text.Trim().Replace("\n", ""));
                this.WebSiteSetShop.Shop_ThumbImageWidth = Globals.HtmlEncode(this.ThumbImgWidth.Text.Trim().Replace("\n", ""));
                this.WebSiteSetShop.Shop_ThumbImageHeight = Globals.HtmlEncode(this.ThumbImgHeight.Text.Trim().Replace("\n", ""));
                this.WebSiteSetShop.Shop_NormalImageWidth = Globals.HtmlEncode(this.NormalImgWidth.Text.Trim().Replace("\n", ""));
                this.WebSiteSetShop.Shop_NormalImageHeight = Globals.HtmlEncode(this.NormalImgHeight.Text.Trim().Replace("\n", ""));
                base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);
                base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.Shop);
                this.btnSave.Enabled = false;
                this.btnReset.Enabled = false;
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "BasicConfig.aspx");
            }
            catch
            {
                MessageBox.ShowFailTip(this, Site.TooltipTryAgainLater, "BasicConfig.aspx");
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
                this.BoundDate();
            }
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

