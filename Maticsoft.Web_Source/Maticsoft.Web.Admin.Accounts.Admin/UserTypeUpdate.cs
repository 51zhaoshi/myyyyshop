namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class UserTypeUpdate : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox txtDescription;
        protected TextBox txtUserType;
        private Maticsoft.Accounts.Bus.UserType userTypeManage = new Maticsoft.Accounts.Bus.UserType();

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UserTypeAdmin.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Accounts.Bus.UserType type = new Maticsoft.Accounts.Bus.UserType();
            string str = this.txtUserType.Text.Trim();
            string str2 = this.txtDescription.Text.Trim();
            if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(str2))
            {
                type.Update(str, str2);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑用户类别用户类别：【{0}】", str), this);
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ResponseScript(this, "parent.location.href='UserTypeAdmin.aspx'");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string userType = this.UserType;
                if (!string.IsNullOrWhiteSpace(userType))
                {
                    this.txtUserType.Text = userType;
                    this.txtUserType.Enabled = false;
                    this.txtDescription.Text = this.userTypeManage.GetDescription(userType);
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xc9;
            }
        }

        public string UserType
        {
            get
            {
                return base.Request.Params["UserType"];
            }
        }
    }
}

