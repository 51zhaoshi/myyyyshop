namespace Maticsoft.Web.Admin.SNS.GradeConfig
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    [Obsolete]
    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.GradeConfig bll = new Maticsoft.BLL.SNS.GradeConfig();
        protected Button btnCancle;
        protected Button btnSave;
        protected Label lblGradeID;
        protected TextBox txtGradeName;
        protected TextBox txtMaxRange;
        protected TextBox txtMinRange;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("GradeList.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
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
            else if (string.IsNullOrWhiteSpace(this.txtMaxRange.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级积分上限！");
            }
            else
            {
                int num = int.Parse(this.lblGradeID.Text);
                string text = this.txtGradeName.Text;
                int num2 = int.Parse(this.txtMinRange.Text);
                int num3 = int.Parse(this.txtMaxRange.Text);
                Maticsoft.Model.SNS.GradeConfig model = new Maticsoft.Model.SNS.GradeConfig {
                    GradeID = num,
                    GradeName = text,
                    MinRange = new int?(num2),
                    MaxRange = new int?(num3)
                };
                if (this.bll.Update(model))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改用户等级（GradeID=" + model.GradeID + "）成功", this);
                    MessageBox.ShowSuccessTip(this, "保存成功！", "GradeList.aspx");
                }
                else
                {
                    this.btnCancle.Enabled = true;
                    this.btnSave.Enabled = true;
                    MessageBox.ShowFailTip(this, "系统忙，请稍后再试！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改用户等级（GradeID=" + model.GradeID + "）失败", this);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                int gradeID = Convert.ToInt32(base.Request.Params["id"]);
                this.ShowInfo(gradeID);
            }
        }

        private void ShowInfo(int GradeID)
        {
            Maticsoft.Model.SNS.GradeConfig model = this.bll.GetModel(GradeID);
            this.lblGradeID.Text = model.GradeID.ToString();
            this.txtGradeName.Text = model.GradeName;
            this.txtMinRange.Text = model.MinRange.ToString();
            this.txtMaxRange.Text = model.MaxRange.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x241;
            }
        }
    }
}

