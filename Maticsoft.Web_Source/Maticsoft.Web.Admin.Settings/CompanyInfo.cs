namespace Maticsoft.Web.Admin.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;

    public class CompanyInfo : PageBaseAdmin
    {
        protected int Act_UpdateData = 0xa1;
        protected Button btnReset;
        protected Button btnSave;
        protected HiddenField hfs_ICOPath;
        protected HiddenField HiddenField_ID;
        protected Image imgLogo;
        protected Literal Litera8;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal9;
        protected TextBox txtCompanyAddress;
        protected TextBox txtCompanyFax;
        protected TextBox txtCompanyMail;
        protected TextBox txtCompanyName;
        protected TextBox txtCompanyTelephone;
        protected TextBox txtWebSiteDomain;
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        private void BoundData()
        {
            this.txtWebSiteDomain.Text = Globals.HtmlDecode(this.WebSiteSet.WebSite_Domain);
            this.txtCompanyName.Text = Globals.HtmlDecode(this.WebSiteSet.Company_Name);
            this.txtCompanyAddress.Text = Globals.HtmlDecode(this.WebSiteSet.Company_Address);
            this.txtCompanyTelephone.Text = Globals.HtmlDecode(this.WebSiteSet.Company_Telephone);
            this.txtCompanyFax.Text = Globals.HtmlDecode(this.WebSiteSet.Company_Fax);
            this.txtCompanyMail.Text = Globals.HtmlDecode(this.WebSiteSet.Company_Mail);
            if (!string.IsNullOrWhiteSpace(this.WebSiteSet.WebSite_Logo))
            {
                this.imgLogo.ImageUrl = this.WebSiteSet.WebSite_Logo;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.BoundData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.WebSiteSet.WebSite_Domain = Globals.HtmlEncode(this.txtWebSiteDomain.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Company_Name = Globals.HtmlEncode(this.txtCompanyName.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Company_Address = Globals.HtmlEncode(this.txtCompanyAddress.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Company_Telephone = Globals.HtmlEncode(this.txtCompanyTelephone.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Company_Fax = Globals.HtmlEncode(this.txtCompanyFax.Text.Trim().Replace("\n", ""));
                this.WebSiteSet.Company_Mail = Globals.HtmlEncode(this.txtCompanyMail.Text.Trim().Replace("\n", ""));
                string oldValue = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string newValue = "/Upload/WebSiteLogo";
                ArrayList fileNameList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.hfs_ICOPath.Value))
                {
                    string str3 = string.Format(this.hfs_ICOPath.Value, "");
                    fileNameList.Add(str3.Replace(oldValue, ""));
                    this.WebSiteSet.WebSite_Logo = str3.Replace(oldValue, newValue);
                }
                base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);
                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;
                FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "CompanyInfo.aspx");
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, Site.TooltipTryAgainLater, "CompanyInfo.aspx");
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

        protected override int Act_PageLoad
        {
            get
            {
                return 160;
            }
        }
    }
}

