namespace Maticsoft.Web.Admin.SNS.StarType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtCheckRule;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("StarTypeList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtTypeName.Text.Trim().Length == 0)
            {
                msg = msg + @"达人类型（如新晋达人不能为空！\n";
            }
            if (msg != "")
            {
                MessageBox.Show(this, msg);
            }
            else
            {
                string text = this.txtTypeName.Text;
                string str3 = this.txtCheckRule.Text;
                string str4 = this.txtRemark.Text;
                int num = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                Maticsoft.Model.SNS.StarType model = new Maticsoft.Model.SNS.StarType {
                    TypeName = text,
                    CheckRule = str3,
                    Remark = str4,
                    Status = new int?(num)
                };
                Maticsoft.BLL.SNS.StarType type2 = new Maticsoft.BLL.SNS.StarType();
                int num2 = 0;
                num2 = type2.Add(model);
                if (num2 > 0)
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "增加达人类型（ID=" + num2 + "）成功", this);
                    MessageBox.ShowSuccessTip(this, "保存成功！", "StarTypeList.aspx");
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
                return 0x266;
            }
        }
    }
}

