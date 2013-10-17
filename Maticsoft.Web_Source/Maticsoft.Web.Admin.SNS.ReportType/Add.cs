namespace Maticsoft.Web.Admin.SNS.ReportType
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
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            Maticsoft.BLL.SNS.ReportType type = new Maticsoft.BLL.SNS.ReportType();
            string text = this.txtTypeName.Text;
            string str2 = this.txtRemark.Text;
            if (text.Length == 0)
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "举报类型的名称不能为空！");
            }
            else if (type.Exists(text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "举报类型的名称已存在！");
            }
            else if (str2.Length > 50)
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "备注不能超过50个字符！");
            }
            else
            {
                Maticsoft.Model.SNS.ReportType model = new Maticsoft.Model.SNS.ReportType {
                    TypeName = text,
                    Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0),
                    Remark = str2
                };
                if (type.Add(model) > 0)
                {
                    MessageBox.ShowSuccessTipScript(this, "添加举报类型成功", "window.parent.location.reload();");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加举报类型成功", this);
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
                return 0x260;
            }
        }
    }
}

