namespace Maticsoft.Web.Accounts
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Drawing;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class SetPass : PageBaseAdmin
    {
        protected Button btnCancel;
        protected Button btnUpdate;
        protected CompareValidator CompareValidator1;
        protected HtmlForm Form1;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox txtPassword;
        protected TextBox txtPassword1;
        protected TextBox txtUserName;

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtPassword.Text = "";
            this.txtPassword1.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string text = this.txtUserName.Text;
                string password = this.txtPassword.Text;
                User user = new User();
                if (!user.SetPassword(text, password))
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = SysManage.TooltipUpdateFail;
                }
                else
                {
                    this.lblMsg.ForeColor = Color.Blue;
                    this.lblMsg.Text = SysManage.TooltipUpdateSucceed;
                }
            }
        }

        private void InitializeComponent()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xcc;
            }
        }
    }
}

