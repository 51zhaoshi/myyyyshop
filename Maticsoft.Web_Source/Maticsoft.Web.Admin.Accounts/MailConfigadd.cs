namespace Maticsoft.Web.Admin.Accounts
{
    using Maticsoft.BLL;
    using Maticsoft.Model;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class MailConfigadd : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkPOPSSL;
        protected CheckBox chkSMTPSSL;
        protected Label lblInfo;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RequiredFieldValidator rfvMailaddress;
        protected RequiredFieldValidator rfvPassword;
        protected RequiredFieldValidator rfvSMTPPort;
        protected RequiredFieldValidator rfvSMTPServer;
        protected RequiredFieldValidator rfvUsername;
        protected TextBox txtMailaddress;
        protected TextBox txtPassword;
        protected TextBox txtPOPPort;
        protected TextBox txtPOPServer;
        protected TextBox txtSMTPPort;
        protected TextBox txtSMTPServer;
        protected TextBox txtUsername;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("MailConfiglist.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.MailConfig model = new Maticsoft.Model.MailConfig {
                Mailaddress = this.txtMailaddress.Text,
                Password = this.txtPassword.Text,
                POPPort = Convert.ToInt32(this.txtPOPPort.Text),
                POPServer = this.txtPOPServer.Text,
                POPSSL = this.chkPOPSSL.Checked,
                SMTPPort = Convert.ToInt32(this.txtSMTPPort.Text),
                SMTPServer = this.txtSMTPServer.Text,
                SMTPSSL = this.chkSMTPSSL.Checked,
                Username = this.txtUsername.Text
            };
            if (base.CurrentUser != null)
            {
                model.UserID = base.CurrentUser.UserID;
            }
            Maticsoft.BLL.MailConfig config2 = new Maticsoft.BLL.MailConfig();
            if (!config2.Exists(model.UserID, model.Mailaddress))
            {
                config2.Add(model);
                base.Response.Redirect("mailconfiglist.aspx");
            }
            else
            {
                this.lblInfo.Visible = true;
                this.lblInfo.Text = "This account already exists";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xce;
            }
        }
    }
}

