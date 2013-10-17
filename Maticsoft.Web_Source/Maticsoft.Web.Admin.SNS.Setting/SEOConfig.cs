namespace Maticsoft.Web.Admin.SNS.Setting
{
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.SNS;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class SEOConfig : PageBaseAdmin
    {
        protected int Act_UpdateData = 0x264;
        private ApplicationKeyType applicationKeyType = ApplicationKeyType.SNS;
        protected Button btnSave;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal18;
        protected Literal Literal19;
        protected Literal Literal2;
        protected Literal Literal20;
        protected Literal Literal21;
        protected Literal Literal217;
        protected Literal Literal22;
        protected Literal Literal23;
        protected Literal Literal24;
        protected Literal Literal25;
        protected Literal Literal26;
        protected Literal Literal27;
        protected Literal Literal28;
        protected Literal Literal29;
        protected Literal Literal3;
        protected Literal Literal30;
        protected Literal Literal31;
        protected Literal Literal32;
        protected Literal Literal33;
        protected Literal Literal34;
        protected Literal Literal35;
        protected Literal Literal36;
        protected Literal Literal37;
        protected Literal Literal38;
        protected Literal Literal39;
        protected Literal Literal4;
        protected Literal Literal40;
        protected Literal Literal41;
        protected Literal Literal42;
        protected Literal Literal43;
        protected Literal Literal44;
        protected Literal Literal45;
        protected Literal Literal46;
        protected Literal Literal47;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        private string[] pageNames = new string[] { "Home", "BlogDetail", "BlogList", "VideoDetail", "VideoList", "ProductList", "ProductDetail", "PhotoList", "PhotoDetail", "Group", "GroupList", "GroupDetail", "Ablum", "AblumList", "AblumDetail", "Star" };
        private Dictionary<string, IPageSetting> pageSettings;
        protected TextBox txtAblumDes;
        protected TextBox txtAblumDetailDes;
        protected TextBox txtAblumDetailKeywords;
        protected TextBox txtAblumDetailTitle;
        protected TextBox txtAblumKeywords;
        protected TextBox txtAblumListDes;
        protected TextBox txtAblumListKeywords;
        protected TextBox txtAblumListTitle;
        protected TextBox txtAblumTitle;
        protected TextBox txtBlogDetailDes;
        protected TextBox txtBlogDetailKeywords;
        protected TextBox txtBlogDetailTitle;
        protected TextBox txtBlogListDes;
        protected TextBox txtBlogListKeywords;
        protected TextBox txtBlogListTitle;
        private string txtDesId = "txt{0}Des";
        protected TextBox txtGroupDes;
        protected TextBox txtGroupDetailDes;
        protected TextBox txtGroupDetailKeywords;
        protected TextBox txtGroupDetailTitle;
        protected TextBox txtGroupKeywords;
        protected TextBox txtGroupListDes;
        protected TextBox txtGroupListKeywords;
        protected TextBox txtGroupListTitle;
        protected TextBox txtGroupTitle;
        protected TextBox txtHomeDes;
        protected TextBox txtHomeKeywords;
        protected TextBox txtHomeTitle;
        private string txtKeywordsId = "txt{0}Keywords";
        protected TextBox txtPhotoDetailDes;
        protected TextBox txtPhotoDetailKeywords;
        protected TextBox txtPhotoDetailTitle;
        protected TextBox txtPhotoListDes;
        protected TextBox txtPhotoListKeywords;
        protected TextBox txtPhotoListTitle;
        protected TextBox txtProductDetailDes;
        protected TextBox txtProductDetailKeywords;
        protected TextBox txtProductDetailTitle;
        protected TextBox txtProductListDes;
        protected TextBox txtProductListKeywords;
        protected TextBox txtProductListTitle;
        protected TextBox txtStarDes;
        protected TextBox txtStarKeywords;
        protected TextBox txtStarTitle;
        private string txtTitleId = "txt{0}Title";
        protected TextBox txtVideoDetailDes;
        protected TextBox txtVideoDetailKeywords;
        protected TextBox txtVideoDetailTitle;
        protected TextBox txtVideoListDes;
        protected TextBox txtVideoListKeywords;
        protected TextBox txtVideoListTitle;

        private void BoundData()
        {
            this.LoadPageSetting();
            foreach (string str in this.pageSettings.Keys)
            {
                this.LoadTextBox(str, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtTitleId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtKeywordsId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtDesId, str)) as TextBox);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.BoundData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.LoadPageSetting();
            try
            {
                foreach (string str in this.pageSettings.Keys)
                {
                    this.SaveTextBox(str, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtTitleId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtKeywordsId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtDesId, str)) as TextBox);
                }
                base.Cache.Remove("ConfigSystemHashList_" + this.applicationKeyType);
                this.btnSave.Enabled = false;
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "设置SEO数据成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "SEOConfig.aspx");
            }
            catch (Exception)
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "设置SEO数据失败", this);
                MessageBox.ShowFailTip(this, Site.TooltipTryAgainLater, "SEOConfig.aspx");
            }
        }

        private void LoadPageSetting()
        {
            this.pageSettings = new Dictionary<string, IPageSetting>();
            foreach (string str in this.pageNames)
            {
                this.pageSettings.Add(str, new PageSetting(str, this.applicationKeyType));
            }
        }

        private void LoadTextBox(string pageName, TextBox txtTitle, TextBox txtKeyWords, TextBox txtDes)
        {
            txtTitle.Text = this.pageSettings[pageName].Title;
            txtKeyWords.Text = this.pageSettings[pageName].Keywords;
            txtDes.Text = this.pageSettings[pageName].Description;
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
        }

        private void SaveTextBox(string pageName, TextBox txtTitle, TextBox txtKeyWords, TextBox txtDes)
        {
            this.pageSettings[pageName].Title = Globals.HtmlEncode(txtTitle.Text.Trim().Replace("\n", ""));
            this.pageSettings[pageName].Keywords = Globals.HtmlEncode(txtKeyWords.Text.Trim().Replace("\n", ""));
            this.pageSettings[pageName].Description = Globals.HtmlEncode(txtDes.Text.Trim().Replace("\n", ""));
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x263;
            }
        }
    }
}

