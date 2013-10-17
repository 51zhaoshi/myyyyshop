namespace Maticsoft.Web.Admin.Accounts
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Userinfo : PageBaseAdmin
    {
        protected Button btnSave;
        protected Label lblEmail;
        protected Label lblMsg;
        protected Label lblName;
        protected Label lblTruename;
        protected Label lblUserIP;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal8;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UserModify.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && this.Context.User.Identity.IsAuthenticated)
            {
                User currentUser = base.CurrentUser;
                this.lblName.Text = currentUser.UserName;
                this.lblTruename.Text = currentUser.TrueName;
                this.lblEmail.Text = currentUser.Email;
                this.lblUserIP.Text = base.Request.UserHostAddress;
            }
        }
    }
}

