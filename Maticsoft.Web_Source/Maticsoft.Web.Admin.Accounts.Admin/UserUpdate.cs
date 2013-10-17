namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class UserUpdate : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkActive;
        protected DropDownList dropUserType;
        protected Label lblMsg;
        protected Label lblName;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RangeValidator RangeValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;
        protected TextBox txtEmail;
        protected TextBox txtEmployeeID;
        protected TextBox txtPassword;
        protected TextBox txtPassword1;
        protected TextBox txtPhone;
        protected TextBox txtTrueName;
        public string userid = "";
        private UserType userTypeManage = new UserType();

        private void BindRoles(AccountsPrincipal user)
        {
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UserAdmin.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string userName = this.lblName.Text.Trim();
            AccountsPrincipal existingPrincipal = new AccountsPrincipal(userName);
            User user = new User(existingPrincipal) {
                UserName = userName,
                TrueName = this.txtTrueName.Text.Trim()
            };
            if (this.txtPassword.Text.Trim() != "")
            {
                user.Password = AccountsPrincipal.EncryptPassword(this.txtPassword.Text);
            }
            user.UserType = this.dropUserType.SelectedValue;
            user.Phone = this.txtPhone.Text.Trim();
            user.Email = this.txtEmail.Text.Trim();
            if (this.txtEmployeeID.Text.Length > 0)
            {
                user.EmployeeID = Convert.ToInt32(this.txtEmployeeID.Text);
            }
            else
            {
                user.EmployeeID = -1;
            }
            user.Activity = !this.chkActive.Checked;
            if (!user.Update())
            {
                this.lblMsg.ForeColor = Color.Red;
                this.lblMsg.Text = Site.TooltipUpdateError;
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("编辑用户：【{0}】", userName), this);
                base.Response.Redirect("useradmin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["userid"] != null)) && (base.Request.Params["userid"].ToString() != ""))
            {
                User user = new User(int.Parse(base.Request["userid"]));
                if (user == null)
                {
                    base.Response.Write("<script language=javascript>window.alert('" + Site.TooltipUserExist + @"\');history.back();</script>");
                }
                else
                {
                    this.dropUserType.DataSource = this.userTypeManage.GetAllList();
                    this.dropUserType.DataTextField = "Description";
                    this.dropUserType.DataValueField = "UserType";
                    this.dropUserType.DataBind();
                    this.lblName.Text = user.UserName;
                    this.txtTrueName.Text = user.TrueName;
                    this.txtPhone.Text = user.Phone;
                    this.txtEmail.Text = user.Email;
                    if (user.EmployeeID > 0)
                    {
                        this.txtEmployeeID.Text = user.EmployeeID.ToString();
                    }
                    this.dropUserType.SelectedValue = user.UserType;
                    this.chkActive.Checked = !user.Activity;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xc5;
            }
        }
    }
}

