namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class AddUser : PageBaseAdmin
    {
        public string adminname = "Management";
        protected Button btnCancle;
        protected Button btnSave;
        protected CompareValidator CompareValidator1;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RadioButtonList radbtnlistUserType;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;
        protected TextBox txtEmail;
        protected TextBox txtPassword;
        protected TextBox txtPassword1;
        protected TextBox txtPhone;
        protected TextBox txtTrueName;
        protected TextBox txtUserName;
        private UserType userTypeManage = new UserType();

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UserAdmin.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            User user = new User();
            string msg = "";
            if (user.HasUserByUserName(this.txtUserName.Text))
            {
                msg = msg + Site.TooltipUserExist;
            }
            if (msg != "")
            {
                MessageBox.ShowSuccessTip(this, msg);
            }
            else
            {
                user.UserName = this.txtUserName.Text;
                user.Password = AccountsPrincipal.EncryptPassword(this.txtPassword.Text);
                user.NickName = user.UserName;
                user.TrueName = this.txtTrueName.Text;
                user.Sex = "1";
                user.Phone = this.txtPhone.Text.Trim();
                user.Email = this.txtEmail.Text;
                user.EmployeeID = 0;
                user.Activity = true;
                user.UserType = this.radbtnlistUserType.SelectedValue;
                user.Style = 1;
                user.User_dateCreate = DateTime.Now;
                user.User_iCreator = base.CurrentUser.UserID;
                user.User_dateValid = DateTime.Now;
                user.User_cLang = "zh-CN";
                int num = user.Create();
                if (num == -100)
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUserExist);
                }
                else
                {
                    UsersExp exp = new UsersExp();
                    UsersExpModel model = new UsersExpModel {
                        UserID = num,
                        LastAccessTime = new DateTime?(DateTime.Now),
                        LastLoginTime = DateTime.Now,
                        LastPostTime = new DateTime?(DateTime.Now)
                    };
                    exp.AddUsersExp(model);
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加用户：【{0}】", this.txtUserName.Text), this);
                    base.Response.Redirect("RoleAssignment.aspx?UserID=" + num);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.radbtnlistUserType.DataSource = this.userTypeManage.GetAllList();
                this.radbtnlistUserType.DataTextField = "Description";
                this.radbtnlistUserType.DataValueField = "UserType";
                this.radbtnlistUserType.DataBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xc4;
            }
        }
    }
}

