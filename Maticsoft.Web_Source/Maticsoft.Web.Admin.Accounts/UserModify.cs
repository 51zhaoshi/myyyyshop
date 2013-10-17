namespace Maticsoft.Web.Admin.Accounts
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class UserModify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Label lblMsg;
        protected Label lblName;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal8;
        protected RequiredFieldValidator RequiredFieldValidator3;
        protected TextBox txtEmail;
        protected TextBox txtTrueName;

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Userinfo.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                string userName = this.lblName.Text.Trim();
                AccountsPrincipal existingPrincipal = new AccountsPrincipal(userName);
                User user = new User(existingPrincipal) {
                    UserName = userName,
                    TrueName = this.txtTrueName.Text.Trim(),
                    Email = this.txtEmail.Text.Trim()
                };
                if (!user.Update())
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && this.Context.User.Identity.IsAuthenticated)
            {
                User currentUser = base.CurrentUser;
                this.lblName.Text = currentUser.UserName;
                this.txtTrueName.Text = currentUser.TrueName;
                this.txtEmail.Text = currentUser.Email;
            }
        }
    }
}

