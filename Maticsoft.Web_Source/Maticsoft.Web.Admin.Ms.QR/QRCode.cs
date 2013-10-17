namespace Maticsoft.Web.Admin.Ms.QR
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using System;
    using System.IO;
    using System.Net;
    using System.Web;
    using System.Web.UI.WebControls;

    public class QRCode : PageBaseAdmin
    {
        private readonly string _uploadFolder = string.Format("/{0}/QR/", MvcApplication.UploadFolder);
        private readonly string _uploadFolderMapPath;
        private readonly string androidImg;
        private const ApplicationKeyType applicationKeyType = ApplicationKeyType.Mobile;
        protected Button btnGen;
        protected Button btnReset;
        protected DropDownList droImgFormat;
        protected DropDownList drpFaultRate;
        protected Image imgResult;
        private const string KEY_ANDROID = "QR_URL_ANDROID";
        private const string KEY_WEBSITE = "QR_URL_WEBSITE";
        protected Literal Literal3;
        protected Literal Literal4;
        protected RadioButtonList rdoMod;
        protected TextBox txtAndroidURL;
        protected TextBox txtContent;
        protected TextBox txtMargin;
        protected TextBox txtSize;
        protected TextBox txtWebsiteURL;
        private readonly string websiteImg;

        public QRCode()
        {
            this._uploadFolderMapPath = HttpContext.Current.Server.MapPath(this._uploadFolder);
            this.websiteImg = this._uploadFolderMapPath + "website.png";
            this.androidImg = this._uploadFolderMapPath + "android.png";
        }

        protected void btnGen_Click(object sender, EventArgs e)
        {
            string str = (!this.txtWebsiteURL.Text.StartsWith("http") ? "http://" : "") + this.txtWebsiteURL.Text;
            string str2 = (!this.txtAndroidURL.Text.StartsWith("http") ? "http://" : "") + this.txtAndroidURL.Text;
            if (str == "http://")
            {
                str = string.Empty;
            }
            if (str2 == "http://")
            {
                str2 = string.Empty;
            }
            ConfigSystem.Modify("QR_URL_WEBSITE", str, "QR_URL_WEBSITE", ApplicationKeyType.Mobile);
            ConfigSystem.Modify("QR_URL_ANDROID", str2, "QR_URL_ANDROID", ApplicationKeyType.Mobile);
            int num = Globals.SafeInt(this.txtSize.Text, -1);
            if (num < 0)
            {
                this.txtSize.Text = (num = 200).ToString();
            }
            string selectedValue = this.drpFaultRate.SelectedValue;
            string str4 = this.droImgFormat.SelectedValue;
            int num2 = Globals.SafeInt(this.txtMargin.Text, -1);
            if (num2 < 0)
            {
                this.txtMargin.Text = (num2 = 4).ToString();
            }
            string format = string.Format("/tools/qr/gen.aspx?margin={0}&size={1}&level={2}&format={3}&content={4}", new object[] { num2, num, selectedValue, str4, "{0}" });
            if (!Directory.Exists(this._uploadFolderMapPath))
            {
                Directory.CreateDirectory(this._uploadFolderMapPath);
            }
            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    if (System.IO.File.Exists(this.websiteImg))
                    {
                        System.IO.File.Delete(this.websiteImg);
                    }
                }
                else
                {
                    str = "http://" + Globals.DomainFullName + string.Format(format, Globals.UrlEncode(str));
                    client.DownloadFile(str, this.websiteImg);
                }
                if (string.IsNullOrWhiteSpace(str2))
                {
                    if (System.IO.File.Exists(this.androidImg))
                    {
                        System.IO.File.Delete(this.androidImg);
                    }
                }
                else
                {
                    str2 = "http://" + Globals.DomainFullName + string.Format(format, Globals.UrlEncode(str2));
                    client.DownloadFile(str2, this.androidImg);
                }
            }
            catch (Exception exception)
            {
                MessageBox.ShowFailTip(this, "二维码生成失败! " + exception.Message);
                return;
            }
            base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.Mobile);
            this.ShowInfo();
            MessageBox.ShowSuccessTip(this, "二维码生成成功!");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            this.txtWebsiteURL.Text = ConfigSystem.GetValueByCache("QR_URL_WEBSITE", ApplicationKeyType.Mobile);
            this.txtAndroidURL.Text = ConfigSystem.GetValueByCache("QR_URL_ANDROID", ApplicationKeyType.Mobile);
            if (string.IsNullOrWhiteSpace(this.txtWebsiteURL.Text))
            {
                this.txtWebsiteURL.Text = "http://";
            }
            if (string.IsNullOrWhiteSpace(this.txtAndroidURL.Text))
            {
                this.txtAndroidURL.Text = "http://";
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x143;
            }
        }
    }
}

