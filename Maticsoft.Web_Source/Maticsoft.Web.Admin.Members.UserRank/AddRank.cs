namespace Maticsoft.Web.Admin.Members.UserRank
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class AddRank : PageBaseAdmin
    {
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

        protected void btnSave_Click(object sender, EventArgs e)
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
                Maticsoft.Model.Members.UserRank model = new Maticsoft.Model.Members.UserRank {
                    Name = text,
                    PointMin = num,
                    PointMax = num2,
                    IsDefault = false,
                    Description = this.txtDesc.Text,
                    PriceOperations = "",
                    PriceType = "",
                    PriceValue = 0M,
                    RankType = 0
                };
                Maticsoft.BLL.Members.UserRank rank2 = new Maticsoft.BLL.Members.UserRank();
                int num3 = rank2.Add(model);
                if (num3 > 0)
                {
                    MessageBox.ShowSuccessTip(this, "保存成功！", "RankList.aspx");
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
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
            {
                MessageBox.ShowAndBack(this, "您没有权限");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x130;
            }
        }
    }
}

