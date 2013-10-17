namespace Maticsoft.Web.Admin.Members.UserRank
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class UpdateRank : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.UserRank bll = new Maticsoft.BLL.Members.UserRank();
        protected Button btnCancle;
        protected Button btnSave;
        protected TextBox txtDesc;
        protected TextBox txtMaxRange;
        protected TextBox txtMinRange;
        protected TextBox txtName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("RankList.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级名称！");
            }
            else if (Globals.SafeInt(this.txtName.Text, 0) > 20)
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
                string text = this.txtName.Text;
                int num = int.Parse(this.txtMinRange.Text);
                int num2 = int.Parse(this.txtMaxRange.Text);
                Maticsoft.Model.Members.UserRank model = this.bll.GetModel(this.GradeID);
                model.Name = text;
                model.PointMin = num;
                model.PointMax = num2;
                model.Description = this.txtDesc.Text;
                if (this.bll.Update(model))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改用户等级（" + model.RankId + "）成功", this);
                    MessageBox.ShowSuccessTip(this, "保存成功！", "RankList.aspx");
                }
                else
                {
                    this.btnCancle.Enabled = true;
                    this.btnSave.Enabled = true;
                    MessageBox.ShowFailTip(this, "系统忙，请稍后再试！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改用户等级（" + model.RankId + "）失败", this);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo(this.GradeID);
            }
        }

        private void ShowInfo(int GradeID)
        {
            Maticsoft.Model.Members.UserRank model = this.bll.GetModel(GradeID);
            this.txtName.Text = model.Name;
            this.txtMinRange.Text = model.PointMin.ToString();
            this.txtMaxRange.Text = model.PointMax.ToString();
            this.txtDesc.Text = model.Description;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x131;
            }
        }

        public int GradeID
        {
            get
            {
                int num = -1;
                if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], -1);
                }
                return num;
            }
        }
    }
}

