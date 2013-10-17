namespace Maticsoft.Web.Admin.Accounts
{
    using Maticsoft.BLL;
    using Maticsoft.Common;
    using Maticsoft.Common.DEncrypt;
    using Maticsoft.Model;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class MailConfigInfoOld : PageBaseAdmin
    {
        protected int Act_UpdateData = 0xa3;
        private Maticsoft.BLL.MailConfig bll = new Maticsoft.BLL.MailConfig();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkSMTPSSL;
        protected HiddenField HiddenField_ID;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected RequiredFieldValidator rfvMailaddress;
        protected RequiredFieldValidator rfvPassword;
        protected RequiredFieldValidator rfvSMTPPort;
        protected RequiredFieldValidator rfvSMTPServer;
        protected RequiredFieldValidator rfvUsername;
        protected TextBox txtMailaddress;
        protected TextBox txtPassword;
        protected TextBox txtSMTPPort;
        protected TextBox txtSMTPServer;
        protected TextBox txtUsername;

        private void BoundData()
        {
            Maticsoft.Model.MailConfig model = this.bll.GetModel();
            if (model != null)
            {
                this.txtSMTPServer.Text = model.SMTPServer;
                this.txtSMTPPort.Text = model.SMTPPort.ToString();
                this.txtMailaddress.Text = model.Mailaddress;
                this.txtPassword.Attributes.Add("value", DESEncrypt.Decrypt(model.Password));
                this.txtUsername.Text = model.Username;
                if (model.SMTPSSL)
                {
                    this.chkSMTPSSL.Checked = true;
                }
                else
                {
                    this.chkSMTPSSL.Checked = false;
                }
                this.HiddenField_ID.Value = model.ID.ToString();
            }
            else
            {
                this.txtSMTPServer.Text = "";
                this.txtSMTPPort.Text = "25";
                this.txtMailaddress.Text = "";
                this.txtUsername.Text = "";
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            this.BoundData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.MailConfig model = this.bll.GetModel();
            if (model != null)
            {
                model.Mailaddress = this.txtMailaddress.Text;
                model.Password = DESEncrypt.Encrypt(this.txtPassword.Text);
                model.SMTPPort = Convert.ToInt32(this.txtSMTPPort.Text);
                model.SMTPServer = this.txtSMTPServer.Text;
                model.SMTPSSL = this.chkSMTPSSL.Checked;
                model.Username = this.txtUsername.Text;
                this.bll.Update(model);
                MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
            }
            else
            {
                Maticsoft.Model.MailConfig config2 = new Maticsoft.Model.MailConfig {
                    Mailaddress = this.txtMailaddress.Text,
                    Password = DESEncrypt.Encrypt(this.txtPassword.Text),
                    SMTPPort = Convert.ToInt32(this.txtSMTPPort.Text),
                    SMTPServer = this.txtSMTPServer.Text,
                    SMTPSSL = this.chkSMTPSSL.Checked,
                    Username = this.txtUsername.Text
                };
                if (base.CurrentUser != null)
                {
                    config2.UserID = base.CurrentUser.UserID;
                }
                new Maticsoft.BLL.MailConfig();
                if (!this.bll.Exists(config2.UserID, config2.Mailaddress))
                {
                    this.bll.Add(config2);
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                }
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
                return 0xa2;
            }
        }
    }
}

