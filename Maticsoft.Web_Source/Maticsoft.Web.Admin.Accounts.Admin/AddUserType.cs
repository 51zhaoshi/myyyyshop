namespace Maticsoft.Web.Admin.Accounts.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;

    public class AddUserType : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Label lblMsg;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal6;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected TextBox txtDescription;
        protected TextBox txtUserType;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("UserTypeAdmin.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            UserType type = new UserType();
            string s = this.txtUserType.Text.Trim();
            if (Encoding.GetEncoding("gb2312").GetBytes(s).Length > 2)
            {
                MessageBox.ShowFailTip(this, "输入的用户类型不能超过两个字符");
            }
            else
            {
                string str2 = this.txtDescription.Text.Trim();
                if (!string.IsNullOrWhiteSpace(s) && !string.IsNullOrWhiteSpace(str2))
                {
                    string msg = "";
                    DataSet list = type.GetList("UserType='" + s + "'");
                    if ((list != null) && (list.Tables[0].Rows.Count > 0))
                    {
                        msg = msg + Site.TooltipUserTypeExist;
                    }
                    if (msg != "")
                    {
                        MessageBox.ShowSuccessTip(this, msg);
                    }
                    else
                    {
                        type.Add(s, str2);
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Format("添加用户类别用户类别：【{0}】", s), this);
                        MessageBox.ResponseScript(this, "parent.location.href='UserTypeAdmin.aspx'");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 200;
            }
        }
    }
}

