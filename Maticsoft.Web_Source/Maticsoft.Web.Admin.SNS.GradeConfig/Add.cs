namespace Maticsoft.Web.Admin.SNS.GradeConfig
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    [Obsolete]
    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected TextBox txtGradeName;
        protected TextBox txtMaxRange;
        protected TextBox txtMinRange;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Gradelist.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtGradeName.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级名称！");
            }
            else if (Globals.SafeInt(this.txtGradeName.Text, 0) > 20)
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入0-20之间正确的等级名称！");
            }
            else if (string.IsNullOrWhiteSpace(this.txtMinRange.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级积分下限！");
            }
            else if (string.IsNullOrWhiteSpace(this.txtGradeName.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级积分上限！");
            }
            else
            {
                string text = this.txtGradeName.Text;
                int num = int.Parse(this.txtMinRange.Text);
                int num2 = int.Parse(this.txtMaxRange.Text);
                Maticsoft.Model.SNS.GradeConfig model = new Maticsoft.Model.SNS.GradeConfig {
                    GradeName = text,
                    MinRange = new int?(num),
                    MaxRange = new int?(num2)
                };
                Maticsoft.BLL.SNS.GradeConfig config2 = new Maticsoft.BLL.SNS.GradeConfig();
                int num3 = config2.Add(model);
                if (num3 > 0)
                {
                    MessageBox.ShowSuccessTip(this, "保存成功！", "Gradelist.aspx");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加用户等级(GradeID=" + num3 + ")成功", this);
                }
                else
                {
                    this.btnCancle.Enabled = true;
                    this.btnSave.Enabled = true;
                    MessageBox.ShowFailTip(this, "系统忙，请稍后再试！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加用户等级失败", this);
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
                return 0x240;
            }
        }
    }
}

