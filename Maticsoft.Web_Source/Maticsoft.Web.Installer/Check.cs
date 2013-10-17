namespace Maticsoft.Web.Installer
{
    using Maticsoft.Components;
    using System;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Check : Page
    {
        protected Button btnCheck;
        protected HtmlForm form1;
        private const string IMAGEOK = "/Installer/images/ok.gif";
        protected Image imgAdmin;
        protected Image imgConfig;
        protected Image imgContent;
        protected Image imgGravatar;
        protected Image imgNetVersion;
        protected Image imgScripts;
        protected Image imgSqlVersion;
        protected Image imgSystem;
        protected Image imgTemp;
        protected Image imgUpload;
        protected Image imgUser;
        protected HiddenField IsChechPass;
        protected Literal Label3;

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            this.StartCheck();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (MvcApplication.IsInstall)
                {
                    base.Response.Redirect("/", true);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]) && (base.Request.Params["type"] == "accepted"))
                    {
                        this.Session["Install"] = "Accepted";
                    }
                    else if (this.Session["Install"] == null)
                    {
                        base.Response.Redirect("/Installer/Default.aspx");
                    }
                    this.StartCheck();
                }
            }
        }

        private void StartCheck()
        {
            bool flag = true;
            if (((Environment.Version.Major > 4) || ((Environment.Version.Major == 4) && (Environment.Version.Minor > 0))) || (((Environment.Version.Major == 4) && (Environment.Version.Minor == 0)) && (Environment.Version.Build >= 0x766f)))
            {
                this.imgNetVersion.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            if (CheckEnvironment.DoCheckFileSystem("/Upload"))
            {
                this.imgUpload.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            if (CheckEnvironment.DoCheckFileSystem("/Upload/Temp"))
            {
                this.imgTemp.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            if (CheckEnvironment.DoCheckFileSystem("/Upload/User"))
            {
                this.imgUser.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            if (CheckEnvironment.DoCheckFileSystem("/Upload/User/Gravatar"))
            {
                this.imgGravatar.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            if (CheckEnvironment.DoCheckFileSystem("/Scripts"))
            {
                this.imgScripts.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            if (CheckEnvironment.DoCheckFileSystem("/Content"))
            {
                this.imgContent.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            if (CheckEnvironment.DoCheckFileSystem("/Admin"))
            {
                this.imgAdmin.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            FileInfo info = new FileInfo(base.Server.MapPath("/Web.config"));
            info.Refresh();
            if (!info.IsReadOnly)
            {
                this.imgConfig.ImageUrl = "/Installer/images/ok.gif";
            }
            else
            {
                flag = false;
            }
            this.IsChechPass.Value = flag.ToString();
            if (flag)
            {
                this.btnCheck.Visible = false;
                this.Session["Install"] = "Checked";
            }
        }
    }
}

