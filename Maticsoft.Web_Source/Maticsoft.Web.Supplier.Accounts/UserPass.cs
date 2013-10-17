namespace Maticsoft.Web.Supplier.Accounts
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class UserPass : PageBaseSupplier
    {
        protected Button btnSave;
        protected Label lblMsg;
        protected Label lblName;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox txtOldPassword;
        protected TextBox txtPassword;
        protected TextBox txtPassword1;

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                SiteIdentity identity = new SiteIdentity(base.User.Identity.Name);
                if (identity.TestPassword(this.txtOldPassword.Text) == 0)
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = Site.ErrorPasswprdError;
                }
                else if (this.txtPassword.Text.Trim() != this.txtPassword1.Text.Trim())
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = Site.ErrorPasswprd;
                }
                else
                {
                    User currentUser = base.CurrentUser;
                    currentUser.Password = AccountsPrincipal.EncryptPassword(this.txtPassword.Text);
                    if (!currentUser.Update())
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                    }
                    else
                    {
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && this.Context.User.Identity.IsAuthenticated)
            {
                this.lblName.Text = base.CurrentUser.UserName;
            }
        }
    }
}

