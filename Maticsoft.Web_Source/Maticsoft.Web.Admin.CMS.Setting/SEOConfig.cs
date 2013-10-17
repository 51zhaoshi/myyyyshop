namespace Maticsoft.Web.Admin.CMS.Setting
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.CMS;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.UI.WebControls;

    public class SEOConfig : PageBaseAdmin
    {
        protected int Act_UpdateData = 0x75;
        private ApplicationKeyType applicationKeyType = ApplicationKeyType.CMS;
        private string[] baseNames = new string[] { "Home" };
        protected Button btnSave;
        private Maticsoft.BLL.CMS.ContentClass classBll = new Maticsoft.BLL.CMS.ContentClass();
        protected DropDownList ddCateImage;
        protected DropDownList ddNewsCate;
        private string[] hasUrlPage = new string[] { "CMS", "CMSSelf" };
        private string[] imageName = new string[0];
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal18;
        protected Literal Literal19;
        protected Literal Literal2;
        protected Literal Literal20;
        protected Literal Literal21;
        protected Literal Literal3;
        protected Literal Literal31;
        protected Literal Literal33;
        protected Literal Literal35;
        protected Literal Literal36;
        protected Literal Literal37;
        protected Literal Literal38;
        protected Literal Literal39;
        protected Literal Literal40;
        protected Literal Literal41;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        private Dictionary<string, PageSetting> pageSettings;
        private string[] selfName = new string[0];
        protected HiddenField TabIndex;
        protected TextBox txtAboutDes;
        protected TextBox txtAboutKeywords;
        protected TextBox txtAboutTitle;
        private string txtAlt = "txt{0}Alt";
        protected TextBox txtCMSDes;
        protected TextBox txtCMSImageAlt;
        protected TextBox txtCMSImageTitle;
        protected TextBox txtCMSKeywords;
        protected TextBox txtCMSSelfDes;
        protected TextBox txtCMSSelfImageAlt;
        protected TextBox txtCMSSelfImageTitle;
        protected TextBox txtCMSSelfKeywords;
        protected TextBox txtCMSSelfTitle;
        protected TextBox txtCMSSelfUrl;
        protected TextBox txtCMSTitle;
        protected TextBox txtCMSUrl;
        protected TextBox txtContactDes;
        protected TextBox txtContactKeywords;
        protected TextBox txtContactTitle;
        private string txtDesId = "txt{0}Des";
        protected TextBox txtHomeDes;
        protected TextBox txtHomeKeywords;
        protected TextBox txtHomeTitle;
        private string txtImageTitle = "txt{0}Title";
        private string txtKeywordsId = "txt{0}Keywords";
        private string txtTitleId = "txt{0}Title";
        private string txtUrl = "txt{0}Url";

        private void BoundData()
        {
            this.LoadPageSetting();
            foreach (string str in this.pageSettings.Keys)
            {
                if (!this.pageSettings[str].IsImage)
                {
                    TextBox txtUrl = null;
                    if (this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtUrl, str)) != null)
                    {
                        txtUrl = this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtUrl, str)) as TextBox;
                    }
                    this.LoadBaseTextBox(str, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtTitleId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtKeywordsId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtDesId, str)) as TextBox, txtUrl);
                    continue;
                }
                this.LoadImageTextBox(str, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtAlt, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtImageTitle, str)) as TextBox);
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
                    if (!this.pageSettings[str].IsImage)
                    {
                        this.SaveTextBox(str, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtTitleId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtKeywordsId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtDesId, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtUrl, str)) as TextBox);
                    }
                    else
                    {
                        this.SaveImageBox(str, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtAlt, str)) as TextBox, this.Page.Master.FindControl("ContentPlaceHolder1").FindControl(string.Format(this.txtImageTitle, str)) as TextBox);
                    }
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

        protected void ddCateImage_IndexChange(object sender, EventArgs e)
        {
            int classID = Globals.SafeInt(this.ddCateImage.SelectedValue, 0);
            if (classID > 0)
            {
                Maticsoft.Model.CMS.ContentClass model = this.classBll.GetModel(classID);
                this.txtCMSSelfImageAlt.Text = model.SeoImageAlt;
                this.txtCMSSelfImageTitle.Text = model.SeoImageTitle;
                this.TabIndex.Value = "3";
            }
        }

        protected void ddNewsCate_IndexChange(object sender, EventArgs e)
        {
            int classID = Globals.SafeInt(this.ddNewsCate.SelectedValue, 0);
            if (classID > 0)
            {
                Maticsoft.Model.CMS.ContentClass model = this.classBll.GetModel(classID);
                this.txtCMSSelfTitle.Text = model.Meta_Title;
                this.txtCMSSelfDes.Text = model.Meta_Description;
                this.txtCMSSelfKeywords.Text = model.Meta_Keywords;
                this.txtCMSSelfUrl.Text = model.SeoUrl;
                this.TabIndex.Value = "1";
            }
        }

        protected void ddProduct_IndexChange(object sender, EventArgs e)
        {
            this.TabIndex.Value = "3";
        }

        private void LoadBaseTextBox(string pageName, TextBox txtTitle, TextBox txtKeyWords, TextBox txtDes, TextBox txtUrl = new TextBox())
        {
            txtTitle.Text = this.pageSettings[pageName].Title;
            txtKeyWords.Text = this.pageSettings[pageName].Keywords;
            txtDes.Text = this.pageSettings[pageName].Description;
            if (txtUrl != null)
            {
                txtUrl.Text = this.pageSettings[pageName].Url;
            }
        }

        private void LoadImageTextBox(string pageName, TextBox txtAlt, TextBox txtImageTitle)
        {
            txtAlt.Text = this.pageSettings[pageName].Alt;
            txtImageTitle.Text = this.pageSettings[pageName].ImageTitle;
        }

        private void LoadPageSetting()
        {
            this.pageSettings = new Dictionary<string, PageSetting>();
            foreach (string str in this.baseNames)
            {
                this.pageSettings.Add(str, new PageSetting(str, this.applicationKeyType, "Base"));
            }
            foreach (string str2 in this.hasUrlPage)
            {
                this.pageSettings.Add(str2, new PageSetting(str2, this.applicationKeyType, "Url"));
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
        }

        private void SaveImageBox(string pageName, TextBox txtAlt, TextBox txtImageTitle)
        {
            this.pageSettings[pageName].Alt = Globals.HtmlEncode(txtAlt.Text.Trim().Replace("\n", ""));
            this.pageSettings[pageName].ImageTitle = Globals.HtmlEncode(txtImageTitle.Text.Trim().Replace("\n", ""));
        }

        private void SaveTextBox(string pageName, TextBox txtTitle, TextBox txtKeyWords, TextBox txtDes, TextBox txtUrl)
        {
            this.pageSettings[pageName].Title = Globals.HtmlEncode(txtTitle.Text.Trim().Replace("\n", ""));
            this.pageSettings[pageName].Keywords = Globals.HtmlEncode(txtKeyWords.Text.Trim().Replace("\n", ""));
            this.pageSettings[pageName].Description = Globals.HtmlEncode(txtDes.Text.Trim().Replace("\n", ""));
            if (txtUrl != null)
            {
                this.pageSettings[pageName].Url = Globals.HtmlEncode(txtUrl.Text.Trim().Replace("\n", ""));
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x74;
            }
        }
    }
}

